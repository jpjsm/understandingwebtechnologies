using System;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace AzureKeyVaultPrimer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting AzureKeyVaultPrimer !!");

            string keyVaultName = "jujofre-test-kv";
            string kvUri = $"https://{keyVaultName}.vault.azure.net/";

            SecretClient client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

            // Retrieve a secret
            string secretName = "thesecret";
            var secret = await client.GetSecretAsync(secretName);

            Console.WriteLine($"... and the secret is: '{secret.Value.Value}'");

            Console.WriteLine("Completed AzureKeyVaultPrimer !!");
        }
    }
}
