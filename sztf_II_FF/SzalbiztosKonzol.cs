using System;

namespace sztf_II_FF
{
    public static class SzalbiztosKonzol
    {
        static char[,] buffer;
        static SzalbiztosKonzol()
        {
            buffer = new char[Console.WindowWidth, Console.WindowHeight + 200];
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
        }

        // a szinkronizacio miatt kell
        static object obj = new Object();

        public static void Clear()
        {
            buffer = new char[Console.WindowWidth, Console.WindowHeight];
        }

        // egy karakter kiirasa
        public static void KiirasXY(int x, int y, char betu)
        {
            lock (obj)
            {
                if (buffer[x, y] != betu)
                {
                    buffer[x, y] = betu;
                    Console.SetCursorPosition(x, y);
                    Console.Write(betu);
                }
            }
        }

        // egy sor kiirasa
        public static void KiirasXY(int x, int y, string szoveg)
        {
            for (int i = 0; i < szoveg.Length; i++)
                KiirasXY(x + i, y, szoveg[i]);
        }
    }
}