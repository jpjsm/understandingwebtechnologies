kubectl delete secret changemanager-secret-tls --namespace azurekeyvault-ns
#kubectl create secret tls changemanager-secret-tls --namespace azurekeyvault-ns --cert=.\mtls-certs\change-manager.fcm.test.crt --key=.\mtls-certs\change-manager.fcm.test.key -o yaml
#kubectl create secret tls changemanager-secret-tls --namespace azurekeyvault-ns --cert=.\changemanager.fcm.msftcloudes.com\changemanager.fcm.msftcloudes.com.crt --key=.\changemanager.fcm.msftcloudes.com\changemanager.fcm.msftcloudes.com.key -o yaml
kubectl create secret tls changemanager-secret-tls --namespace azurekeyvault-ns --cert=.\changemanager.fcm.msftcloudes.com\changemanager.fcm.msftcloudes.com.pem --key=.\changemanager.fcm.msftcloudes.com\changemanager.fcm.msftcloudes.com.key -o yaml

kubectl delete secret changemanager-secret-mtls --namespace azurekeyvault-ns
#kubectl create secret generic changemanager-secret-mtls --namespace azurekeyvault-ns --from-file=tls.crt=.\mtls-certs\change-manager.fcm.test.crt --from-file=tls.key=.\mtls-certs\change-manager.fcm.test.key --from-file=ca.crt=.\mtls-certs\ca-root.crt -o yaml
kubectl create secret generic changemanager-secret-mtls --namespace azurekeyvault-ns --from-file=tls.crt=.\changemanager.fcm.msftcloudes.com\changemanager.fcm.msftcloudes.com.pem --from-file=tls.key=.\changemanager.fcm.msftcloudes.com\changemanager.fcm.msftcloudes.com.key --from-file=ca.crt=.\changemanager.fcm.msftcloudes.com\Microsoft-Azure-TLS-Issuing-CA-06.crt -o yaml
