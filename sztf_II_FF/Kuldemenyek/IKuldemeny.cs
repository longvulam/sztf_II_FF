using System;

namespace sztf_II_FF.Kuldemenyek
{
    public interface IKuldemeny : IComparable
    {
        int Prioritas { get; set; }
        float Tomeg { get; set; }
    }
}