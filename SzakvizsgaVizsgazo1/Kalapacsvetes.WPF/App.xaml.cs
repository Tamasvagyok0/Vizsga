using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;

namespace Kalapacsvetes.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly HttpClient Http = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44350/api/"),
            //Timeout = TimeSpan.FromSeconds(2000)
        };
    }

}
