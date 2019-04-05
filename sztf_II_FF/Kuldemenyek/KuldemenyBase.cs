namespace sztf_II_FF.Kuldemenyek
{
    public abstract class KuldemenyBase : EntityBase, IKuldemeny
    {
        protected KuldemenyBase()
        {
            Count++;
        }

        public int CompareTo(object obj)
        {
            var masikCsomag = (obj as IKuldemeny);
            var csomag = (this as IKuldemeny);
            if (csomag.Prioritas < masikCsomag.Prioritas)
            {
                return -1;
            }

            if (csomag.Prioritas == masikCsomag.Prioritas)
            {
                return 0;
            }

            return 1;
        }

        public override string ToString()
        {
            return $"Prio: {Prioritas}, Tomeg: {Tomeg}, Hash: {GetHashCode()}";
        }

        public int Prioritas { get; set; }
        public int Tomeg { get; set; }
        public static int Count { get; set; }
    }
}