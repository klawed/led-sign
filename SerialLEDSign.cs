namespace MySign
{
    using System;
    using System.Collections.Generic;
    using System.IO.Ports;
    using System.Windows.Forms;

    internal class SerialLEDSign
    {
        private byte[] clockBytes = new byte[] { 0xef, 0x80 };
        public bool clockEnabled = true;
        private byte[] endBytes = new byte[] { 0xef, 0xb0, 0xef, 160, 0xff, 0xff, 0 };
        private byte[] fileName = new byte[] { 1 };
        private string runmode = "Runmode1";
        private static Dictionary<string, byte[]> runModes;
        private SerialPort serialPort;
        private byte[] startBytes = new byte[] { 0, 0xff, 0xff, 0, 11, 1, 0xff, 1, 0x30 };

        /*
		* portName is in the form COM1, COM2 etc
		*/
		public SerialLEDSign(string portName)
        {
            this.initSerialPort();
            this.serialPort.PortName = portName;
            try
            {
                this.serialPort.Open();
            } catch (Exception e) {
                throw new Exception("Unable to open serial port " + portName, e);
            }
        }

		private void initSerialPort()
        {
            this.serialPort = new SerialPort();
            this.serialPort.BaudRate = 0x960;
            this.serialPort.StopBits = StopBits.One;
            this.serialPort.DataBits = 8;
        }

        public void close()
        {
            if ((this.serialPort != null) && this.serialPort.IsOpen)
            {
                this.serialPort.Close();
            }
        }

        ~SerialLEDSign()
        {
            if (this.serialPort.IsOpen)
            {
                this.serialPort.Close();
            }
        }

        public static Dictionary<string, byte[]> getRunmodes()
        {
            if (runModes == null)
            {
                runModes = new Dictionary<string, byte[]>();
                for (int i = 1; i < 0x1a; i++)
                {
                    runModes.Add("Runmode" + i, new byte[] { (byte) i });
                }
            }
            return runModes;
        }

        

        public bool isClockEnabled()
        {
            return this.clockEnabled;
        }

        private string padNumber(int num)
        {
            return ((num > 9) ? num.ToString() : ("0" + num.ToString()));
        }

        public void println(string message)
        {
            this.serialPort.Write(this.startBytes, 0, this.startBytes.Length);
            this.serialPort.Write(this.fileName, 0, this.fileName.Length);
            byte[] theRunmode = new byte[1];
            this.serialPort.Write(getRunmodes()[this.runmode], 0, 1);
            if (this.isClockEnabled())
            {
                this.serialPort.Write(this.clockBytes, 0, this.clockBytes.Length);
            }
            this.serialPort.Write(message);
            this.serialPort.Write(this.endBytes, 0, this.endBytes.Length);
        }

        public void setClockEnabled(bool enable)
        {
            this.clockEnabled = enable;
        }

        public void setRunmode(string p)
        {
            this.runmode = p;
        }

        public void setTimeToCurrent()
        {
            int hourNow = DateTime.Now.Hour;
            int minuteNow = DateTime.Now.Minute;
            int year = Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2));
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            string timeString = this.padNumber(year) + this.padNumber(month) + this.padNumber(day) + this.padNumber(hourNow) + this.padNumber(minuteNow) + "00";
            byte[] bytes = new byte[] { 0, 0xff, 0xff, 0, 11, 1, 0xff, 8, 3, 0 };
            byte[] tmp= new byte[2];
            tmp[0] = 0xff;
            byte[] closeBytes = tmp;
            this.serialPort.Write(bytes, 0, bytes.Length);
            MessageBox.Show(timeString);
            this.serialPort.Write(timeString);
            this.serialPort.Write(closeBytes, 0, closeBytes.Length);
        }
    }
}

