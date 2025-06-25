# Create CA Root
openssl req -new -x509 -newkey rsa:4096 -sha256 -nodes -keyout ca-root.key -days 3560 -out ca-root.crt -config ca-root.cnf

# Create Server key and cert (signed by CA Root)
openssl req -new -newkey rsa:4096 -keyout ingress-mtls.example.key -out ingress-mtls.example.csr -nodes -config ingress-mtls.example.cnf

openssl x509 -req -sha256 -days 365 -in ingress-mtls.example.csr -CA ca-root.crt -CAkey ca-root.key -set_serial 01 -out ingress-mtls.example.crt

# Create Client key and cert (signed by CA Root)
openssl req -new -newkey rsa:4096 -keyout ingress-mtls-client.example.key -out ingress-mtls-client.example.csr -nodes -config ingress-mtls-client.example.cnf
openssl x509 -req -sha256 -days 365 -in ingress-mtls-client.example.csr -CA ca-root.crt -CAkey ca-root.key -set_serial 02 -out ingress-mtls-client.example.crt


# Create PEM and PFX objects
openssl rsa -in ca-root.key -outform PEM -out ca-root.key.pem 
openssl rsa -in ingress-mtls.example.key -outform PEM -out ingress-mtls.example.key.pem
openssl rsa -in ingress-mtls-client.example.key -outform PEM -out ingress-mtls-client.example.key.pem

openssl x509 -in ingress-mtls.example.crt -out ingress-mtls.example.pem
openssl x509 -in ingress-mtls-client.example.crt -out ingress-mtls-client.example.pem

openssl pkcs12 -export -out ingress-mtls-client.example.pfx -inkey ingress-mtls-client.example.key -in ingress-mtls-client.example.crt -certfile ca-root.crt