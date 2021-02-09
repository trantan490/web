:: SET ENV (개인 경로에 맞추어 이곳을 수정하세요)
set _SRC_DIR=D:\MESplus_HANA\Korea\MESPLUS_HANA\SmartWebV42\Component
set _TARGET_DIR=Y:\bin
set _BACKUP_ROOT=%_TARGET_DIR%\backup
set _SRC_DIR_CAPTION=D:\MESplus_HANA\Korea\MESPLUS_HANA\SmartWebV42\SmartWebUI\Miracom.SmartWeb.MainUI\bin\Debug

:: MAKE ENV
set todayDate=%date:~0,4%%date:~5,2%%date:~8,2%
:set todayTime=%time:~0,2%%time:~3,2%%time:~6,2%
:set backupDir=%_BACKUP_ROOT%\dlls_%todayDate%_%todayTime%
set backupDir=%_BACKUP_ROOT%\dlls_%todayDate%

:: Back up
mkdir %backupDir%
copy %_TARGET_DIR%\Miracom.SmartWeb.MainUI.dll %backupDir%\
copy %_TARGET_DIR%\Miracom.SmartWeb.CusUI.dll %backupDir%\
copy %_TARGET_DIR%\Hana.TAT.dll %backupDir%\
copy %_TARGET_DIR%\Hana.CUS.dll %backupDir%\
copy %_TARGET_DIR%\Hana.PQC.dll %backupDir%\
copy %_TARGET_DIR%\Hana.PRD.dll %backupDir%\
copy %_TARGET_DIR%\Hana.RAS.dll %backupDir%\
copy %_TARGET_DIR%\Hana.YLD.dll %backupDir%\
copy %_TARGET_DIR%\Hana.MAT.dll %backupDir%\
copy %_TARGET_DIR%\Hana.TRN.dll %backupDir%\
copy %_TARGET_DIR%\Hana.REG.dll %backupDir%\
copy %_TARGET_DIR%\Hana.RFID.dll %backupDir%\
copy %_TARGET_DIR%\Miracom.SmartWeb.StdUI.dll %backupDir%\
copy %_TARGET_DIR%\Miracom.SmartWeb.UI.dll %backupDir%\
copy %_TARGET_DIR%\SmartWeb.xml %backupDir%\
copy %_TARGET_DIR%\SmartWebCaption.xml %backupDir%\


:: SOURCE COPY
copy /Y %_SRC_DIR%\Miracom.SmartWeb.MainUI.dll %_TARGET_DIR%\
copy /Y %_SRC_DIR%\Miracom.SmartWeb.CusUI.dll %_TARGET_DIR%\
copy /Y %_SRC_DIR%\Hana.TAT.dll %_TARGET_DIR%\
copy /Y %_SRC_DIR%\Hana.CUS.dll %_TARGET_DIR%\
copy /Y %_SRC_DIR%\Hana.PQC.dll %_TARGET_DIR%\
copy /Y %_SRC_DIR%\Hana.PRD.dll %_TARGET_DIR%\
copy /Y %_SRC_DIR%\Hana.RAS.dll %_TARGET_DIR%\
copy /Y %_SRC_DIR%\Hana.YLD.dll %_TARGET_DIR%\
copy /Y %_SRC_DIR%\Hana.MAT.dll %_TARGET_DIR%\
copy /Y %_SRC_DIR%\Hana.TRN.dll %_TARGET_DIR%\
copy /Y %_SRC_DIR%\Hana.REG.dll %_TARGET_DIR%\
copy /Y %_SRC_DIR%\Hana.RFID.dll %_TARGET_DIR%\
copy /Y %_SRC_DIR%\Miracom.SmartWeb.StdUI.dll %_TARGET_DIR%\
copy /Y %_SRC_DIR%\Miracom.SmartWeb.UI.dll %_TARGET_DIR%\
copy /Y %_SRC_DIR%\SmartWeb.xml %_TARGET_DIR%\
copy /Y %_SRC_DIR_CAPTION%\SmartWebCaption.xml %_TARGET_DIR%\

pause