using System;
using System.Collections.Generic;
using sztf_II_FF.Kuldemenyek;

namespace sztf_II_FF.Futarok
{
    public abstract class Futar : EntityBase, IComparable
    {
        public bool Dolgozik { get; set; }
        public int Kapacitas { get; set; }
        public int AktalisTeher { get; set; }
        public bool TeleVan { get; set; }

        public IList<IKuldemeny> Kuldemenyek = new List<IKuldemeny>();

        protected Futar()
        {
            AktalisTeher = 0;
        }


        public void UjNap()
        {
            AktalisTeher = 0;
            Kuldemenyek.Clear();
            TeleVan = false;
        }

        public void Kiiras()
        {
            Console.WriteLine("\n"+ToString() + ":\n");
            Console.WriteLine($"{string.Join("\n", Kuldemenyek)}\n");
        }

        public override string ToString()
        {
            var dolgozikStr = Dolgozik ? "Dolgozik" : "Nem";
            return $"{Id}: {GetType().Name} -- {dolgozikStr} -- {Kapacitas} gramm";
        }

        public int CompareTo(object obj)
        {
            if (!(obj is Futar)) return 1;
            if (ReferenceEquals(this, obj)) return 0;

            var other = (Futar)obj;
            var szabadsagonVanComparison = Dolgozik.CompareTo(other.Dolgozik);
            if (szabadsagonVanComparison != 0) return szabadsagonVanComparison;
            return Kapacitas.CompareTo(other.Kapacitas);
        }
    }
}