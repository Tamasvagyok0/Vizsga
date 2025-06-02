using KozosKonyvtar.VIEWMODEL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kalapacsvetes.WPF
{
    /// <summary>
    /// Interaction logic for UjAdatAblak.xaml
    /// </summary>
    public partial class UjAdatAblak : Window
    {
        //                       lekérdezhető, de csak helyben módosítható
        public VersenyViewModel? UjAdat { get; private set; } // Egy ilyen modelt töltünk fel adatokkal
        public UjAdatAblak()
        {
            InitializeComponent();

            // Magyar dátumformátum beállítása DatePicker-re
            DatumPicker.Language = System.Windows.Markup.XmlLanguage.GetLanguage("hu-HU");

        }
        /// <summary>
        /// Az uj adat felvitelének elvetése
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Megsem_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        /// <summary>
        /// Uj adat mentése
        /// Egy SportoloViewModel osztály feltöltése adatokkal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mentes_Click(object sender, RoutedEventArgs e)
        {
            // Kitöltöttség ellenörzése
            if (string.IsNullOrWhiteSpace(NevBox.Text) ||
                 string.IsNullOrWhiteSpace(OrszagkodBox.Text) ||
                 string.IsNullOrWhiteSpace(HelyszinBox.Text) ||
                 !DatumPicker.SelectedDate.HasValue ||
                 !decimal.TryParse(EredmenyBox.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal eredmeny))
            {
                MessageBox.Show("Kérlek, töltsd ki helyesen az összes mezőt!",
                                     "Hiányzó vagy hibás adat", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Dátum konvertálása DateOnly-ra
            var datum = DateOnly.FromDateTime(DatumPicker.SelectedDate.Value);

            // Ennek a példánynak a mentése történik meg 
            // Az UsAdatAblak bezárása után a MainWindow UjAdat_Click metódusában.
            UjAdat = new VersenyViewModel
            {
                SportoloNev = NevBox.Text.Trim(),
                OrszagKod = OrszagkodBox.Text.Trim(),
                HelyszinNev = HelyszinBox.Text.Trim(),
                Datum = datum,
                Eredmeny = eredmeny,
                Helyezes = 0 // Ha nem szerepel, nullázzuk
            };

            DialogResult = true;
            Close();
        }
    }
}
