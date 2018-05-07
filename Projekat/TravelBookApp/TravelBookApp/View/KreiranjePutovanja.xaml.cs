﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TravelBookApp.Model;
using TravelBookApp.ViewModel;
using Windows.UI.Popups;
using Windows.UI;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelBookApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class KreiranjePutovanja : Page
    {
        static KreiranjePutovanjaViewModel putovanjeVM = new KreiranjePutovanjaViewModel();
        static List<String> naziviDestinacija = new List<string>();
        static List<String> naziviHotela = new List<string>();
        List<String> autobusi = new List<string>();
        List<String> avioKompanije = new List<string>();
        List<String> listaPolaznihLetova = new List<string>();
        List<String> listaPovratnihLetova = new List<string>();
      //  String odabranaDestinacija;
        String odabraniHotel;
        Boolean istaknuto;
      
        //STAVITI DA SU CRNI border-i NEVIDLJIVI PA DA SE AKTIVIRAJU KAD SKONTAM GDJE TREBA IĆI TO

        public KreiranjePutovanja()
        {
            this.InitializeComponent();
            gLet.Visibility = Visibility.Collapsed;
            putovanjeVM.dodajListuBusPrevoza();

            cKontinent.Items.Add("Afrika");
            cKontinent.Items.Add("Antartika");
            cKontinent.Items.Add("Australija");
            cKontinent.Items.Add("Azija");
            cKontinent.Items.Add("Evropa");
            cKontinent.Items.Add("Južna Amerika");
            cKontinent.Items.Add("Sjeverna Amerika");

            //DESTINACIJE
            Destinacija prva = new Destinacija("Sarajevo", "Bosna i Hercegovina", Kontinenti.Evropa);
            Globalna.nasaAgencija.Destinacije.Add(prva);
            putovanjeVM.dodajNoviHotel("Radon Plaza", 600, 30, prva, 150);

            prva = new Destinacija("Zagreb", "Hrvatska", Kontinenti.Evropa);
            Globalna.nasaAgencija.Destinacije.Add(prva);
            prva = new Destinacija("Tokio", "Japan", Kontinenti.Azija);

            Globalna.nasaAgencija.Destinacije.Add(prva);
            putovanjeVM.dodajNoviHotel("Hokaido Hotel", 500, 30, prva, 250);

            prva = new Destinacija("Seul", "Južna Koreja", Kontinenti.Azija);
            Globalna.nasaAgencija.Destinacije.Add(prva);
            prva = new Destinacija("Kairo", "Egipat", Kontinenti.Afrika);
            Globalna.nasaAgencija.Destinacije.Add(prva);
            prva = new Destinacija("Adis Abeba", "Etiopija", Kontinenti.Afrika);
            Globalna.nasaAgencija.Destinacije.Add(prva);
            prva = new Destinacija("Otava", "Kanada", Kontinenti.SjevernaAmerika);
            Globalna.nasaAgencija.Destinacije.Add(prva);
            prva = new Destinacija("Rio de Janeiro", "Brazil", Kontinenti.JuznaAmerika);
            Globalna.nasaAgencija.Destinacije.Add(prva);
            cDestinacije.Items.Clear();
            cHoteli.Items.Clear();
            cPrevoz.Items.Clear();
            gDestinacija.Visibility = Visibility.Collapsed;
            foreach (var dest in Globalna.nasaAgencija.Destinacije)
            {
                cDestinacije.Items.Add(dest.Naziv);
            }
            cDestinacije.Items.Add("Ništa od ponuđenog");
            gHotel.Visibility = Visibility.Collapsed;
            if(naziviDestinacija.Count == 0)
            {
                naziviDestinacija.Add("Nijedna od ponuđenih");
                naziviHotela.Add("Nijedan od ponuđenih");
            }    

            foreach (var pr in Globalna.nasaAgencija.Prevozi)
            {
                if (pr.VrstaPrevoza.Equals(VrstaPrevoza.autobus))
                {
                    autobusi.Add(pr.Ime);                    
                }
            }
        }

        public void popuniKomboBoxove()
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Frame.Navigate(typeof(PocetnaStranica));
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (rAvion.IsChecked == true)
            {
              /*  foreach(String autobus in autobusi)
                cPrevoz.Items.Add(autobus);*/
                gLet.Visibility = Visibility.Visible;
            }
            else
            {
               // cPrevoz.Items.Add(avioKompanije);
                gLet.Visibility = Visibility.Collapsed;
            }
        }

        private void cDestinacije_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string odabrano = cDestinacije.SelectedItem.ToString();
            if (odabrano.Equals("Ništa od ponuđenog")) gDestinacija.Visibility = Visibility.Visible;
           
            List<string> hoteli = putovanjeVM.dajListuHotelaPoDestinaciji(odabrano);
            foreach (var temp in hoteli) cHoteli.Items.Add(temp);
            if (odabrano != "Ništa od ponuđenog")
            {
                cHoteli.Items.Add("Ništa od ponuđenog");
                gDestinacija.Visibility = Visibility.Collapsed;
            }

            /*else
            {
                gDestinacija.Visibility = Visibility.Collapsed;
               // naziviHotela = putovanjeVM.dajListuHotelaPoDestinaciji(naziviDestinacija[cDestinacije.SelectedItem]);
                autobusi = putovanjeVM.dajListuBusevaPoDestinacijiIKapacitetu(naziviDestinacija[cDestinacije.SelectedIndex], Convert.ToInt32(sMax.Value));
                   // avioKompanije
            }*/
            /*  if(cDestinacije.SelectedIndex > -1)
              odabranaDestinacija = naziviDestinacija[cDestinacije.SelectedIndex];*/
        }

        private void cHoteli_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string odabrano = cHoteli.SelectedItem.ToString();
            if (odabrano.Equals("Ništa od ponuđenog")) gHotel.Visibility = Visibility.Visible;
            else gHotel.Visibility = Visibility.Collapsed;

            if (cHoteli.SelectedIndex > -1)
                odabraniHotel = naziviHotela[cHoteli.SelectedIndex];
        }

        //ALMAAAA trebas dodat kad se vidi onaj grid za dodavanje destinacije da se destinacija doda u listu utravelbook isto i za hotel
        //pooooiiiii  -- GOTOVO POZZZZ 
        private void Button_Click_1(object sender, RoutedEventArgs e)
        { //dodat polje za upis opisa putovanja
            Prevoz prevoz;
            string odabranaDestinacija = cDestinacije.SelectedItem.ToString(); 
            string odabraniHotel = cHoteli.SelectedItem.ToString();
            Boolean jelOK = true;
            if (rAutobus.IsChecked == true)
                prevoz = Globalna.nasaAgencija.Prevozi[cPrevoz.SelectedIndex];
            else
                prevoz = null;

            /*VALIDACIJE PRIJE KREIRANJA PUTOVANJA 
             AKO JE UREDU, VRATI jelOK NA TRUE RADI FINALNE PROVJERE*/

            if (sMin.Value <= sMax.Value)
            {
                jelOK = true;
                greska.Visibility = Visibility.Collapsed;
                minBroj.Foreground = new SolidColorBrush(Colors.Black);
                maxBroj.Foreground = new SolidColorBrush(Colors.Black);
            }
            if (dPolaska.Date < dPovratka.Date)
            {
                jelOK = true;
                greska8.Visibility = Visibility.Collapsed;
            }
           
            if (rDA.IsChecked == true || rNE.IsChecked == true)
            {
                
                greska1.Visibility = Visibility.Collapsed;
                jelOK = true;
            }

            if (!String.IsNullOrEmpty(tCijena.Text))
            {
                jelOK = true;
                greska5.Visibility = Visibility.Collapsed;
            }

            if (cPrevoz.SelectedIndex != -1)
            {
                jelOK = true;
                greska4.Visibility = Visibility.Collapsed;
            }

            if (cHoteli.SelectedIndex != -1)
            {
                jelOK = true;
                greska3.Visibility = Visibility.Collapsed;
            }

            if (rAvion.IsChecked == true)
            {
                if (comboBox.SelectedIndex != -1)
                {
                    jelOK = true;
                    greska6.Visibility = Visibility.Collapsed;
                }

                if (comboBox1.SelectedIndex != -1)
                {
                    jelOK = true;
                    greska7.Visibility = Visibility.Collapsed;
                }
            }
            //AKO NIJE UREDU
            if (sMin.Value > sMax.Value)
            {
                jelOK = false;
                greska.Visibility = Visibility.Visible;
                minBroj.Foreground = new SolidColorBrush(Colors.Red);
                maxBroj.Foreground = new SolidColorBrush(Colors.Red);
            }

            if (rDA.IsChecked == false && rNE.IsChecked == false)
            {
                greska1.Visibility = Visibility.Visible; //AKTIVACIJA textBloc-a KOJI PRIKAZUJE GREŠKU, KAO errorProvider
                jelOK = false;
            }

            if (dPolaska.Date > dPovratka.Date)
            {
                jelOK = false;
                greska8.Visibility = Visibility.Visible;
            }

            if (String.IsNullOrEmpty(tCijena.Text))
            {
                jelOK = false;
                greska5.Visibility = Visibility.Visible;
            }

            if (cPrevoz.SelectedIndex == -1)
            {
                jelOK = false;
                greska4.Visibility = Visibility.Visible;
            }

            if (cHoteli.SelectedIndex == -1)
            {
                jelOK = false;
                greska3.Visibility = Visibility.Visible;
            }

            if (rAvion.IsChecked == true)
            {
                if (comboBox.SelectedIndex == -1)
                {
                    jelOK = false;
                    greska6.Visibility = Visibility.Visible;
                }

                if (comboBox1.SelectedIndex == -1)
                {
                    jelOK = false;
                    greska7.Visibility = Visibility.Visible;
                }
            }
            if (!jelOK)
            {
                var dialog = new MessageDialog("Postoje greške. Popravite pa ponovo kreirajte!");
                dialog.ShowAsync();
            }

            Destinacija novaDestinacija = new Destinacija("random", "random", Kontinenti.Evropa);
            if (odabranaDestinacija != ("Ništa od ponuđenog")) novaDestinacija = Globalna.nasaAgencija.Destinacije[cDestinacije.SelectedIndex];
            if (odabranaDestinacija.Equals("Ništa od ponuđenog"))
            {
                Kontinenti kon = new Kontinenti();
                if (cKontinent.SelectedItem.ToString().Equals("Evropa")) kon = Kontinenti.Evropa;
                if (cKontinent.SelectedItem.ToString().Equals("Azija")) kon = Kontinenti.Azija;
                if (cKontinent.SelectedItem.ToString().Equals("Afrika")) kon = Kontinenti.Afrika;
                if (cKontinent.SelectedItem.ToString().Equals("Sjeverna Amerika")) kon = Kontinenti.SjevernaAmerika;
                if (cKontinent.SelectedItem.ToString().Equals("Južna Amerika")) kon = Kontinenti.JuznaAmerika;
                if (cKontinent.SelectedItem.ToString().Equals("Antartika")) kon = Kontinenti.Antartika;
                if (cKontinent.SelectedItem.ToString().Equals("Australija")) kon = Kontinenti.Australija;
                novaDestinacija = new Destinacija(tDestinacija.Text, tDrzava.Text, kon, iSlikaDestinacije);
                putovanjeVM.dodajNovuDestinaciju(tDestinacija.Text, tDrzava.Text, kon, iSlikaDestinacije);
            }

            Hotel noviHotel = Globalna.nasaAgencija.Hoteli[cHoteli.SelectedIndex];
            if (odabraniHotel.Equals("Ništa od ponuđenog"))
            {
                putovanjeVM.dodajNoviHotel(tHotel.Text, 300, Convert.ToInt32(300 - sMax.Value), novaDestinacija, 120, iSlikaHotela);
                Hotel hot = new Hotel(tHotel.Text, 500, Convert.ToInt32(500 - sMax.Value), novaDestinacija, 120, iSlikaHotela);
            }
            if (jelOK)
            {
                putovanjeVM.kreirajPutovanje(dPolaska.Date.Value.Date, dPovratka.Date.Value.Date, Convert.ToInt32(sMin.Value), Convert.ToInt32(sMax.Value), "opis putovanja", istaknuto, Globalna.prijavljenaAgencijaId, novaDestinacija, noviHotel, prevoz, Convert.ToDouble(tCijena.Text));
                var dialog = new MessageDialog("Putovanje uspješno kreirano!");
                dialog.ShowAsync();
               /* cHoteli.Items.Clear(); KRAHIRA SE RADI OVOG, PREBAČENO GORE, VAJDA OK
                cPrevoz.Items.Clear();
                tCijena.Text = string.Empty;
                tDrzava.Text = string.Empty;
                tDestinacija.Text = string.Empty;*/

            } 
        }

        private void bDodajHotel_Click(object sender, RoutedEventArgs e)
        {
            putovanjeVM.dodajNoviHotel(tHotel.Text, Convert.ToInt32(sMax.Value), Convert.ToInt32(sMin.Value), Globalna.nasaAgencija.Destinacije[cDestinacije.SelectedIndex], Convert.ToDouble(100), null);
        }

        //UBAČENA VARIJABLA U button-u
       private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            istaknuto = true;
        }
        
        private void rAutobus_Checked(object sender, RoutedEventArgs e)
        {
            string destinacija = string.Empty;
            string odabrano = cDestinacije.SelectedItem.ToString();
            if (odabrano.Equals("Ništa od ponuđenog")) destinacija = tDestinacija.Text;
            if (odabrano != "Ništa od ponuđenog") destinacija = cDestinacije.SelectedItem.ToString();
            List<string> prevozi = putovanjeVM.dajListuBusevaPoDestinacijiIKapacitetu(destinacija, Convert.ToInt32(sMax.Value));
            foreach (var temp in prevozi) cPrevoz.Items.Add(temp);
            //cPrevoz.Items.Add(avioKompanije);
            gLet.Visibility = Visibility.Collapsed;
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            istaknuto = false;
        }

        private void bDodajDestinaciju_Click_1(object sender, RoutedEventArgs e)
        {
            putovanjeVM.dodajNovuDestinaciju(tDestinacija.Text, "-", Kontinenti.Afrika, null); //kako sliku spasit?

        }

        
        private async void bUcitajHotel_Click(object sender, RoutedEventArgs e)
        {
           
                var fileOpenPicker = new FileOpenPicker();
                fileOpenPicker.ViewMode = PickerViewMode.Thumbnail;
                fileOpenPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                fileOpenPicker.FileTypeFilter.Add(".png");
                fileOpenPicker.FileTypeFilter.Add(".jpg");
                fileOpenPicker.FileTypeFilter.Add(".jpeg");
                fileOpenPicker.FileTypeFilter.Add(".bmp");

                var storageFile = await fileOpenPicker.PickSingleFileAsync();

                if (storageFile != null)
                {
                   
                    using (IRandomAccessStream fileStream = await storageFile.OpenAsync(Windows.Storage.FileAccessMode.Read))
                    {
                       
                        BitmapImage bitmapImage = new BitmapImage();

                        await bitmapImage.SetSourceAsync(fileStream);
                        iSlikaHotela.Visibility = Visibility.Visible;
                        iSlikaHotela.Source = bitmapImage;
                    }
                }
            
        }

        private void cPrevoz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void sMax_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            
           
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private async void bUcitajSliku_Click(object sender, RoutedEventArgs e)
        {
            var fileOpenPicker = new FileOpenPicker();
            fileOpenPicker.ViewMode = PickerViewMode.Thumbnail;
            fileOpenPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            fileOpenPicker.FileTypeFilter.Add(".png");
            fileOpenPicker.FileTypeFilter.Add(".jpg");
            fileOpenPicker.FileTypeFilter.Add(".jpeg");
            fileOpenPicker.FileTypeFilter.Add(".bmp");

            var storageFile = await fileOpenPicker.PickSingleFileAsync();

            if (storageFile != null)
            {
                
                using (IRandomAccessStream fileStream = await storageFile.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    
                    BitmapImage bitmapImage = new BitmapImage();

                    await bitmapImage.SetSourceAsync(fileStream);
                    iSlikaDestinacije.Visibility = Visibility.Visible;
                    iSlikaDestinacije.Source = bitmapImage;

                }
            }
        }

        private void tDestinacija_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void cKontinent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string destinacija = tDestinacija.Text;
            List<string> hoteli = putovanjeVM.dajListuHotelaPoDestinaciji(destinacija);
            foreach (var temp in hoteli) cHoteli.Items.Add(temp);
            cHoteli.Items.Add("Ništa od ponuđenog");

        }
    }
}
