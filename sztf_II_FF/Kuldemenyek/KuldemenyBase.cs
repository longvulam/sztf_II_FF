using System;

namespace sztf_II_FF.Kuldemenyek
{
    public abstract class KuldemenyBase : EntityBase, IKuldemeny
    {
        private static int IdIndex = 1;
        public int Prioritas { get; set; }
        public int Tomeg { get; }
        public bool Beosztva { get; set; }

        public static int Count { get; set; }

        protected KuldemenyBase(int prio, int tomeg)
        {
            Prioritas = prio;
            Tomeg = tomeg;
            Count++;
            Id = IdIndex++;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is IKuldemeny)) return 1;
            var masikCsomag = (IKuldemeny)obj;
            var csomag = (IKuldemeny)this;
            if (csomag.Prioritas < masikCsomag.Prioritas)
            {
                return -1;
            }

            if (csomag.Prioritas == masikCsomag.Prioritas)
            {
                return csomag.Tomeg > masikCsomag.Tomeg ? 1 : csomag.Tomeg == masikCsomag.Tomeg ? 0 : -1;
            }

            return 1;
        }

        public override string ToString()
        {
            var beosztvaHun = Beosztva ? $"Igen" : "Nem";
            return $"Id: {Id} Prio: {Prioritas}, Tomeg: {Tomeg}, Beosztva: {beosztvaHun}";
        }
    }
}