using System;
using System.Collections.Generic;
using System.Linq;

namespace MVC5App.Extensions
{
    public static class ListExtensions
    {
        private static readonly Random Random = new Random();

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            var copy = source.ToArray();

            for (var i = copy.Length - 1; i >= 0; i--)
            {
                var index = Random.Next(i + 1);
                yield return copy[index];
                copy[index] = copy[i];
            }
        }
    }
}