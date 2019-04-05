using System;

namespace sztf_II_FF.Kuldemenyek
{
    public interface IKuldemeny : IComparable
    {
        int Prioritas { get; set; }
        /// <summary>
        /// Tömeg grammban megadva
        /// </summary>
        int Tomeg { get; set; }
    }
}