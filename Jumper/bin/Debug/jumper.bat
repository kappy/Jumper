@ECHO OFF
JUMPER.EXE %*
IF EXISTS __JUMPER_EXECUTE.BAT __JUMPER_EXECUTE.BAT
IF EXISTS __JUMPER_EXECUTE.BAT DEL __JUMPER_EXECUTE.BAT