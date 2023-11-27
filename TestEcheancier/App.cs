using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using TestEcheancier.Models;

namespace TestEcheancier
{
    public class App
    {

        public void Run(Arguments arguments)
        {
            Console.Write("Date début : \n");
            Console.Write(string.Concat(arguments.DateDebut.ToShortDateString(),"\n"));
            int choix = 0;
            if (arguments.DateDebut == default || arguments.MontantAnnuel <= 0)
            {
                Console.WriteLine("Erreur: Les arguments requis (Date de debut et Montant Annuel) sont manquants ou incorrects.");
                return;
            }
            if (arguments.DateFin.HasValue)
            {
                choix += 1;
                //
            }
            if (arguments.NBEcheance > 0)
            {
                choix += 2;
                //
            }
            if (arguments.Periodicite > 0)
            {
                choix += 3;
            }
            switch (choix)
            {
                case 3: 
                    CalculerEcheancesParDateDebutNombreEcheanceEtDateFin(arguments.DateDebut, arguments.NBEcheance, arguments.DateFin.Value, arguments.MontantAnnuel);
                    break;
                case 4: 
                    CalculerEcheancesParDateDebutDateFinEtPeriodicite(arguments.DateDebut, arguments.DateFin.Value, arguments.Periodicite, arguments.MontantAnnuel);
                    break;
                case 5: 
                    CalculerEcheancesParDateDebutNombreEcheanceEtPeriodicite(arguments.DateDebut, arguments.NBEcheance, arguments.Periodicite, arguments.MontantAnnuel);
                    break;
                default:
                    Console.WriteLine("Pour procéder à un calcul d'échéancier nous avons besoin des arguments obligatoire et une combinaison valide de deux des autres arguments possible");
                    break;

            }

        }

        static void CalculerEcheancesParDateDebutDateFinEtPeriodicite(DateTime debut, DateTime fin, int periodicite, decimal montantAnnuel)
        {
            int nombreEcheances = (int)Math.Ceiling((fin - debut).Days / (double)(periodicite * 30));
            decimal montantEcheance = montantAnnuel / nombreEcheances;

            for (int i = 0; i < nombreEcheances; i++)
            {
                DateTime debutEcheance = debut.AddMonths(i * periodicite);
                DateTime finEcheance = debutEcheance.AddMonths(periodicite).AddDays(-1);
                AfficherEcheance(debutEcheance, finEcheance, montantEcheance);
            }
        }

        static void CalculerEcheancesParDateDebutNombreEcheanceEtPeriodicite(DateTime debut, int nombreEcheances, int periodicite, decimal montantAnnuel)
        {
            decimal montantEcheance = montantAnnuel / nombreEcheances;

            for (int i = 0; i < nombreEcheances; i++)
            {
                DateTime debutEcheance = debut.AddMonths(i * periodicite);
                DateTime finEcheance = debutEcheance.AddMonths(periodicite).AddDays(-1);
                AfficherEcheance(debutEcheance, finEcheance, montantEcheance);
            }
        }
        static void CalculerEcheancesParDateDebutNombreEcheanceEtDateFin(DateTime debut, int nombreEcheances, DateTime fin, decimal montantAnnuel)
        {
            int periodicite = (int)Math.Ceiling((fin - debut).Days / (double)(30* nombreEcheances));
            decimal montantEcheance = montantAnnuel / nombreEcheances;

            for (int i = 0; i < nombreEcheances; i++)
            {
                DateTime debutEcheance = debut.AddMonths(i * periodicite);
                DateTime finEcheance = debutEcheance.AddMonths(periodicite).AddDays(-1);
                AfficherEcheance(debutEcheance, finEcheance, montantEcheance);
            }

        }

        static void AfficherEcheance(DateTime debut, DateTime fin, decimal montant)
        {
            Console.WriteLine($"Du {debut:dd/MM/yyyy} au {fin:dd/MM/yyyy} pour un montant de {montant:C}");
        }
    }
}
