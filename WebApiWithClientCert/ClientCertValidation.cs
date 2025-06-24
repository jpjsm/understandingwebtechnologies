
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace WebApiWithClientCert
{
    public class ClientCertValidation
    {
        private static X509Certificate2 issuer = new X509Certificate2(@"certs\AMERoot_ameroot.crt");

        public bool ValidateClientCert(X509Certificate2 clientCert)
        {
            X509Chain clientcertchain = new X509Chain();
            clientcertchain.Build(clientCert);
            HashSet<string> chainthumbprints = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            foreach (X509ChainElement item in clientcertchain.ChainElements)
            {
                chainthumbprints.Add(item.Certificate.Thumbprint);
            }

            bool result = chainthumbprints.Contains(issuer.Thumbprint);
            string resultmsg = result 
                ? $"Client was issued by AME issuer: {issuer.Thumbprint} in [{string.Join(", ", chainthumbprints.Select(t => $"'{t}'"))}]" 
                : $"Client was NOT issued by AME issuer: {issuer.Thumbprint} NOT in [{string.Join(", ", chainthumbprints.Select(t => $"'{t}'"))}]";
            Console.WriteLine($"[ClientCertValidation:ValidateClientCert]: {resultmsg}");

            return result;
        }
    }
}
