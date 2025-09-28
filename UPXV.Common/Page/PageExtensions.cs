namespace UPXV.Common.Page;

public static class PageExtensions
{
   public static IPage<T> ToPage<T> (this IEnumerable<T> items, int pageSize)
   {
      return new Page<T>(items, pageSize);
   }
}