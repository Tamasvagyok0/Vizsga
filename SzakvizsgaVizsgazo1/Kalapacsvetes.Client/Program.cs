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
            // Figyelem! Ne �sz n�lk�l m�sold! Ez az �n p�ld�nyom API port sz�ma!!! (Saj�todat n�zd meg a swaggerben!)
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44350/") });

            await builder.Build().RunAsync();
        }
    }
}
