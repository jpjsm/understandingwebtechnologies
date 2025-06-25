using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;
using CsvHelper;


namespace AzureSearch
{
    class Program
    {
        static List<OrganizationHierarchy> OrganizationHierarchyRecords = new List<OrganizationHierarchy>();
        static List<Service> Services = new ();
        static List<Subscription> Subscriptions = new();
        const string ServiceName = "jujofretestazuresearch";
        const string ServiceUrl = "https://jujofretestazuresearch.search.windows.net";
        const string OrganizationHierarchyServiceIndexName = "ServiceIndex";
        const string OrganizationHierarchySubscriptionIndexName = "SubscriptionIndex";
        const string ApiKey = "954AE01792A2B1C254B6EDC6D4763EF7";
        static AzureKeyCredential credential;
        static SearchIndexClient adminClient;
        static SearchClient serviceIndexClient;
        static SearchClient subscriptionIndexClient;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello AzureSearch Demo!");

            DateTime stopwatch = DateTime.Now;
            OrganizationHierarchyRecords = LoadOrganizationHierarchyRecords();
            Console.WriteLine($"Elapsed time to load CSV file: {(DateTime.Now - stopwatch).TotalMilliseconds:N2} ms");

            stopwatch = DateTime.Now;
            LoadServicesAndSubscriptions();
            Console.WriteLine($"Elapsed time to build Services and Subscriptions data: {(DateTime.Now - stopwatch).TotalMilliseconds:N2} ms");

            stopwatch = DateTime.Now;
            SetupSearchClient();
            Console.WriteLine($"Elapsed time to build SearchClient: {(DateTime.Now - stopwatch).TotalMilliseconds:N2} ms");

            stopwatch = DateTime.Now;
            CreateIndex(typeof(Service), OrganizationHierarchyServiceIndexName, adminClient, "service_sg", "ServiceName", "ServiceKey");
            CreateIndex(typeof(Subscription), OrganizationHierarchySubscriptionIndexName, adminClient, "subscription_sg", "SubscriptionName", "SubscriptionKey");
            Console.WriteLine($"Elapsed time to create indexes: {(DateTime.Now - stopwatch).TotalMilliseconds:N2} ms");

            stopwatch = DateTime.Now;
            IndexDocumentsBatch<Service> serviceBatch = IndexDocumentsBatch.Upload(Services);
            try
            {
                IndexDocumentsResult result = serviceIndexClient.IndexDocuments(serviceBatch);
                Console.WriteLine($"Elapsed time to upload Service data to search index: {(DateTime.Now - stopwatch).TotalMilliseconds:N2} ms");
            }
            catch (Exception ex)
            {
                // If for some reason any documents are dropped during indexing, you can compensate by delaying and
                // retrying. This simple demo just logs the failed document keys and continues.
                Console.WriteLine($"Elapsed time to error while trying to upload Service data to search index: {(DateTime.Now - stopwatch).TotalMilliseconds:N2} ms");
                Console.WriteLine($"Failed to index some of the documents: {ex.Message}");
            }
        }

        private static void CreateIndex(Type _type, string indexName, SearchIndexClient adminClient, string suggesterName, params string[] suggesterValues)
        {
            FieldBuilder fieldBuilder = new FieldBuilder();
            var searchFields = fieldBuilder.Build(_type);

            var definition = new SearchIndex(indexName, searchFields);

            var suggester = new SearchSuggester(suggesterName, suggesterValues);// new[] { "HotelName", "Category", "Address/City", "Address/StateProvince" });
            definition.Suggesters.Add(suggester);

            adminClient.CreateOrUpdateIndex(definition);
        }

        private static void SetupSearchClient()
        {
            // Create a SearchIndexClient to send create/delete index commands
            Uri serviceEndpoint = new Uri(ServiceUrl);
            credential = new AzureKeyCredential(ApiKey);
            adminClient = new SearchIndexClient(serviceEndpoint, credential);

            // Create a SearchClient to load and query documents
            serviceIndexClient = new SearchClient(serviceEndpoint, OrganizationHierarchyServiceIndexName, credential);
            subscriptionIndexClient = new SearchClient(serviceEndpoint, OrganizationHierarchyServiceIndexName, credential);
        }

        private static void LoadServicesAndSubscriptions()
        {
            HashSet<Guid> serviceIds = new();
            HashSet<Guid> subscriptionIds = new();

            foreach (OrganizationHierarchy record in OrganizationHierarchyRecords)
            {
                if (record.ServiceId.HasValue && serviceIds.Add(record.ServiceId.Value))
                {
                    Services.Add(new Service() { ServiceId = record.ServiceId.Value, ServiceName = record.ServiceName });
                }

                if (record.SubscriptionId.HasValue && subscriptionIds.Add(record.SubscriptionId.Value))
                {
                    Subscriptions.Add(new Subscription() { SubscriptionId = record.SubscriptionId.Value, SubscriptionName = record.SubscriptionName });
                }
            }
        }

        private static List<OrganizationHierarchy> LoadOrganizationHierarchyRecords()
        {
            using (var reader = new StreamReader("ServiceTree_OrganizationHierarchy.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<OrganizationHierarchy>().ToList();
            }
        }
    }

    public class OrganizationHierarchy
    {
        [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string DivisionName { get; set; }

        [SimpleField(IsKey = true, IsFilterable = true)]
        public Guid? DivisionId { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string OrganizationName { get; set; }

        [SimpleField(IsKey = true, IsFilterable = true)]
        public Guid? OrganizationId { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string ServiceGroupName { get; set; }

        [SimpleField(IsKey = true, IsFilterable = true)]
        public Guid? ServiceGroupId { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string TeamGroupName { get; set; }

        [SimpleField(IsKey = true, IsFilterable = true)]
        public Guid? TeamGroupId { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string ServiceName { get; set; }

        [SimpleField(IsKey = true, IsFilterable = true)]
        public Guid? ServiceId { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string MicroServiceName { get; set; }

        [SimpleField(IsKey = true, IsFilterable = true)]
        public Guid? MicroServiceId { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string ComponentName { get; set; }

        [SimpleField(IsKey = true, IsFilterable = true)]
        public Guid? ComponentId { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string ModifiedBy { get; set; }

        [SimpleField(IsKey = true, IsFilterable = true)]
        public Guid? SubscriptionId { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string SubscriptionName { get; set; }

        [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string ServiceLevel { get; set; }
    }

    class Service
    {
        [FieldBuilderIgnore]
        public Guid ServiceId { get; set; }

        [SimpleField(IsKey = true, IsFilterable = true)]
        public string ServiceKey => ServiceId.ToString();

        [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string ServiceName { get; set; }
    }

    class Subscription
    {
        [FieldBuilderIgnore]
        public Guid SubscriptionId { get; set; }

        [SimpleField(IsKey = true, IsFilterable = true)]
        public string SubscriptionKey => SubscriptionId.ToString();

        [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
        public string SubscriptionName { get; set; }
    }
}
