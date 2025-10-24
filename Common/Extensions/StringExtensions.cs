using System.Collections.Generic;

namespace UPXV.Backend.Common.Extensions;

public static class StringExtensions
{
   public static string[] SplitSections (this string value, params string[]? separators)
   {
      if (string.IsNullOrEmpty(value)) return [value];
      if (separators is null || separators.Length == 0) return [value];
      if (separators?.Length == 1) return value.Split(separators[0]);

      Stack<string> parts = new([value]);

      foreach (string separator in separators!)
      {
         var slices = parts.Pop().Split(separator);
         parts.Push(slices[0]);
         parts.Push(slices[1]);
      }

      return parts.ToArray();
   }
}
