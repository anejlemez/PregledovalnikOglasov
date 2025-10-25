using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PregledovalnikOglasov
{
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<Oglas> _oglasi = new();
        private ICollectionView _oglasiView;

        public MainWindow()
        {
            InitializeComponent();

         
            _oglasi.Add(new Oglas { Naziv = "Audi A4 2.0 TDI", Cena = 12900, Letnik = 2018, Kilometri = 145000, Gorivo = "Dizel", Znamka = "Audi", Model = "A4", Menjalnik = "Ročni", Slika = "https://assets.adac.de/image/upload/v1/Autodatenbank/Fahrzeugbilder/im05628-1-audi-a4.jpg" });
            _oglasi.Add(new Oglas { Naziv = "BMW 320d xDrive", Cena = 15900, Letnik = 2019, Kilometri = 120000, Gorivo = "Dizel", Znamka = "BMW", Model = "320d", Menjalnik = "Avtomatski", Slika = "https://www.avtoo.si/assets/img/car/1189/02_718825536.jpg" });
            _oglasi.Add(new Oglas { Naziv = "Volkswagen Golf 1.6 TDI", Cena = 8900, Letnik = 2016, Kilometri = 175000, Gorivo = "Dizel", Znamka = "Volkswagen", Model = "Golf", Menjalnik = "Ročni", Slika = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/8a/2020_Volkswagen_Golf_Style_1.5_Front.jpg/1200px-2020_Volkswagen_Golf_Style_1.5_Front.jpg" });
            _oglasi.Add(new Oglas { Naziv = "Renault Clio 1.2 16V", Cena = 6900, Letnik = 2015, Kilometri = 98000, Gorivo = "Bencin", Znamka = "Renault", Model = "Clio", Menjalnik = "Ročni", Slika = "https://upload.wikimedia.org/wikipedia/commons/8/88/Nouvelle_Renault_Clio_E-Tech_full_hybrid.png" });
            _oglasi.Add(new Oglas { Naziv = "Škoda Octavia 2.0 TDI", Cena = 11900, Letnik = 2017, Kilometri = 130000, Gorivo = "Dizel", Znamka = "Škoda", Model = "Octavia", Menjalnik = "Ročni", Slika = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTowW4zgOIwrKqqXnAZ84x87oO9NK7C98SpTw&s" });
            _oglasi.Add(new Oglas { Naziv = "Peugeot 308 1.2 PureTech", Cena = 7500, Letnik = 2016, Kilometri = 160000, Gorivo = "Bencin", Znamka = "Peugeot", Model = "308", Menjalnik = "Ročni", Slika = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9e/2022_-_Peugeot_308_III_%28C%29_-_121.jpg/1200px-2022_-_Peugeot_308_III_%28C%29_-_121.jpg" });
            _oglasi.Add(new Oglas { Naziv = "Toyota Yaris Hybrid", Cena = 10400, Letnik = 2018, Kilometri = 90000, Gorivo = "Hibrid", Znamka = "Toyota", Model = "Yaris", Menjalnik = "Avtomatski", Slika = "https://www.motorfinity.uk/photos/Yaris_Blog_1732619536.jpg" });

            lvOglasi.ItemsSource = _oglasi;
            _oglasiView = CollectionViewSource.GetDefaultView(lvOglasi.ItemsSource);
        }

        private void MenuIzhod_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnIsci_Click(object sender, RoutedEventArgs e)
        {
            var query = (txtIskanje.Text ?? "").Trim();

            if (string.IsNullOrWhiteSpace(query))
            {
                _oglasiView.Filter = null; 
            }
            else
            {
                _oglasiView.Filter = item =>
                {
                    if (item is not Oglas o) return false;
                    return o.Naziv.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                           o.Znamka.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                           o.Model.Contains(query, StringComparison.OrdinalIgnoreCase);
                };
            }

            _oglasiView.Refresh();
        }

        private void BtnPonastavi_Click(object sender, RoutedEventArgs e)
        {
            cbZnamka.SelectedIndex = 0;
            txtModel.Text = "";
            txtCenaMin.Text = "";
            txtCenaMax.Text = "";
            txtKmMin.Text = "";
            txtKmMax.Text = "";
            chk1.IsChecked = chk2.IsChecked = chk3.IsChecked = false;
            txtIskanje.Text = "";

            _oglasiView.Filter = null;
            _oglasiView.Refresh();
        }

        private void BtnFiltriraj_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void lvOglasi_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvOglasi.SelectedItem is Oglas o)
                MessageBox.Show($"Izbran oglas: {o.Naziv}");
        }

        private void lvOglasi_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lvOglasi.SelectedItem is not Oglas selected) return;

            txtNaziv.Text = selected.Naziv;
            lblCena.Content = $"Cena: {selected.CenaString}";
            lblLetnik.Content = $"Letnik: {selected.Letnik}";
            lblKm.Content = $"Prevoženi km: {selected.KilometriString}";
            lblGorivo.Content = $"Gorivo: {selected.Gorivo}";

            try
            {
                var uri = new Uri(selected.Slika, UriKind.RelativeOrAbsolute);
                imgVozilo.Source = new BitmapImage(uri);
            }
            catch
            {
                
            }
        }
    }
}
