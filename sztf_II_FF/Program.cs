using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sztf_II_FF.Futarok;
using sztf_II_FF.Kivetelek;
using sztf_II_FF.Kuldemenyek;

namespace sztf_II_FF
{
    class FileReader
    {
        public static List<FutarBase> ReadCarriers()
        {
            List<FutarBase> lista = new List<FutarBase>();
            StreamReader sr = new StreamReader("InputFiles/futarok.txt");
            int azon = 1;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (line == null) continue;

                var parts = line.Split('-');
                //jarmu szabadsagonVanE SzallitasiKapacitas
                string jarmuStr = parts[0];
                var jarmuEnum = (Jarmu)Enum.Parse(typeof(Jarmu), jarmuStr);

                FutarBase jarmu;
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

                jarmu.Id = azon;
                jarmu.Dolgozik = parts[1] != "van";
                jarmu.SzallitasiKapacitas = int.Parse(parts[2]);
            }
            return lista;
        }

        public static LancoltLista<IKuldemeny> ReadPackages()
        {
            LancoltLista<IKuldemeny> lista = new LancoltLista<IKuldemeny>();
            StreamReader sr = new StreamReader("InputFiles/kuldemenyek.txt");
            int azon = 1;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (line == null) continue;

                var parts = line.Split('-');
                //Tipus Prioritas Tomeg
                string tipus = parts[0];
                int prio = int.Parse(parts[1]);
                int tomeg = int.Parse(parts[2]);

                KuldemenyBase kuldemeny;
                switch (tipus)
                {
                    case nameof(NormalLevel):
                        kuldemeny = new NormalLevel(prio, tomeg);
                        break;
                    case nameof(ElsobbsegiLevel):
                        kuldemeny = new ElsobbsegiLevel(prio, tomeg);
                        break;
                    case nameof(NormalCsomag):
                        kuldemeny = new NormalCsomag(prio, tomeg);
                        break;
                    case nameof(ElsobbsegiCsomag):
                        kuldemeny = new ElsobbsegiCsomag(prio, tomeg);
                        break;
                    default:
                        throw new NincsIlyenKuldemenyException($"Nem létezik ilyen Küldemény: {tipus}");
                }

                kuldemeny.Id = azon++;
                lista.BeszurasElejere(kuldemeny);
            }

            return lista;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<FutarBase> futarok = FileReader.ReadCarriers();
            LancoltLista<IKuldemeny> kuldemenyek = FileReader.ReadPackages();

            Console.WriteLine(string.Join("\n", futarok));
            Console.WriteLine("\n=============================\n");
            kuldemenyek.Bejaras();



        }
    }
}
