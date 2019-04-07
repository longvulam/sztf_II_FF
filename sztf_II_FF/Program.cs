using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sztf_II_FF.Exceptions;
using sztf_II_FF.Futarok;
using sztf_II_FF.Kuldemenyek;

namespace sztf_II_FF
{
    class FileReader
    {
        public static List<FutarBase> FutarokBeolvass()
        {
            List<FutarBase> lista = new List<FutarBase>();
            StreamReader sr = new StreamReader("InputFiles/futarok.txt");
            int azon = 1;
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine().Split(' ');
                //jarmu szabadsagonVanE SzallitasiKapacitas
                string jarmuStr = line[0];
                var jarmuEnum = (Jarmu)Enum.Parse(typeof(Jarmu), jarmuStr);

                FutarBase jarmu = null;
                switch (jarmuEnum)
                {
                    case Jarmu.Bicikli:
                        jarmu = new BiciklisFutar();
                        break;
                    case Jarmu.Motorkerekpar:
                        jarmu = new MotorosFutar();
                        break;
                    case Jarmu.Szemelygepkocsi:
                        jarmu = new KocsiFutar();
                        break;
                    case Jarmu.Tehergepkocsi:
                        jarmu = new TeherKocsisFutar();
                        break;
                    default:
                        throw new NincsIlyenJarmuException($"Nem létezik ilyen Jármű: {jarmuStr}");
                }

                lista.Add(jarmu);

                jarmu.Id = azon++;
                jarmu.Jarmu = jarmuEnum;
                jarmu.Dolgozik = line[1] != "van";
                jarmu.SzallitasiKapacitas = int.Parse(line[2]);
            }

            return lista;
        }

        public static void KuldemenyekBeolvass(LancoltLista<IKuldemeny> lista)
        {
            StreamReader sr = new StreamReader("InputFiles/kuldemenyek.txt");
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine().Split(' ');
                //
            }
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            var elemek = new LancoltLista<IKuldemeny>();
            FileReader.KuldemenyekBeolvass(elemek);


            List<FutarBase> futarok = FileReader.FutarokBeolvass();

            Console.WriteLine(string.Join("\n", futarok));

            //elemek.Bejaras();


        }
    }
}
