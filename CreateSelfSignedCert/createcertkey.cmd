@echo ON


REM Create CA Root
openssl req -new -x509 -newkey rsa:4096 -sha256 -nodes -keyout ca-root.key -days 3560 -out ca-root.crt -config ca-root.cnf

REM Create Server key and cert (signed by CA Root)
openssl req -new -newkey rsa:4096 -keyout serendipity.test.key -out serendipity.test.csr -nodes -config serendipity.test.cnf
openssl x509 -req -sha256 -days 365 -in serendipity.test.csr -CA ca-root.crt -CAkey ca-root.key -set_serial 01 -out serendipity.test.crt

REM Create Client key and cert (signed by CA Root)
openssl req -new -newkey rsa:4096 -keyout serendipity-client.test.key -out serendipity-client.test.csr -nodes -config serendipity-client.test.cnf
openssl x509 -req -sha256 -days 365 -in serendipity-client.test.csr -CA ca-root.crt -CAkey ca-root.key -set_serial 02 -out serendipity-client.test.crt


REM Create PEM and PFX objects
openssl rsa -in ca-root.key -outform PEM -out ca-root.key.pem 
openssl rsa -in serendipity.test.key -outform PEM -out serendipity.test.key.pem
openssl rsa -in serendipity-client.test.key -outform PEM -out serendipity-client.test.key.pem

openssl x509 -in serendipity.test.crt -out serendipity.test.pem
openssl x509 -in serendipity-client.test.crt -out serendipity-client.test.pem

openssl pkcs12 -export -out serendipity-client.test.pfx -inkey serendipity-client.test.key -in serendipity-client.test.crt -certfile ca-root.crt