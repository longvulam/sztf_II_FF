using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using sztf_II_FF.Futarok;
using sztf_II_FF.Kivetelek;
using sztf_II_FF.Kuldemenyek;

namespace sztf_II_FF
{
    delegate void Kiszallitva(List<IKuldemeny> kuldemeny, LancoltLista<IKuldemeny> kuldemenyek);

    class Program
    {
        private static event Kiszallitva KiszallitvaEvent;
        private static readonly List<IKuldemeny> Kiszallitandok = new List<IKuldemeny>();
        private static readonly DateTime Today = DateTime.Today;
        private static int daysToAdd = 0;

        static void Main(string[] args)
        {
            List<Futar> futarok = FileReader.ReadCarriers();
            LancoltLista<IKuldemeny> kuldemenyek = FileReader.ReadPackages();

            var rendezdettFutarok = futarok
                    .Where(f => f.Dolgozik)
                    .OrderBy(f => f.Kapacitas);

            KiszallitvaEvent += Kiszallitas;

            if (!rendezdettFutarok.Any())
            {
                throw new NincsDolgozoFutarException();
            }

            do
            {
                Csomagfelvetel(kuldemenyek);

                Beosztas(kuldemenyek, rendezdettFutarok);
                Console.WriteLine("\n\n");
                Console.WriteLine("Az enter lenyomásával kiszállításra kerülnek a napi küldemények.");
                ConsoleKeyInfo inp;
                do
                {
                    inp = Console.ReadKey();
                } while (inp.Key != ConsoleKey.Enter);

                Console.WriteLine("Kiszállítás: ");
                Kiszallitas(rendezdettFutarok, kuldemenyek);
                Console.WriteLine("Kész!");

                do
                {
                    Console.WriteLine("\nEnter lenyomásával a következő nap beosztása látható.");
                    inp = Console.ReadKey();
                } while (inp.Key != ConsoleKey.Enter);

                Console.Clear();
                SzalbiztosKonzol.Clear();
            } while (kuldemenyek.Length > 0);

            Console.WriteLine();
        }

        private static void Csomagfelvetel(LancoltLista<IKuldemeny> kuldemenyek)
        {
            Console.WriteLine("Szeretne csomagot felvenni a rendszerbe?");
            string igenNem;
            do
            {
                igenNem = Console.ReadLine();
            } while (igenNem != "Nem" && igenNem != "Igen");

            if (igenNem == "Nem")
            {
                Console.Clear();
                return;
            }

            Console.WriteLine($"Kérem a típusát: ({nameof(NormalLevel)} - {nameof(ElsobbsegiLevel)} - {nameof(NormalCsomag)} - {nameof(ElsobbsegiCsomag)})");
            string tipus = Console.ReadLine();

            Console.WriteLine("Mennyire sűrgös: (1-10)");
            string prioStr;
            int prio;
            do
            {
                prioStr = Console.ReadLine();
            } while (int.TryParse(prioStr, out prio));

            Console.WriteLine("Kérem a tömegét: (grammban)");
            string tomegStr;
            int tomeg;
            do
            {
                tomegStr = Console.ReadLine();
            } while (int.TryParse(tomegStr, out tomeg));


            KuldemenyBase kuldemeny;
            switch (tipus)
            {
                case nameof(NormalLevel):
                    kuldemeny = new NormalLevel(tomeg, tomeg);
                    break;
                case nameof(ElsobbsegiLevel):
                    kuldemeny = new ElsobbsegiLevel(tomeg, tomeg);
                    break;
                case nameof(NormalCsomag):
                    kuldemeny = new NormalCsomag(tomeg, tomeg);
                    break;
                case nameof(ElsobbsegiCsomag):
                    kuldemeny = new ElsobbsegiCsomag(tomeg, tomeg);
                    break;
                default:
                    throw new NincsIlyenKuldemenyException($"Nem létezik ilyen Küldemény: {tipus}");
            }

            kuldemenyek.BeszurasElejere(kuldemeny);

            Console.Clear();
        }

        private static void Beosztas(LancoltLista<IKuldemeny> kuldemenyek, IOrderedEnumerable<Futar> rendezdettFutarok)
        {
            int x = 0, y = 0, btm = 0, nap = 0;
            while (kuldemenyek.Any(k => !k.Beosztva))
            {
                Ujnap(rendezdettFutarok);
                SzalbiztosKonzol.KiirasXY(x, y, $"{Today.Date.AddDays(nap + daysToAdd):M}:");
                nap++;

                foreach (Futar futar in rendezdettFutarok)
                {
                    FutarnakBeoszt(futar, kuldemenyek);
                    var worksStr = futar.Dolgozik ? "+" : "-";
                    SzalbiztosKonzol.KiirasXY(x, y += 2, $"Futar {futar.Id}. ({futar.Kapacitas}): {worksStr}");
                    y += 2;

                    foreach (IKuldemeny kuldemeny in futar.Kuldemenyek)
                    {
                        SzalbiztosKonzol.KiirasXY(x, y += 1, $"- {kuldemeny.Prioritas}, {kuldemeny.Tomeg}");
                        if (nap == 1)
                        {
                            Kiszallitandok.Add(kuldemeny);
                        }
                    }

                    x += 20;
                    btm = y > btm ? y : btm;
                    y = 0;
                }

                x = 0;
                y = btm + 2;
            }
        }

        private static void Ujnap(IEnumerable<Futar> futarok)
        {
            foreach (Futar futar in futarok)
            {
                futar.UjNap();
            }
        }

        private static void Kiszallitas(List<IKuldemeny> kiszallitandok, LancoltLista<IKuldemeny> kuldemenyek)
        {
            int waiting = 500;

            Thread.Sleep(waiting);
            Console.Write(".");
            Thread.Sleep(waiting);
            Console.Write(".");
            Thread.Sleep(waiting);
            Console.Write(".");
            Thread.Sleep(waiting);

            foreach (IKuldemeny kuldemeny in kiszallitandok)
            {
                kuldemenyek.Torles(kuldemeny);
            }

            kuldemenyek.Bejaras(k =>
            {
                k.Prioritas++;
                k.Beosztva = false;
            });

            Kiszallitandok.Clear();
        }

        private static void Kiszallitas(IEnumerable<Futar> futarok, LancoltLista<IKuldemeny> kuldemenyek)
        {
            Ujnap(futarok);
            KiszallitvaEvent(Kiszallitandok, kuldemenyek);
            daysToAdd++;
        }


        private static void FutarnakBeoszt(Futar futar, LancoltLista<IKuldemeny> kuldemenyek)
        {
            Func<IKuldemeny, bool> expr = x => !x.Beosztva &&
            x.Tomeg + futar.AktalisTeher < futar.Kapacitas;

            IKuldemeny kov = kuldemenyek
                .Kovetkezo(expr);

            while (kov != null)
            {
                futar.Kuldemenyek.Add(kov);
                futar.AktalisTeher += kov.Tomeg;
                kov.Beosztva = true;
                kov = kuldemenyek.Kovetkezo(expr);
            }

            futar.TeleVan = true;
        }
    }
}
