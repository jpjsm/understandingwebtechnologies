@ECHO OFF
if "%~1" == "" GOTO No_Arguments

set "pfx=%~1"

if not exist %pfx% GOTO Pfx_Not_Exist

set "cert=%~1"
if NOT "%~2" == "" (set "cert=%~2")


ECHO PFX: %pfx%
ECHO Cert: %cert%

@ECHO ON
openssl pkcs12 -in %pfx% -nocerts -nodes -out %cert%.key
openssl pkey -in %cert%.key -outform PEM -out %cert%.key.pem
openssl pkcs12 -in %pfx% -clcerts -nokeys -out %cert%.crt
openssl x509 -in %cert%.crt -out %cert%.pem


GOTO End

:No_Arguments
@ECHO OFF
ECHO NO arguments given, exit without executing.
ECHO -                                              -
ECHO Usage:
ECHO       Pfx2PEMs <cert-file>.PFX [<new-cert-name>]
GOTO END

:Pfx_Not_Exist
ECHO PKCS12 file not exists %1, exit without executing.

:End