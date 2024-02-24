using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for frmKupac.xaml
    /// </summary>
    public partial class frmKupac : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        bool azuriraj;
        DataRowView pomocniRed;

        public frmKupac()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtIme.Focus();
        }

        public frmKupac(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
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
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@Ime", System.Data.SqlDbType.NVarChar).Value = txtIme.Text;
                cmd.Parameters.Add("@Prezime", System.Data.SqlDbType.NVarChar).Value = txtPrezime.Text;
                cmd.Parameters.Add("@Kontakt", System.Data.SqlDbType.Int).Value = txtKontakt.Text;

                

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update tbl_Kupac
                                          set Ime=@Ime,Prezime=@Prezime,Kontakt=@Kontakt  where KupacID=@ID";
                }
                else
                {
                    cmd.CommandText = @"Insert into tbl_Kupac(Ime,Prezime,Kontakt) 
                                    values(@Ime,@Prezime,@Kontakt) ";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Unos odredjenih vrednosti nije validan!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
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
