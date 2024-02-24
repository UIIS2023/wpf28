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
    /// Interaction logic for frmZanr.xaml
    /// </summary>
    public partial class frmZanr : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        bool azuriraj;
        DataRowView pomocniRed;

        public frmZanr()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtNaziv.Focus();
        }

        public frmZanr(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
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
                cmd.Parameters.Add("@NazivZanra", System.Data.SqlDbType.NVarChar).Value = txtNaziv.Text;
                

                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update tbl_Zanr
                                          set NazivZanra=@NazivZanra  where ZanrID=@ID";
                }
                else
                {
                    cmd.CommandText = @"Insert into tbl_Zanr(NazivZanra) 
                                    values(@NazivZanra) ";
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

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
