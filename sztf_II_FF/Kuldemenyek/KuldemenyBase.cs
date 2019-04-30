namespace sztf_II_FF.Kuldemenyek
{
    public abstract class KuldemenyBase : EntityBase, IKuldemeny
    {
        protected KuldemenyBase(int prio, int tomeg)
        {
            Prioritas = prio;
            Tomeg = tomeg;
            Count++;
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
            return $"Id: {Id} Prio: {Prioritas}, Tomeg: {Tomeg}, Hash: {GetHashCode()}";
        }

        public int Prioritas { get; }
        public int Tomeg { get; }
        public static int Count { get; set; }
    }
}