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
    /// Interaction logic for Sala.xaml
    /// </summary>
    public partial class Sala : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        bool azuriraj;
        DataRowView pomocniRed;

        public Sala()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniFormu();
        }
        public Sala(bool azuriraj, DataRowView pomocniRed)
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
                string vratiFilmovi = @"select FilmID,Ime from tbl_Film";
                SqlDataAdapter daFilmovi= new SqlDataAdapter(vratiFilmovi, konekcija);
                DataTable dtFilmovi = new DataTable();
                daFilmovi.Fill(dtFilmovi);
                cbFilm.ItemsSource = dtFilmovi.DefaultView;
                dtFilmovi.Dispose();
                daFilmovi.Dispose();

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
            txtBrojSale.Focus();
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

                cmd.Parameters.Add("@BrojSale", System.Data.SqlDbType.Int).Value = txtBrojSale.Text;
                cmd.Parameters.Add("@FilmID", System.Data.SqlDbType.Int).Value = cbFilm.SelectedValue;
                

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update tbl_Sala
                                          set BrojSale=@BrojSale,FilmID=@FilmID where SalaID=@ID";
                }
                else
                {
                    cmd.CommandText = @"Insert into tbl_Sala(BrojSale,FilmID) 
                                    values(@BrojSale,@FilmID); ";
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
