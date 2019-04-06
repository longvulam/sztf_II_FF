using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sztf_II_FF.Futarok;
using sztf_II_FF.Kuldemenyek;

namespace sztf_II_FF
{
    class FileReader
    {
        public static void ReadFromFile<T>(LancoltLista<T> lista) where T : IKuldemeny
        {
            StreamReader sr = new StreamReader("InputFiles/futarok.txt");
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine().Split(' ');
                //jarmu szabadsagonVanE SzallitasiKapacitas
                var jarmuEnum = (Jarmu)Enum.Parse(typeof(Jarmu), line[0]);

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
                }
            }
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            var elemek = new LancoltLista<IKuldemeny>();
            FileReader.ReadFromFile(elemek);

            elemek.Bejaras();

        }
    }
}
