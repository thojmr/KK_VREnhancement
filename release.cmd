::Zips the dll into the correct directory structure for release
::Make sure to increment the version

::CLear last release
rd /s /q "./release"

"%ProgramFiles%\7-Zip\7z.exe" a -tzip "./release/KK_VREnhancement v0.1.zip" "./bin/BepinEx" -mx0