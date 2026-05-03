using Core.ElasticSearch;

namespace WebApi
{
    public static class CreateCustomerIndex
    {
        public static async Task EnsureElasticIndexCreateAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var elastic = scope.ServiceProvider.GetRequiredService<IElasticSearchService>();
            var result = await elastic.CreateIndexAsync("customers");

            if (!result.Success)
                Console.WriteLine($"[Elastic] Index creation skipped or failed : {result.Message}");
        }
    }
}
