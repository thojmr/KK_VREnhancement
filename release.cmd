::Zips the dll into the correct directory structure for release
::Make sure to increment the version

set kk_version=0.3
set kk_name=KK_VREnhancement

IF EXIST "./bin/%kk_name%/BepinEx/plugins/%kk_name%.dll" "%ProgramFiles%\7-Zip\7z.exe" a -tzip "%HOMEPATH%/downloads/%kk_name% v%kk_version%.zip" "./bin/%kk_name%/BepinEx" -mx0

start %HOMEPATH%/downloads