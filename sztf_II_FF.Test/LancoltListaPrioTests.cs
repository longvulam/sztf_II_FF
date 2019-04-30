using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sztf_II_FF.Kuldemenyek;

namespace sztf_II_FF.Test
{
    [TestClass]
    public class LancoltListaPrioTests
    {
        private readonly KuldemenyBase magasPrioCs = new NormalLevel(10, 0);
        private readonly KuldemenyBase magasPrioCs2 = new NormalLevel(10, 0);

        private readonly KuldemenyBase kozepesPrioCs = new NormalLevel(5, 0);
        private readonly KuldemenyBase kozepesPrioCs2 = new NormalLevel(5, 0);
        private readonly KuldemenyBase kozepesPrioCs3 = new NormalLevel(5, 0);

        private readonly KuldemenyBase kisPrioCs = new NormalLevel(1, 0);
        
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

        private void BeszurasVegere(IKuldemeny csomag)
        {
            Action<IKuldemeny> act = lista.BeszurasVegere;

            act.Invoking(y => y.Invoke(csomag))
                .Should()
                .NotThrow();
        }

        private void BeszurasElejere(IKuldemeny csomag)
        {
            Action<IKuldemeny> act = lista.BeszurasElejere;

            act.Invoking(y => y.Invoke(csomag))
                .Should()
                .NotThrow();
        }
    }
}
