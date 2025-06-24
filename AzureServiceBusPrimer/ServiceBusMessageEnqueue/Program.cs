using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;


namespace ServiceBusMessageEnqueue
{
    class Program
    {
        // connection string to your Service Bus namespace
        static string sbcnxstr = "<NAMESPACE CONNECTION STRING>";

        // name of your Service Bus queue
        static string queueName = "changeevent";

        // the client that owns the connection and can be used to create senders and receivers
        static ServiceBusClient serviceBusClient;

        // the sender used to publish messages to the queue
        static ServiceBusSender sender;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting ServiceBusMessageEnqueue !!");

            #region Get secret connection string from key-vault --> sbcnxstr
            string keyVaultName = "jujofre-test-kv";
            string kvUri = $"https://{keyVaultName}.vault.azure.net/";

            SecretClient keyVAultClient = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

            // Retrieve a secret
            string secretName = "RootManageSharedAccessPrimaryConnectionString";
            KeyVaultSecret sbcnxstrsecret = await keyVAultClient.GetSecretAsync(secretName);

            sbcnxstr = sbcnxstrsecret.Value;
            #endregion

            // Create the clients that we'll use for sending and processing messages.
            serviceBusClient = new ServiceBusClient(sbcnxstr);
            sender = serviceBusClient.CreateSender(queueName);


            
            Console.WriteLine("Complete ServiceBusMessageEnqueue !!");
        }
    }
}
