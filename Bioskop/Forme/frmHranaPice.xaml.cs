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
    /// Interaction logic for frmHranaPice.xaml
    /// </summary>
    public partial class frmHranaPice : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        bool azuriraj;
        DataRowView pomocniRed;

        public frmHranaPice()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniFormu();
        }
        public frmHranaPice(bool azuriraj, DataRowView pomocniRed)
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
            txtNaziv.Focus();
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

                cmd.Parameters.Add("@Naziv", System.Data.SqlDbType.NVarChar).Value = txtNaziv.Text;
                cmd.Parameters.Add("@Cena", System.Data.SqlDbType.Int).Value = txtCena.Text;
               
                cmd.Parameters.Add("@RadnikID", System.Data.SqlDbType.Int).Value = cbRadnik.SelectedValue;
                

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update tbl_HranaPice
                                          set Naziv=@Naziv,Cena=@Cena,RadnikID=@RadnikID where HranaPiceID=@ID";
                }
                else
                {
                    cmd.CommandText = @"Insert into tbl_HranaPice(Naziv,Cena,RadnikID) 
                                    values(@Naziv,@Cena,@RadnikID); ";
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
