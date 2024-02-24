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
    /// Interaction logic for frmKarta.xaml
    /// </summary>
    public partial class frmKarta : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        bool azuriraj;
        DataRowView pomocniRed;

        public frmKarta()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniFormu();
        }

        public frmKarta(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniFormu();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
        }

        void PopuniFormu() 
        { 
            try
            {
                konekcija.Open();
                string vratiSale = @"select SalaID,BrojSale from tbl_Sala";
                SqlDataAdapter daSale = new SqlDataAdapter(vratiSale, konekcija);
                DataTable dtSale = new DataTable();
                daSale.Fill(dtSale);
                cbSala.ItemsSource = dtSale.DefaultView;
                dtSale.Dispose();
                daSale.Dispose();

                string vratiTransakcija = @"select TransakcijaID,KolicinaProizvoda from tbl_Transakcija";
                SqlDataAdapter daTransakcija = new SqlDataAdapter(vratiTransakcija, konekcija);
                DataTable dtTransakcija = new DataTable();
                daTransakcija.Fill(dtTransakcija);
                cbTransakcija.ItemsSource = dtTransakcija.DefaultView;
                dtTransakcija.Dispose();
                daTransakcija.Dispose();

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
            txtCena.Focus();
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
                DateTime date = (DateTime)dpDatum.SelectedDate;
                string datum = date.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture);
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@SalaID", System.Data.SqlDbType.Int).Value = cbSala.SelectedValue;
                cmd.Parameters.Add("@TransakcijaID", System.Data.SqlDbType.Int).Value = cbTransakcija.SelectedValue;
                cmd.Parameters.Add("@DatumProjekcije", System.Data.SqlDbType.DateTime).Value = datum;
                cmd.Parameters.Add("@Cena", System.Data.SqlDbType.Int).Value = txtCena.Text;
                
                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update tbl_Karta
                                   set SalaID=@SalaID, TransakcijaID=@TransakcijaID, DatumProjekcije=@DatumProjekcije,Cena=@Cena where KartaID=@ID";
                }
                else
                {
                    cmd.CommandText = @"Insert into tbl_Karta(SalaID,TransakcijaID,DatumProjekcije,Cena) 
                                    values(@SalaID,@TransakcijaID,@DatumProjekcije,@Cena); ";
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
