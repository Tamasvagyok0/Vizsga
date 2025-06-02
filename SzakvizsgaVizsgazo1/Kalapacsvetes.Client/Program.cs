using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Kalapacsvetes.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            // Figyelem! Ne ész nélkül másold! Ez az én példányom API port száma!!! (Sajátodat nézd meg a swaggerben!)
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44350/") });

            await builder.Build().RunAsync();
        }
    }
}
