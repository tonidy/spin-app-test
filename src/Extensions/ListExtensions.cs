using System;
using System.Runtime.CompilerServices;

namespace SpinGameApp.Extensions
{
    public static class ListExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Shuffle<T>(this IList<T> list)
        {
            ArgumentNullException.ThrowIfNull(list);
            int n = list.Count;
            while (n > 1)
            {
                int k = Random.Shared.Next(n--);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }

        public static void Shuffle<T>(this IList<T> list, int times)
        {
            if (times <= 1)
                return;
                
            int n = times;
            while (n > 1)
            {
                list.Shuffle();
                n--;
            }
        }
    }
}