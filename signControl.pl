
#!/usr/bin/perl

#
# /bin/stty 2400 -opost -ocrnl -onlcr cs8 parenb -parodd stop 0 < /dev/ttyUSB0
#
use strict;

# runmode gets ignored if message is to big to fit on screen without scrolling???

# define runmodes \x1 -\x19
my $someRunMode = "\x2";
my $CYCLIC = "\x1";
my $STATIC = "\x2";
# define properties

# Symbols \x60 - \x7D
my $STAR="\x60";
my $SQUIGGLY="\x61";
my $SPAZ="\x62";

my $TIME="\x80";
my $DATE="\x81";

# Animations \x90 - \x97
my $MERRY_CHRISTMAS="\x90";
my $HAPPY_NEWYEAR="\x91";
my $FOURTH_OF_JULY="\x92";
my $HAPPY_EASTER="\x93";
my $HAPPY_HALLOWEEN="\x94";
my $DONT_DRINK_AND_DRIVE="\x95";
my $NO_SMOKING="\x96";
my $WELCOME="\X97";


# Fonts \xA0 - \xA5
my $FIVEbyFIVE     = "\xA0";  # all caps
my $TENbyFIVE      = "\xA1";  # all caps
my $FIVEbySEVEN    = "\xA2";  # mixed case
my $TENbySEVEN     = "\xA3";  # mixed case
my $EIGHTbySEVEN   = "\xA4";  # mixed case
my $SIXTEENbySEVEN = "\xA5";  # mixed case

# Colors \xB0 - \xBF
my $BRIGHT_RED = "\xef" . "\xb0";
my $ALL_COLORS = "\xef" . "\xc0";

# Speed \xC0 -\xC7

# Pause Time \xC8 -\xCF

# Graphics \xD0 - \xDF

# Beeps \xE0 - \xE2




#my $header     = "\x0" . "\xff" . "\xff" . "\x1" . "\x1";
my $header = "\x0". "\xff" . "\xff" . "\x0" . "\xb" . "\x1" . "\xff" . "\x1" . "\x30" . "\x31" . "\xef" . "\xb0" . "\xef" . "\xa2" . "feembooso" . "\xef" ."\xb1" . "\xef" . "\xa0" . "\xff" . "\xff" . "\x0";

my $fileName   = "01";


my $runMode    = "\x19";
my $properties = "\xef" . "\xA0";
my $color      = "";
my $message    = $ARGV[0];
my $endFrame   = "\xff";
my $endFile    =  "\xff" . "\x0";

print $header . $fileName . $runMode . $properties . $message . $endFrame . $endFile;
