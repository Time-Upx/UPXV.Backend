﻿using System.Collections;

namespace UPXV.Backend.Common.Extensions;

public static class EnumerableExtensions
{
   private static readonly Random _random = Random.Shared;

   public static ICollection<R> ToNonNullableList<T, R> (this ICollection<T> original, Func<T?, R?> mapper) => original
      .Where(r => r is not null)
      .Select(mapper)
      .Where(r => r is not null)
      .ToList()!;

   public static IEnumerable<T> Peek<T> (this IEnumerable<T> enumerable, Action<T> action)
      => enumerable.Select(i =>
      {
         action(i);
         return i;
      });

   public static IEnumerable<T> Paging<T>(this IEnumerable<T> enumerable, int pageIndex, int pageSize)
   {
      return enumerable.Skip(pageIndex * pageSize).Take(pageSize);
   }


   // Fisher-Yates (Knuth shuffle)
   public static IEnumerable<T> Shuffle<T> (this IEnumerable<T> enumerable)
   {
      var array = enumerable.ToArray();
      int n = array.Length;
      while (n > 1)
      {
         int k = _random.Next(n--);
         (array[k], array[n]) = (array[n], array[k]);
      }
      return array;
   }
}
