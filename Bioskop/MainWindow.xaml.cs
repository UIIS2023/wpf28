using Bioskop.Forme;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bioskop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Select upiti
        string kupciSelect = @"Select KupacID as ID,Ime,Prezime,Kontakt from tbl_Kupac";
        string radniciSelect = @"Select RadnikID as ID,Ime,Prezime,Kontakt from tbl_Radnik";
        string zanrSelect = @"Select ZanrID as ID,NazivZanra as 'Naziv zanra' from tbl_Zanr";
        string saleSelect = @"Select SalaID as ID,BrojSale as 'Broj sale', Ime as 'Ime filma'
                       from tbl_Sala
                            join tbl_Film on tbl_Sala.FilmID=tbl_Film.FilmID ";
        string hranaPiceSelect = @"Select HranaPiceID as ID, Naziv,Cena, Ime + ' ' + Prezime as Radnik 
                        from tbl_HranaPice
                            join tbl_Radnik on tbl_HranaPice.RadnikID=tbl_Radnik.RadnikID";
        string transakcijeSelect = @"Select TransakcijaID as ID,DatumTransakcije as 'Datum transakcije', KolicinaProizvoda as 'Kolicina proizvoda',
                                   tbl_Radnik.Ime + ' ' + tbl_Radnik.Prezime as Radnik, tbl_Kupac.Ime + ' ' + tbl_Kupac.Prezime as Kupac
                        from tbl_Transakcija
                             join tbl_Radnik on tbl_Transakcija.RadnikID=tbl_Radnik.RadnikID
                             join tbl_Kupac on tbl_Transakcija.KupacID=tbl_Kupac.KupacID ";
        string karteSelect = @"Select KartaID as ID, Cena, DatumProjekcije as 'Datum projekcije', BrojSale as 'Broj sale', 
                               KolicinaProizvoda as 'Kolicina proizvoda'
                        from tbl_Karta
	                          join tbl_Sala on tbl_Karta.SalaID=tbl_Sala.SalaID
	                          join tbl_Transakcija on tbl_Karta.TransakcijaID=tbl_Transakcija.TransakcijaID";
        string filmoviSelect = @"Select FilmID as ID,Ime,NazivZanra as 'Zanr',Godina,Trajanje
                        from tbl_Film
                              join tbl_Zanr on tbl_Film.ZanrID=tbl_Zanr.ZanrID";

        #endregion

        #region Delete upiti
        string kupciDelete = @"delete from tbl_Kupac where KupacID=";
        string radniciDelete = @"delete from tbl_Radnik where RadnikID=";
        string zanrDelete = @"delete from tbl_Zanr where ZanrID=";
        string saleDelete = @"delete from tbl_Sala where SalaID=";
        string hranaPiceDelete = @"delete from tbl_HranaPice where HranaPiceID=";
        string transakcijeDelete = @"delete from tbl_Transakcija where TransakcijaID";
        string karteDelete = @"delete from tbl_Karta where KartaID";
        string filmoviDelete = @"delete from tbl_Film where FilmID";
        #endregion

        #region Select upiti sa uslovom
        string selectUslovKupci= @"Select * from tbl_Kupac where KupacID=";
        string selectUslovRadnici = @"Select * from tbl_Radnik where RadnikID=";
        string selectUslovSale = @"Select * from tbl_Sala where SalaID=";
        string selectUslovZanr = @"Select * from tbl_Zanr where ZanrID=";
        string selectUslovHranaPice = @"Select * from tbl_HranaPice where HranaPiceID=";
        string selectUslovTransakcije = @"Select * from tbl_Transakcija where TransakcijaID=";
        string selectUslovKarte = @"Select * from tbl_Karta where KartaID=";
        string selectUslovFilmovi = @"Select * from tbl_Film where FilmID=";
        #endregion

        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        bool azuriraj;
        DataRowView pomocniRed;
        string ucitanaTabela;
        public MainWindow()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            UcitajPodatke(dataGridCentralni, filmoviSelect);


        }

        void UcitajPodatke(DataGrid grid, string selectUpit) 
        {
            try
            {
                konekcija.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectUpit, konekcija); 
                DataTable dataTable = new DataTable(); 

                ucitanaTabela = selectUpit;
                
                dataAdapter.Fill(dataTable);
                if(grid != null)
                {
                    grid.ItemsSource = dataTable.DefaultView; 
                }
                dataTable.Dispose();
                dataAdapter.Dispose();


            }
            catch (SqlException)
            {
                MessageBox.Show("Neuspesno ucitani podaci!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }

        }

       

        private void btnFilm_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, filmoviSelect);
        }

        private void btnKarta_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, karteSelect);
        }

        private void btnRadnik_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, radniciSelect);
        }
        private void btnKupac_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, kupciSelect);
        }

        private void btnTransakcija_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, transakcijeSelect);
        }

        private void btnHranaPice_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, hranaPiceSelect);
        }

        private void btnSala_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, saleSelect);
        }

        private void btnZanr_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, zanrSelect);
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Window prozor;
            if (ucitanaTabela.Equals(kupciSelect))
            {
                prozor = new frmKupac(); 
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, kupciSelect); 
            }
            else if (ucitanaTabela.Equals(filmoviSelect))
            {
                prozor = new frmFilm();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, filmoviSelect);
            }
            else if (ucitanaTabela.Equals(karteSelect))
            {
                prozor = new frmKarta();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, karteSelect);
            }
            else if (ucitanaTabela.Equals(radniciSelect))
            {
                prozor = new frmRadnik();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, radniciSelect);
            }
            else if (ucitanaTabela.Equals(transakcijeSelect))
            {
                prozor = new frmTransakcija();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, transakcijeSelect);
            }
            else if (ucitanaTabela.Equals(hranaPiceSelect))
            {
                prozor = new frmHranaPice();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, hranaPiceSelect);
            }
            else if (ucitanaTabela.Equals(saleSelect))
            {
                prozor = new Sala();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, saleSelect);
            }
            else if (ucitanaTabela.Equals(zanrSelect))
            {
                prozor = new frmZanr();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, zanrSelect);
            }
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
          
            if (ucitanaTabela.Equals(kupciSelect))
            {
                ObrisiZapis(dataGridCentralni, kupciDelete); 
                UcitajPodatke(dataGridCentralni, kupciSelect); 

            }
            else if (ucitanaTabela.Equals(radniciSelect)) 
            {
                ObrisiZapis(dataGridCentralni, radniciDelete);
                UcitajPodatke(dataGridCentralni, radniciSelect);
            }
            else if (ucitanaTabela.Equals(zanrSelect))
            {
                ObrisiZapis(dataGridCentralni, zanrDelete);
                UcitajPodatke(dataGridCentralni, zanrSelect);
            }
            else if (ucitanaTabela.Equals(saleSelect))
            {
                ObrisiZapis(dataGridCentralni, saleDelete);
                UcitajPodatke(dataGridCentralni, saleSelect);
            }
            else if (ucitanaTabela.Equals(hranaPiceSelect))
            {
                ObrisiZapis(dataGridCentralni, hranaPiceDelete);
                UcitajPodatke(dataGridCentralni, hranaPiceSelect);
            }
            else if (ucitanaTabela.Equals(transakcijeSelect))
            {
                ObrisiZapis(dataGridCentralni, transakcijeDelete);
                UcitajPodatke(dataGridCentralni, transakcijeSelect);
            }
            else if (ucitanaTabela.Equals(karteSelect))
            {
                ObrisiZapis(dataGridCentralni, karteDelete);
                UcitajPodatke(dataGridCentralni, karteSelect);
            }
            else if (ucitanaTabela.Equals(filmoviSelect))
            {
                ObrisiZapis(dataGridCentralni, filmoviDelete);
                UcitajPodatke(dataGridCentralni, filmoviSelect);
            }
        }

        private void ObrisiZapis(DataGrid grid, object deleteUpit)
        {
            try
            {
                konekcija.Open();
                DataRowView red = (DataRowView)grid.SelectedItems[0];    
                MessageBoxResult rezultat = MessageBox.Show("Da li ste sigurni?","Pitanje", MessageBoxButton.YesNo,MessageBoxImage.Question);
                if(rezultat == MessageBoxResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = konekcija
                    };
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = red["ID"]; 
                    cmd.CommandText = deleteUpit + "@ID";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red", "Obavestenje", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SqlException)
            {
                MessageBox.Show("Postoje povezani podaci u drugim tabelama", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(kupciSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovKupci);
                UcitajPodatke(dataGridCentralni, kupciSelect);
            }
            else if (ucitanaTabela.Equals(radniciSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovRadnici);
                UcitajPodatke(dataGridCentralni, radniciSelect);
            }
            else if (ucitanaTabela.Equals(saleSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovSale);
                UcitajPodatke(dataGridCentralni, saleSelect);
            }
            else if (ucitanaTabela.Equals(zanrSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovZanr);
                UcitajPodatke(dataGridCentralni, zanrSelect);
            }
            else if (ucitanaTabela.Equals(hranaPiceSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovHranaPice);
                UcitajPodatke(dataGridCentralni, hranaPiceSelect);
            }
            else if (ucitanaTabela.Equals(transakcijeSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovTransakcije);
                UcitajPodatke(dataGridCentralni, transakcijeSelect);
            }
            else if (ucitanaTabela.Equals(karteSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovKarte);
                UcitajPodatke(dataGridCentralni, karteSelect);
            }
            else if (ucitanaTabela.Equals(filmoviSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovFilmovi);
                UcitajPodatke(dataGridCentralni, filmoviSelect);
            }
        }

       private void PopuniFormu(DataGrid grid, object selectUslov) 
        {
           
            
            try
            {
                konekcija.Open();
                azuriraj = true;
                DataRowView red = (DataRowView)grid.SelectedItems[0];
                pomocniRed = red;
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = red["ID"]; 
                cmd.CommandText = selectUslov + "@ID";
                SqlDataReader citac =cmd.ExecuteReader();
                cmd.Dispose();
                while(citac.Read())
                {
                    if (ucitanaTabela.Equals(kupciSelect))
                    {
                        frmKupac prozorKupac = new frmKupac(azuriraj, pomocniRed);
                        prozorKupac.txtIme.Text = citac["Ime"].ToString();
                        prozorKupac.txtPrezime.Text = citac["Prezime"].ToString();
                        prozorKupac.txtKontakt.Text = citac["Kontakt"].ToString();
                        prozorKupac.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(radniciSelect))
                    {
                        frmRadnik prozorRadnik = new frmRadnik(azuriraj, pomocniRed);
                        prozorRadnik.txtIme.Text = citac["Ime"].ToString();
                        prozorRadnik.txtPrezime.Text = citac["Prezime"].ToString();
                        prozorRadnik.txtKontakt.Text = citac["Kontakt"].ToString();
                        prozorRadnik.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(saleSelect))
                    {
                        Forme.Sala prozorSala = new Sala(azuriraj, pomocniRed);
                        prozorSala.txtBrojSale.Text = citac["BrojSale"].ToString();
                        prozorSala.cbFilm.SelectedValue = citac["FilmID"].ToString();
                        prozorSala.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(zanrSelect))
                    {
                        Forme.frmZanr prozorZanr = new frmZanr(azuriraj, pomocniRed);
                        prozorZanr.txtNaziv.Text = citac["NazivZanra"].ToString();
                        prozorZanr.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(hranaPiceSelect))
                    {
                        Forme.frmHranaPice prozorHranaPice = new frmHranaPice(azuriraj, pomocniRed);
                        prozorHranaPice.txtNaziv.Text = citac["Naziv"].ToString();
                        prozorHranaPice.txtCena.Text = citac["Cena"].ToString();
                        prozorHranaPice.cbRadnik.SelectedValue = citac["RadnikID"].ToString(); 
                        prozorHranaPice.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(transakcijeSelect))
                    {
                        Forme.frmTransakcija prozorTransakcija = new frmTransakcija(azuriraj, pomocniRed);
                        prozorTransakcija.txtKolicina.Text = citac["KolicinaProizvoda"].ToString();
                        prozorTransakcija.cbKupac.SelectedValue = citac["KupacID"].ToString();
                        prozorTransakcija.cbRadnik.SelectedValue = citac["RadnikID"].ToString();
                        prozorTransakcija.dtDatum.SelectedDate = (DateTime)citac["DatumTransakcije"];
                        prozorTransakcija.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(karteSelect))
                    {
                        Forme.frmKarta prozorKarta = new frmKarta(azuriraj, pomocniRed);
                        prozorKarta.cbSala.SelectedValue =citac["SalaID"].ToString();
                        prozorKarta.cbTransakcija.SelectedValue =citac["TransakcijaID"].ToString();
                        prozorKarta.dpDatum.SelectedDate = (DateTime)citac["DatumProjekcije"];
                        prozorKarta.txtCena.Text = citac["Cena"].ToString();
                        prozorKarta.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(filmoviSelect))
                    {
                        Forme.frmFilm prozorFilm = new frmFilm(azuriraj, pomocniRed);
                        prozorFilm.txtIme.Text = citac["Ime"].ToString();
                        prozorFilm.txtGodina.Text = citac["Godina"].ToString();
                        prozorFilm.txtTrajanje.Text = citac["Trajanje"].ToString();
                        prozorFilm.cbZanr.SelectedValue = citac["ZanrID"].ToString();
                        prozorFilm.ShowDialog();
                    }
                    
                   
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red", "Greska",MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
                azuriraj = false;
            }
        }
    }
}
