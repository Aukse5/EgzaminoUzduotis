using System;
using System.Diagnostics; // laiko matavimui
using System.Security.Cryptography; // hash šifravimui
using System.Threading.Tasks; // Gijoms
using System.Windows;

namespace Egzaminouzduotis
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void KoduotiIrAtspetiSlaptazodi_Click(object sender, RoutedEventArgs e)
        {
            string ivestasSlaptazodis = SlaptazodzioIvedimoLaukas.Text;
            string Simboliai = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890@#$%^&*";
            if (!string.IsNullOrEmpty(ivestasSlaptazodis))
            {
                Stopwatch laikmatis = Stopwatch.StartNew(); 
                string uzkoduotasSlaptazodis = SlaptazodzioGeneravimas.UzkoduotiSlaptazodi(ivestasSlaptazodis);
                RezultatoTekstas.Text = $"Hash: {uzkoduotasSlaptazodis}";
                if (int.TryParse(MaxGijuLaukas.Text, out int maxGijos) && maxGijos > 0)
                {
                    string atspetasSlaptazodis = BruteForceAtaka.AtspetiSlaptazodi(ivestasSlaptazodis, maxGijos, Simboliai);
                    laikmatis.Stop();
                    if (!string.IsNullOrEmpty(atspetasSlaptazodis))
                    {
                        RezultatoTekstas.Text += $"\nAtspėtas Slaptažodis: {atspetasSlaptazodis}";
                    }
                    else
                    {
                        RezultatoTekstas.Text += "\nNepavyko atspėti slaptažodžio.";
                    }
                    LaikoTekstas.Text += $"\nSlaptažodžio spėjimas užtruko: {laikmatis.ElapsedMilliseconds} ms";
                }
                else
                {
                    RezultatoTekstas.Text += "\nNeteisinga Max Gijų Reikšmė";
                }
            }
            else
            {
                RezultatoTekstas.Text = "Įveskite slaptažodį";
            }
        }
    }
}