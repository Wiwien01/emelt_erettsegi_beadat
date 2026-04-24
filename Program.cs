using System;
using System.Collections.Generic;
using System.IO;

namespace erettsegi
{
    class Program
    {
        static void Main()
        {
            List<string> rendszam = new List<string>();
            List<int> ora = new List<int>();
            List<int> perc = new List<int>();
            List<int> sebess = new List<int>();

            string[] sorok = File.ReadAllLines("jeladas.txt");
            for (int i = 0; i < sorok.Length; i++)
            {
                string[] s = sorok[i].Split('\t');
                rendszam.Add(s[0]);
                ora.Add(int.Parse(s[1]));
                perc.Add(int.Parse(s[2]));
                sebess.Add(int.Parse(s[3]));
            }

            Console.WriteLine("2. feladat");
            int utolso = rendszam.Count - 1;
            Console.WriteLine("Az utolsó jeladás időpontja " + ora[utolso] + ":" + perc[utolso] + ", a jármű rendszáma " + rendszam[utolso]);

            Console.WriteLine("3. feladat");
            string elso_auto = rendszam[0];
            Console.WriteLine("Az első jármű: " + elso_auto);
            Console.Write("Jeladásainak időpontjai:");
            for (int i = 0; i < rendszam.Count; i++)
            {
                if (rendszam[i] == elso_auto)
                {
                    Console.Write(" " + ora[i] + ":" + perc[i]);
                }
            }
            Console.WriteLine();
            Console.WriteLine("4. feladat");
            Console.Write("Kérem, adja meg az órát: ");
            int be_ora = int.Parse(Console.ReadLine());
            Console.Write("Kérem, adja meg a percet: ");
            int be_perc = int.Parse(Console.ReadLine());

            int szamlalo = 0;
            for (int i = 0; i < ora.Count; i++)
            {
                if (ora[i] == be_ora && perc[i] == be_perc)
                {
                    szamlalo++;
                }
            }
            Console.WriteLine("A jeladások száma: " + szamlalo);

            Console.WriteLine("5. feladat");
            int max_seb = 0;
            for (int i = 0; i < sebess.Count; i++)
            {
                if (sebess[i] > max_seb)
                {
                    max_seb = sebess[i];
                }
            }
            Console.WriteLine("A legnagyobb sebesség km/h: " + max_seb);
            Console.Write("A járművek:");
            for (int i = 0; i < sebess.Count; i++)
            {
                if (sebess[i] == max_seb)
                {
                    Console.Write(" " + rendszam[i]);
                }
            }
            Console.WriteLine();
            Console.WriteLine("6. feladat");
            Console.Write("Kérem, adja meg a rendszámot: ");
            string bekert = Console.ReadLine();

            double tav = 0;
            int elozo_index = -1;

            for (int i = 0; i < rendszam.Count; i++)
            {
                if (rendszam[i] == bekert)
                {
                    if (elozo_index == -1)
                    {
                        Console.WriteLine(ora[i] + ":" + perc[i] + " 0,0 km");
                    }
                    else
                    {
                        double p1 = ora[elozo_index] * 60 + perc[elozo_index];
                        double p2 = ora[i] * 60 + perc[i];
                        double idokulonb = (p2 - p1) / 60.0;

                        tav = tav + (idokulonb * sebess[elozo_index]);

                        Console.WriteLine(ora[i] + ":" + perc[i] + " " + tav.ToString("F1") + " km");
                    }
                    elozo_index = i;
                }
            }

            List<string> kulonbozo_rendszamok = new List<string>();
            for (int i = 0; i < rendszam.Count; i++)
            {
                bool mar_benne_van = false;
                for (int j = 0; j < kulonbozo_rendszamok.Count; j++)
                {
                    if (kulonbozo_rendszamok[j] == rendszam[i]) mar_benne_van = true;
                }
                if (mar_benne_van == false) kulonbozo_rendszamok.Add(rendszam[i]);
            }

            StreamWriter ki = new StreamWriter("ido.txt");
            for (int i = 0; i < kulonbozo_rendszamok.Count; i++)
            {
                string aktualis = kulonbozo_rendszamok[i];
                int elso_o = 0, elso_p = 0, utolso_o = 0, utolso_p = 0;
                bool megvolt_az_elso = false;

                for (int j = 0; j < rendszam.Count; j++)
                {
                    if (rendszam[j] == aktualis)
                    {
                        if (megvolt_az_elso == false)
                        {
                            elso_o = ora[j];
                            elso_p = perc[j];
                            megvolt_az_elso = true;
                        }
                        utolso_o = ora[j];
                        utolso_p = perc[j];
                    }
                }
                ki.WriteLine(aktualis + " " + elso_o + " " + elso_p + " " + utolso_o + " " + utolso_p);
            }
            ki.Close();
        }
    }
}