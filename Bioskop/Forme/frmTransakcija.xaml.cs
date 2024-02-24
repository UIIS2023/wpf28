using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Bioskop.Forme
{
    /// <summary>
    /// Interaction logic for Transakcija.xaml
    /// </summary>
    public partial class frmTransakcija : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        bool azuriraj;
        DataRowView pomocniRed;

        public frmTransakcija()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniFormu();
        }
        public frmTransakcija(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniFormu();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
        }

        void PopuniFormu() { 

            try
            {
                konekcija.Open();
                string vratiKupce = @"select KupacID,Ime + ' ' + Prezime + ' ' as Kupac from tbl_Kupac";
                SqlDataAdapter daKupce = new SqlDataAdapter(vratiKupce, konekcija);
                DataTable dtKupce = new DataTable();
                daKupce.Fill(dtKupce);
                cbKupac.ItemsSource = dtKupce.DefaultView;
                dtKupce.Dispose();
                daKupce.Dispose();


                string vratiRadnike = @"select RadnikID,Ime + ' ' + Prezime + ' ' as Radnik from tbl_Radnik";
                SqlDataAdapter daRadnike = new SqlDataAdapter(vratiRadnike, konekcija);
                DataTable dtRadnike = new DataTable();
                daRadnike.Fill(dtRadnike);
                cbRadnik.ItemsSource = dtRadnike.DefaultView;
                dtRadnike.Dispose();
                daRadnike.Dispose();

            }
            catch (SqlException)
            {
                MessageBox.Show("Padajuce liste nisu popunjene!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }

        }

     

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
            {
                this.Close();
            }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                konekcija.Open();
                DateTime date = (DateTime)dtDatum.SelectedDate;
                string datum = date.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture);
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@KupacID", System.Data.SqlDbType.Int).Value = cbKupac.SelectedValue;
                cmd.Parameters.Add("@RadnikID", System.Data.SqlDbType.Int).Value = cbRadnik.SelectedValue;
                cmd.Parameters.Add("@DatumTransakcije", System.Data.SqlDbType.DateTime).Value = datum;
                cmd.Parameters.Add("@KolicinaProizvoda", System.Data.SqlDbType.Int).Value = txtKolicina.Text;
                

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update tbl_Transakcija
                                          set KupacID=@KupacID, RadnikID=@RadnikID, DatumTransakcije=@DatumTransakcije,
                                          KolicinaProizvoda=@KolicinaProizvoda where TransakcijaID=@ID";
                }
                else
                {
                    cmd.CommandText = @"Insert into tbl_Transakcija(DatumTransakcije,KolicinaProizvoda,RadnikID,KupacID) 
                                    values(@DatumTransakcije,@KolicinaProizvoda,@RadnikID,@KupacID); ";
                }

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Unos odredjenih vrednosti nije validan!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
            catch (InvalidOperationException)
            {
                MessageBox.Show("Odaberite datum!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }
    }
}
