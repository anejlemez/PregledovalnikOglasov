using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PregledovalnikOglasov
{
    public class Oglas
    {
        public string Naziv { get; init; } = "";
        public decimal Cena { get; init; }
        public int Letnik { get; init; }
        public int Km { get; init; }
        public string Gorivo { get; init; } = "";
        public string ImagePath { get; init; } = "";

        public string CenaString => Cena == 0 ? "-" : Cena.ToString("N0") + " €";
    }

    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<Oglas> _oglasi = new();
        private ICollectionView _oglasiView;

        public MainWindow()
        {
            InitializeComponent();

            _oglasi.Add(new Oglas { Naziv = "Audi A4 2.0 TDI", Cena = 12900, Letnik = 2018, Km = 145000, Gorivo = "Dizel", ImagePath = "Images/audi.jpg" });
            _oglasi.Add(new Oglas { Naziv = "BMW 320d xDrive", Cena = 15900, Letnik = 2019, Km = 120000, Gorivo = "Dizel", ImagePath = "Images/bmw.jpg" });
            _oglasi.Add(new Oglas { Naziv = "Volkswagen Golf 1.6 TDI", Cena = 8900, Letnik = 2016, Km = 175000, Gorivo = "Dizel", ImagePath = "Images/golf.jpg" });
            _oglasi.Add(new Oglas { Naziv = "Renault Clio 1.2 16V", Cena = 6900, Letnik = 2015, Km = 98000, Gorivo = "Bencin", ImagePath = "Images/clio.jpg" });
            _oglasi.Add(new Oglas { Naziv = "Škoda Octavia 2.0 TDI", Cena = 11900, Letnik = 2017, Km = 130000, Gorivo = "Dizel", ImagePath = "Images/octavia.jpg" });
            _oglasi.Add(new Oglas { Naziv = "Peugeot 308 1.2 PureTech", Cena = 7500, Letnik = 2016, Km = 160000, Gorivo = "Bencin", ImagePath = "Images/peugeot.jpg" });
            _oglasi.Add(new Oglas { Naziv = "Toyota Yaris Hybrid", Cena = 10400, Letnik = 2018, Km = 90000, Gorivo = "Hibrid", ImagePath = "Images/yaris.jpg" });

            // bind to ListView
            lvOglasi.ItemsSource = _oglasi;

            // prepare view for filtering/search
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
                _oglasiView.Filter = null; // show all
            }
            else
            {
                _oglasiView.Filter = item =>
                {
                    if (item is not Oglas o) return false;
                    return o.Naziv?.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0;
                };
            }

            _oglasiView.Refresh();
        }

        private void BtnFiltriraj_Click(object sender, RoutedEventArgs e)
        {
            // implement additional filter logic here (by brand, price, km, year, etc.)
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

            // clear search and reset view
            txtIskanje.Text = "";
            _oglasiView.Filter = null;
            _oglasiView.Refresh();
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
            lblKm.Content = $"Prevoženi km: {selected.Km:N0} km";
            lblGorivo.Content = $"Gorivo: {selected.Gorivo}";

            // set image (use relative resource or absolute path). If resource not found, keep placeholder.
            try
            {
                var uri = new Uri(selected.ImagePath, UriKind.RelativeOrAbsolute);
                imgVozilo.Source = new BitmapImage(uri);
            }
            catch
            {

            }
        }
    }
}