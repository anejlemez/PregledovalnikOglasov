using System.ComponentModel;

namespace PregledovalnikOglasov
{
    public class Oglas : INotifyPropertyChanged
    {
        
        private string _naziv = string.Empty;
        private decimal _cena;
        private string _slika = string.Empty;
        private string _znamka = string.Empty;
        private string _model = string.Empty;
        private int _letnik;
        private int _kilometri;
        private string _gorivo = string.Empty;
        private string _menjalnik = string.Empty;

       
        public string Naziv
        {
            get => _naziv;
            set { _naziv = value; OnPropertyChanged(nameof(Naziv)); }
        }

        public decimal Cena
        {
            get => _cena;
            set
            {
                _cena = value;
                OnPropertyChanged(nameof(Cena));
                OnPropertyChanged(nameof(CenaString));
                OnPropertyChanged(nameof(JeCenovnoUgoden));
            }
        }

        public string Slika
        {
            get => _slika;
            set { _slika = value; OnPropertyChanged(nameof(Slika)); }
        }

        public string Znamka
        {
            get => _znamka;
            set { _znamka = value; OnPropertyChanged(nameof(Znamka)); }
        }

        public string Model
        {
            get => _model;
            set { _model = value; OnPropertyChanged(nameof(Model)); }
        }

        public int Letnik
        {
            get => _letnik;
            set { _letnik = value; OnPropertyChanged(nameof(Letnik)); }
        }

        public int Kilometri
        {
            get => _kilometri;
            set
            {
                _kilometri = value;
                OnPropertyChanged(nameof(Kilometri));
                OnPropertyChanged(nameof(KilometriString));
            }
        }

        public string Gorivo
        {
            get => _gorivo;
            set { _gorivo = value; OnPropertyChanged(nameof(Gorivo)); }
        }

        public string Menjalnik
        {
            get => _menjalnik;
            set { _menjalnik = value; OnPropertyChanged(nameof(Menjalnik)); }
        }

       
        public string CenaString => Cena <= 0 ? "-" : $"{Cena:N0} €";
        public string KilometriString => Kilometri <= 0 ? "-" : $"{Kilometri:N0} km";

       
        public bool JeCenovnoUgoden => Cena < 10000m;

        
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
