using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                var line = sr.ReadLine();

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
