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
    /// Interaction logic for frmFilm.xaml
    /// </summary>
    public partial class frmFilm : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        bool azuriraj;
        DataRowView pomocniRed;
        public frmFilm()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniFormu();
        }
        public frmFilm(bool azuriraj, DataRowView pomocniRed)
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
                string vratiZanr = @"select ZanrID,NazivZanra from tbl_Zanr";
                SqlDataAdapter daZanr = new SqlDataAdapter(vratiZanr, konekcija);
                DataTable dtZanr = new DataTable();
                daZanr.Fill(dtZanr);
                cbZanr.ItemsSource = dtZanr.DefaultView;
                dtZanr.Dispose();
                daZanr.Dispose();

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
            txtIme.Focus();
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
                cmd.Parameters.Add("@Godina", System.Data.SqlDbType.Int).Value = txtGodina.Text;
                cmd.Parameters.Add("@Trajanje", System.Data.SqlDbType.Int).Value = txtTrajanje.Text;
                cmd.Parameters.Add("@ZanrID", System.Data.SqlDbType.Int).Value = cbZanr.SelectedValue;
                

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update tbl_Film
                                          set Ime=@Ime,Godina=@Godina,Trajanje=@Trajanje,ZanrID=@ZanrID where FilmID=@ID";
                }
                else
                {
                    cmd.CommandText = @"Insert into tbl_Film(Ime,Godina,Trajanje,ZanrID) 
                                    values(@Ime,@Godina,@Trajanje,@ZanrID); ";
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
