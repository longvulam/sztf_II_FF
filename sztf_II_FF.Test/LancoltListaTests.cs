using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sztf_II_FF.Kuldemenyek;

namespace sztf_II_FF.Test
{
    [TestClass]
    public class LancoltListaTests
    {
        private readonly KisCsomag magasPrioCs = new KisCsomag
        {
            Prioritas = 10
        };

        private readonly KisCsomag magasPrioCs2 = new KisCsomag
        {
            Prioritas = 10
        };



        private readonly KisCsomag kozepesPrioCs = new KisCsomag
        {
            Prioritas = 5
        };

        private readonly KisCsomag kozepesPrioCs2 = new KisCsomag
        {
            Prioritas = 5
        };

        private readonly KisCsomag kozepesPrioCs3 = new KisCsomag
        {
            Prioritas = 5
        };

        private readonly KisCsomag kisPrioCs = new KisCsomag
        {
            Prioritas = 0
        };

        private LancoltLista<IKuldemeny> lista;

        [TestInitialize]
        public void Init()
        {
            lista = new LancoltLista<IKuldemeny>();
        }

        [TestMethod]
        public void BeszurasElejereTest()
        {
            BeszurasElejere(magasPrioCs);
            lista.Length.Should().Be(1);

            BeszurasElejere(kozepesPrioCs);
            lista.Length.Should().Be(2);

            BeszurasElejere(kozepesPrioCs2);
            lista.Length.Should().Be(3);

            BeszurasElejere(kisPrioCs);
            lista.Length.Should().Be(4);

            lista.Bejaras();
            Console.WriteLine(kozepesPrioCs2.GetHashCode());
        }

        [TestMethod]
        public void BeszurasVegereTest()
        {
            BeszurasVegere(magasPrioCs);
            lista.Length.Should().Be(1);

            BeszurasVegere(kozepesPrioCs);
            lista.Length.Should().Be(2);

            BeszurasVegere(kozepesPrioCs2);
            lista.Length.Should().Be(3);

            lista.Bejaras();
            Console.WriteLine(kozepesPrioCs2.GetHashCode());
        }

        private void BeszurasVegere(KisCsomag csomag)
        {
            Action<KisCsomag> act = lista.BeszurasVegere;

            act.Invoking(y => y.Invoke(csomag))
                .Should()
                .NotThrow();
        }

        private void BeszurasElejere(KisCsomag csomag)
        {
            Action<KisCsomag> act = lista.BeszurasElejere;

            act.Invoking(y => y.Invoke(csomag))
                .Should()
                .NotThrow();
        }
    }
}
