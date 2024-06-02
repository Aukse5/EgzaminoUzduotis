using System;
using System.Threading.Tasks; //gijoms

namespace Egzaminouzduotis
{
    internal class BruteForceAtaka
    {
        private static bool arSlaptazodisRastas = false; // Kintamasis, skirtas žymėti, ar slaptažodis jau rastas

        public static string AtspetiSlaptazodi(string ivestasSlaptazodis, int maxGijos, string simboliai)
        {
            string rezultatas = null;

            for (int ilgis = 1; ilgis <= ivestasSlaptazodis.Length; ilgis++) // Generuoja derinius nuo minimalaus iki maksimalaus įvesto slaptažodžio ilgio
            {
                // Parallel.ForEach naudojamas, kad daug gijų galėtų dirbti vienu metu, ParallelOptions nurodo kiek gijų galima naudoti
                Parallel.ForEach(GeneruotiDerinius(simboliai, ilgis), new ParallelOptions { MaxDegreeOfParallelism = maxGijos }, (derinys, state) =>
                {
                    if (arSlaptazodisRastas) state.Stop();

                    if (derinys == ivestasSlaptazodis)
                    {
                        rezultatas = ivestasSlaptazodis;
                        arSlaptazodisRastas = true;
                        state.Stop();
                    }
                });

                if (rezultatas != null) break; // Jei slaptažodis rastas, baigia vykdyti ciklą
            }

            return rezultatas;
        }

        private static IEnumerable<string> GeneruotiDerinius(string simboliai, int ilgis) // Funkcija generuoja visus galimus slaptažodžio derinius
        {
            if (ilgis <= 0)
            {
                yield return string.Empty;
                yield break;
            }

            var derinioIndeksai = new int[ilgis]; // Masyvas, kuris saugo derinio indeksus
            var derinys = new char[ilgis]; // Masyvas, kuriame saugomas generuojamas slaptažodis
            var simboliuSkaicius = simboliai.Length; // Leidžimų simbolių skaičius

            while (true)
            {
                for (int i = 0; i < ilgis; i++)
                {
                    derinys[i] = simboliai[derinioIndeksai[i]];
                }
                yield return new string(derinys);

                int indeksas = ilgis - 1;
                while (indeksas >= 0 && ++derinioIndeksai[indeksas] == simboliuSkaicius)
                {
                    derinioIndeksai[indeksas] = 0;
                    indeksas--;
                }

                if (indeksas < 0)
                {
                    yield break;
                }
            }
        }
    }
}
