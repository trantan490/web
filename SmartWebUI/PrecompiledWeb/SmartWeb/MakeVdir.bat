@echo off
rem %1 = Real Directory
rem %2 = Web Site

if '%1'=='' goto wrong

if '%2'=='' goto default

iisweb /create %1 %2 /b 80

iisvdir /create %2 bin %1\bin
iisvdir /create %2 install %1\install
iisvdir /create %2 SmartWeb %1\SmartWeb
iisvdir /create %2 SmartWebService %1\SmartWebService

goto end

:default
iisvdir /create w3svc/1/ROOT bin %1\bin
iisvdir /create w3svc/1/ROOT install %1\install
iisvdir /create w3svc/1/ROOT SmartWeb %1\SmartWeb
iisvdir /create w3svc/1/ROOT SmartWebService %1\SmartWebService

goto end

:wrong
cls
echo Input Real Directory.
echo MakeVdir Real_Directory [Web Site]

:end
