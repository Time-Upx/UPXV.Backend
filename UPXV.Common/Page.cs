namespace UPXV_API;

public interface IPage<T> : IList<T>
{
   public int CurrentPage { get; }
   public int TotalPages { get; }
   public int PageSize { get; }
   public int TotalCount { get; }
   public IPage<T> Next (int offset = 1);
   public IPage<T> Previous (int offset = 1);
   public IPage<T> To(int pageIndex);
   public IPage<T> ChangePageSize (int newSize);
   public PageDTO<T> ToDTO ();
}

public class Page<T> : List<T>, IPage<T>
{
   private List<T> _allItems = [];
   public ICollection<T> AllItems => _allItems.ToList();
   public int TotalCount { get; private set; }
   public int CurrentPage { get; private set; }
   public int TotalPages { get; private set; }
   public int PageSize { get; private set; }
   public Page (IEnumerable<T> source, int pageSize)
   {
      PageSize = pageSize;
      CurrentPage = 0;
      _allItems.AddRange(source);
      TotalCount = _allItems.Count();
      TotalPages = (int) Math.Ceiling(TotalCount / (double) PageSize);
      ResetCurrentItems();
   }

   public IPage<T> Next (int offset = 1)
   {
      CurrentPage += offset;
      if (CurrentPage >= TotalPages) CurrentPage = TotalPages - 1;
      ResetCurrentItems();
      return this;
   }
   public IPage<T> Previous (int offset = 1)
   {
      CurrentPage -= offset;
      if (CurrentPage < 0) CurrentPage = 0;
      ResetCurrentItems();
      return this;
   }

   public IPage<T> To (int pageIndex)
   {
      CurrentPage = pageIndex;
      ResetCurrentItems();
      return this;
   }

   public IPage<T> ChangePageSize (int newSize)
   {
      PageSize = newSize;
      CurrentPage = 0;
      TotalPages = (int) Math.Ceiling(TotalCount / (double) PageSize);
      ResetCurrentItems();
      return this;
   }

   public PageDTO<T> ToDTO()
   {
      return new PageDTO<T>
      {
         TotalPages = TotalPages,
         CurrentPage = CurrentPage,
         PageSize = PageSize,
         TotalCount = TotalCount,
         Items = ToArray(),
      };
   }

   private void ResetCurrentItems ()
   {
      var items = _allItems.Skip(CurrentPage * PageSize).Take(PageSize).ToList();
      Clear();
      AddRange(items);
   }
}

public readonly record struct PageDTO<T>
{
   public int CurrentPage { get; init; }
   public int TotalPages { get; init; }
   public int PageSize { get; init; }
   public int TotalCount { get; init; }
   public T[] Items { get; init; } = [];
   public PageDTO() {}
}

public static class PageExtensions
{
   public static IPage<T> ToPage<T>(this IEnumerable<T> items, int pageSize)
   {
      return new Page<T>(items, pageSize);
   }
}