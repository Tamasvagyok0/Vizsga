using Kalapacsvetes.WPF.SERVICE;
using KozosKonyvtar.VIEWMODEL;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Kalapacsvetes.WPF
{
    public partial class MainWindow : Window
    {
        private readonly SportoloApiService _api = new();  // Példányosítsuk a szolgáltatást
        private List<VersenyViewModel> _sportolok = new(); // Ebben gyűjtjük a verseny adatokat

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;  // A lap betöltése után hívja meg a MainWindow_Loaded metódust
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await BetoltAdatokAsync();
        }

        private async Task BetoltAdatokAsync()
        {
            _sportolok = await _api.GetAllAsync();   // Meghívja a szolgálatásokból azt a metódust, amely minden rekordot betölt
            SportoloLista.ItemsSource = _sportolok;  // A ListBox adatnézete
            SportoloLista.SelectedIndex = 0;
            DataContext = _sportolok;                // Az űrlap adatnézete
            KiemelkedoBox.Text = $"Összesen: {_sportolok.Count} sportoló";
        }

        private void SportoloLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var v = (VersenyViewModel)SportoloLista.SelectedItem; //Változott a kijelölt
            DataContext = v; // Új dataContext
        }

        private async void Torles_Click(object sender, RoutedEventArgs e)
        {
            if (SportoloLista.SelectedItem is not VersenyViewModel v) return;

            var megerosites = MessageBox.Show(
                $"Biztosan törölni szeretnéd:\n{v.SportoloNev} ({v.VersenyId})?",
                "Törlés megerősítése",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (megerosites != MessageBoxResult.Yes) return;

            await _api.DeleteAsync(v.VersenyId);
            await BetoltAdatokAsync();
        }

        private async void UjAdat_Click(object sender, RoutedEventArgs e)
        {
            var ablak = new UjAdatAblak(); // Megnyitjuk az UjAdatAblakot
            if (ablak.ShowDialog() == true) // UjAdatAblak bezárása 
            {
                //  UjAdat az  UjAdatAblak publikus tulajdonsága
                // Bezárás után még hivatkozhatunk rá
                await _api.AddAsync(ablak.UjAdat);
                await BetoltAdatokAsync();
            }
        }
    }
}
