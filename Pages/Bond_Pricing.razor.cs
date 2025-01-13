using Microsoft.AspNetCore.Components;
using System;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace MagicNumber.Pages
{
    public class BondPricing : ComponentBase
    {
        protected int FV { get; set; } // Valeur nominale de l'obligation
        protected double Yield { get; set; } // Taux d'intérêt en pourcentage
        protected double rate { get; set; } // Taux d'intérêt en pourcentage

        protected int Maturity { get; set; } // Durée de l'obligation en années
        protected string? Result { get; set; } // Résultat du calcul du prix de l'obligation

        protected double final_price { get; set; }
        protected double macDuration { get; set; }
        protected double macDuration1 { get; set; }
        protected double modifiedDuration { get; set; }


        protected bool? state { get; set; } // Résultat du calcul du prix de l'obligation



        private List<int> label = new List<int>();
        private List<double> flux = new List<double>();
        private List<double> flux_duration = new List<double>();



        protected override void OnInitialized()
        {
            // Initialiser les valeurs par défaut
            ReinitGame();

            base.OnInitialized();

        }
        protected void ReinitGame()
        {
            FV = 1000;
            Yield = 2;
            rate = 4;
            Maturity = 10;
            Result = "Entrez les valeurs pour calculer le prix.";

        }


        protected void UpdateResult()
        {

            state = true;
            label.Clear();
            flux.Clear();
            // Calculer le prix de l'obligation
            double price=0;
            double coupon = FV * (rate / 100);
            if (Maturity < 101)
            {


                for (int i = 1; i <= Maturity; i++)
                {

                    if (i == Maturity)
                    {
                        // Ajouter le dernier paiement (valeur nominale + coupon)
                        price += (FV + coupon) / Math.Pow(1 + Yield / 100, Maturity);
                        flux.Add(Math.Round((FV + coupon) / Math.Pow(1 + Yield / 100, Maturity), 2));
                        label.Add(i);
                        Result = $"Le prix de l'obligation est : {price.ToString()}"; // Formatage en 2 décimales


                    }
                    else
                    {
                        // Ajouter les coupons
                        price += coupon / Math.Pow(1 + Yield / 100, i);
                        flux.Add(Math.Round(coupon / Math.Pow(1 + Yield / 100, i), 2));
                        label.Add(i);


                    }
                }
            }
            else
            {

            }

            // Mettre à jour le résultat affiché
            Result = $"{price.ToString()}"; // Formatage en 2 décimales
            final_price = price;




        }
        // Calcul de la Macauley Duration
        public double CalculateMacauleyDuration()
        {
            if (final_price == 0)
            {
                throw new InvalidOperationException("Le prix de l'obligation doit être calculé avant la duration.");
            }

            double coupon = FV * (rate / 100);
            double macDuration = 0;

            for (int i = 1; i <= Maturity; i++)
            {
                if (i == Maturity)
                {
                    double cashFlow = FV + coupon;
                    macDuration += i * (cashFlow / Math.Pow(1 + Yield / 100, i));
                    flux_duration.Add(((FV + coupon) / Math.Pow(1 + Yield / 100, i)));
                }
                else
                {
                    double cashFlow = coupon;
                    macDuration += i * (cashFlow / Math.Pow(1 + Yield / 100, i));
                }
            }

            macDuration /= final_price; // Diviser par le prix de l'obligation
            return macDuration;
        }

        public double CalculateConvexity()
        {
            if (final_price == 0)
            {
                throw new InvalidOperationException("Le prix de l'obligation doit être calculé avant la convexité.");
            }

            double coupon = FV * (rate / 100);
            double convexity = 0;

            // Calcul de la somme pondérée des flux de trésorerie pour la convexité
            for (int i = 1; i <= Maturity; i++)
            {
                if (i == Maturity)
                {
                    double cashFlow = FV + coupon; // Dernier paiement (valeur nominale + coupon)
                    convexity += (cashFlow * i * (i + 1)) / Math.Pow(1 + Yield / 100, i);
                }
                else
                {
                    double cashFlow = coupon; // Coupons périodiques
                    convexity += (cashFlow * i * (i + 1)) / Math.Pow(1 + Yield / 100, i);
                }
            }

            // Diviser par le prix et ajuster l'échelle
            convexity /= (final_price * Math.Pow(1 + Yield / 100, 2)); // Facteur d'échelle

            return convexity;
        }

        // Calcul de la Duration Modifiée
        public double CalculateModifiedDuration()
        {
            double macDuration = CalculateMacauleyDuration();

            modifiedDuration = macDuration / (1 + Yield / 100);
            return modifiedDuration;
        }
        public List<int> GetLabels()
        {
            return label ?? new List<int>(); // Retourne une liste vide si labels est null
        }


        public List<double> GetData()
        {
            return flux ?? new List<double>(); // Retourne une liste vide si labels est null
        }
        public List<double> GetDataDuration()
        {
            return flux_duration ?? new List<double>(); // Retourne une liste vide si labels est null
        }

        public double GetmacDuration()
        {
            UpdateResult();
            double macDuration = CalculateMacauleyDuration();


            return Math.Round(macDuration, 2); // Retourne une liste vide si labels est null
        }

        public double GetModifiedDuration()
        {

            CalculateModifiedDuration();

            return Math.Round(modifiedDuration, 2); // Retourne une liste vide si labels est null
        }

        public double GetConvexity()
        {
            UpdateResult();
            double convexity = CalculateConvexity();


            return Math.Round(convexity, 2); // Retourne une liste vide si labels est null
        }
        public double GetPrice()
        {
            return Math.Round(final_price, 2);
        }


    }
}
