using Microsoft.Net.Http.Headers;

namespace Exchange.WebApp.HttpConfiguration
{
    public static class HttpConfiguration
    {
        public static IServiceCollection AddHttpClientFactory(this IServiceCollection services)
        {
            //services.AddHttpClient("MovieAPIClient", client =>
            //{
            //    client.BaseAddress = new Uri("https://localhost:5010/"); // API GATEWAY URL
            //    client.DefaultRequestHeaders.Clear();
            //    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            //}).AddHttpMessageHandler<AuthenticationDelegatingHandler>();


            return services;
        }
    }
}
