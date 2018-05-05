﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace TravelBookApp
{
    public class LoginViewModel
    {
        public MessageDialog Poruka { get; set; }
        public String KorisnickoIme { get; set; }
        public String Sifra { get; set; }

        public bool prijaviAgenciju(String korisnickoIme, String password)
        {
            if (Globalna.nasaAgencija.Agencije.Any(agencija => agencija.NazivAgencije == korisnickoIme && agencija.Sifra == password))
            {
                foreach (Agencija nova in Globalna.nasaAgencija.Agencije)
                {

                    if (nova.NazivAgencije.Equals(korisnickoIme) && nova.Sifra.Equals(password)) Globalna.prijavljenaAgencijaId = nova.Id;
                }
                return true;
            }
            return false;

        }
    }
}