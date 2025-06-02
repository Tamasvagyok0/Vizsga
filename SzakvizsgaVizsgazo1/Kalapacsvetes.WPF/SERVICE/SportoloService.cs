using KozosKonyvtar.MODEL;
using KozosKonyvtar.VIEWMODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kalapacsvetes.WPF.SERVICE
{
    public class SportoloApiService
    {
        private static readonly HttpClient _http = App.Http; // Példányosítsd a HtmlClint osztályt az App.xaml.cs-ből 

        public async Task<List<VersenyViewModel>> GetAllAsync() // Az összes rekord lekérdezése 
        {
            // Lekérdezzük az összes táblát amiből adatra van szükség
            var versenyek = await _http.GetFromJsonAsync<List<Verseny>>("Verseny") ?? new(); // Főtábla
            var sportolok = await _http.GetFromJsonAsync<List<Sportolo>>("Sportolo") ?? new(); // Kell a neve és országId

            var orszagok = await _http.GetFromJsonAsync<List<Orszag>>("Orszag") ?? new();  // Kell az OrszagKod
            var helyszinek = await _http.GetFromJsonAsync<List<Helyszin>>("Helyszin") ?? new(); // Kell a helyszin

            var viewModelLista = versenyek.Select(v =>
            {
                // A Verseny rekordban levő ID-k alapján szűkítjük az adatokat
                var sportolo = sportolok.FirstOrDefault(s => s.SportoloId == v.SportoloId);
                var orszag = orszagok.FirstOrDefault(o => o.OrszagId == sportolo?.OrszagId);
                var helyszin = helyszinek.FirstOrDefault(h => h.HelyszinId == v.HelyszinId);

                // ez a return nem a metódusból lép ki, egy rekordot ad vissza a .Select függvénynek
                return new VersenyViewModel // Egy sor feltöltése a kapcsolódó táblák adataival
                {
                    VersenyId = v.VersenyId,
                    SportoloNev = sportolo.SportoloNev ,
                    OrszagKod = orszag.OrszagKod,
                    HelyszinNev = helyszin.HelyszinNev,
                    Datum = v.Datum,
                    Eredmeny = v.Eredmeny,
                    Helyezes = v.Helyezes
                };
            })
            .OrderBy(v => v.SportoloNev)
            .ToList();

            return viewModelLista;
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"Verseny/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task AddAsync(VersenyViewModel model)
        {
            // Fordítva működik, mint a GetAllAsync
            // AVersenyViewModeel adatait kell szédszedni a táblák adataira
            // és a Verseny táblát fel kell tölteni az ...Id-kel

            // 1. Ország megkeresése OrszagKod alapján
            var orszagok = await _http.GetFromJsonAsync<List<Orszag>>("Orszag");
            var orszag = orszagok?.FirstOrDefault(o => o.OrszagKod == model.OrszagKod);

            if (orszag == null) // Ha nincs ilyen ország felírjuk
            {
                orszag = new Orszag
                {
                    OrszagKod = model.OrszagKod
                };

                var orszagResp = await _http.PostAsJsonAsync("Orszag", orszag);
                orszagResp.EnsureSuccessStatusCode(); // Státuszkódot adja vissza
                orszag = await orszagResp.Content.ReadFromJsonAsync<Orszag>(); // Visszakeressük a beszurt elemet
            }

            // 2. Sportoló megkeresése (név alapján)
            var sportolok = await _http.GetFromJsonAsync<List<Sportolo>>("Sportolo");
            var sportolo = sportolok?.FirstOrDefault(s => s.SportoloNev == model.SportoloNev);

            if (sportolo == null)
            {
                sportolo = new Sportolo
                {
                    SportoloNev = model.SportoloNev,
                    OrszagId = orszag.OrszagId
                };

                var response = await _http.PostAsJsonAsync("Sportolo", sportolo);
                response.EnsureSuccessStatusCode();
                sportolo = await response.Content.ReadFromJsonAsync<Sportolo>();
            }

            // 3. Helyszín megkeresése
            var helyszinek = await _http.GetFromJsonAsync<List<Helyszin>>("Helyszin");
            var helyszin = helyszinek?.FirstOrDefault(h => h.HelyszinNev == model.HelyszinNev);

            if (helyszin == null)
            {
                helyszin = new Helyszin
                {
                    HelyszinNev = model.HelyszinNev
                };

                var response = await _http.PostAsJsonAsync("Helyszin", helyszin);
                response.EnsureSuccessStatusCode();
                helyszin = await response.Content.ReadFromJsonAsync<Helyszin>();
            }

            // 4. Verseny beszúrása
            var verseny = new Verseny
            {
                Datum = model.Datum,
                Eredmeny = model.Eredmeny,
                Helyezes = model.Helyezes,
                SportoloId = sportolo.SportoloId,
                HelyszinId = helyszin.HelyszinId
            };

            var versenyResp = await _http.PostAsJsonAsync("Verseny", verseny);
            versenyResp.EnsureSuccessStatusCode();
        }
    }

 }
