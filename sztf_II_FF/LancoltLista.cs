using System;
namespace sztf_II_FF
{
    public class LancoltLista<T> where T : IComparable
    {
        private int count;
        

        public int Length => count;

        class ListaElem
        {
            public T tartalom;
            public ListaElem kovetkezo;
        }

        ListaElem fej;

        public LancoltLista()
        {
            fej = null;
        }

        public void BeszurasElejere(T ujTartalom)
        {
            ListaElem uj = new ListaElem
            {
                tartalom = ujTartalom
            };
            count++;

            if (fej == null)
            {
                fej = uj;
                return;
            }

            ListaElem p = fej;
            while (p.kovetkezo != null && ujTartalom.CompareTo(p.kovetkezo.tartalom) < 0)
            {
                p = p.kovetkezo;
            }

            p.kovetkezo = uj;

        }

        public void BeszurasVegere(T ujTartalom)
        {
            ListaElem uj = new ListaElem();
            uj.tartalom = ujTartalom;
            uj.kovetkezo = null;
            count++;

            if (fej == null)
            {
                fej = uj;
                return;
            }


            ListaElem p = fej;
            while (p.kovetkezo != null && ujTartalom.CompareTo(p.kovetkezo.tartalom) >= 0)
            {
                p = p.kovetkezo;
            }

            p.kovetkezo = uj;
        }

        void Feldolgoz(T tartalom)
        {
            // csinálunk valamit
            Console.WriteLine(tartalom.ToString());
        }

        public void Bejaras()
        {
            ListaElem p = fej;
            while (p != null)
            {
                Feldolgoz(p.tartalom);
                p = p.kovetkezo;
            }
        }

        public void Torles(T torlendo)
        {
            ListaElem p = fej;
            ListaElem e = null;
            while (p != null && !p.tartalom.Equals(torlendo))
            {
                e = p;
                p = p.kovetkezo;
            }

            if (p != null)
            {
                if (e == null)                  // megvan (első elem)
                    fej = p.kovetkezo;
                else                            // megvan (többi elem között)
                    e.kovetkezo = p.kovetkezo;
            }
            else                                // nem talált VAGY üres lista
            {
                throw new NincsIlyenElemException();
            }
        }
    }

    class NincsIlyenElemException : Exception { }
}