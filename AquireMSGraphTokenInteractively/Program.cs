using System;

namespace AquireMSGraphTokenInteractively
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            IPublicClientApplication app;
            app = PublicClientApplicationBuilder.Create(clientId)
                    .WithDefaultRedirectUri()
                    .Build();
        }
    }
}
