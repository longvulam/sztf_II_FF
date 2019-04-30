using System;
using sztf_II_FF.Kuldemenyek;

namespace sztf_II_FF.Futarok
{
    public abstract class FutarBase : EntityBase, IComparable
    {
        public bool Dolgozik { get; set; }
        public int SzallitasiKapacitas { get; set; }


        public override string ToString()
        {
            var dolgozikStr = Dolgozik ? "Dolgozik" : "Nem";
            return $"{Id}: {GetType().Name} -- {dolgozikStr} -- {SzallitasiKapacitas} gramm";
        }

        public int CompareTo(object obj)
        {
            if (!(obj is FutarBase)) return 1;
            if (ReferenceEquals(this, obj)) return 0;

            var other = (FutarBase)obj;
            var szabadsagonVanComparison = Dolgozik.CompareTo(other.Dolgozik);
            if (szabadsagonVanComparison != 0) return szabadsagonVanComparison;
            return SzallitasiKapacitas.CompareTo(other.SzallitasiKapacitas);
        }
    }
}