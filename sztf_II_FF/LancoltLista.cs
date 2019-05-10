using System;
namespace sztf_II_FF
{
    public class LancoltLista<T> where T : class, IComparable
    {
        private int count;
        private ListaElem aktualisElem;
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

        public bool Any(Func<T, bool> boolExpr)
        {
            ListaElem p = fej;
            while (p != null && !boolExpr(p.tartalom))
            {
                p = p.kovetkezo;
            }

            return p != null;
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

            if (p == fej && ujTartalom.CompareTo(fej.tartalom) > 0)
            {
                uj.kovetkezo = fej;
                fej = uj;
                return;
            }

            if (p.kovetkezo != null)
            {
                uj.kovetkezo = p.kovetkezo;
                p.kovetkezo = uj;
                return;
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

        public void Bejaras(Action<T> action)
        {
            ListaElem p = fej;
            while (p != null)
            {
                action(p.tartalom);
                p = p.kovetkezo;
            }
        }

        public T Kovetkezo(Func<T, bool> boolExpr)
        {
            if (aktualisElem == null)
            {
                aktualisElem = fej;
            }

            var p = aktualisElem == fej ? aktualisElem : aktualisElem.kovetkezo;
            while (p != null && !boolExpr(p.tartalom))
            {
                p = p.kovetkezo;
            }
            aktualisElem = p;
            return p?.tartalom;
        }

        /// <summary>
        /// Get the first item's content and removes it from the list.
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            ListaElem p = fej;
            fej = p.kovetkezo;
            p.kovetkezo = null;
            return p.tartalom;
        }

        /// <summary>
        /// Get the first item's content.
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            return fej.tartalom;
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

                count--;
            }
            else                                // nem talált VAGY üres lista
            {
                throw new NincsIlyenElemException();
            }
        }
    }

    class NincsIlyenElemException : Exception { }
}