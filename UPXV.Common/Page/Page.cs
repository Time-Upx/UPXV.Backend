namespace UPXV.Common.Page;

public interface IPage<T> : ICollection<T>, IList<T>
{
   public int CurrentPage { get; }
   public int TotalPages { get; }
   public int PageSize { get; }
   public int TotalCount { get; }
   public IEnumerable<T> Items { get; }
   public IPage<T> Next (int offset = 1);
   public bool HasNext (int offset = 1);
   public IPage<T> Previous (int offset = 1);
   public bool HasPrevious (int offset = 1);
   public IPage<T> To (int pageIndex);
   public bool HasPage (int pageIndex);
   public IPage<T> ChangeSize (int newSize);
}

public class Page<T> : List<T>, IPage<T>
{
   private int _currentPage = 0;
   public int CurrentPage 
   { 
      get => _currentPage; 
      private set
      {
         if (!HasPage(value)) throw new ArgumentOutOfRangeException(nameof(value));
         _currentPage = value;
      }
   }
   public int PageSize { get; private set; } = 5;
   public int TotalCount => Count;
   public int TotalPages => (int) Math.Ceiling(TotalCount / (double) PageSize);
   public IEnumerable<T> Items => this.Skip(CurrentPage * PageSize).Take(PageSize);
   public Page (int pageSize) : base() => PageSize = pageSize;
   public Page (IEnumerable<T> source, int pageSize) : base(source) => PageSize = pageSize;
   public bool HasNext (int offset = 1) 
   {
      CheckOffsetIsPositive(offset);
      return CurrentPage < TotalPages - offset;
   }
   public bool HasPrevious (int offset = 1) 
   {
      CheckOffsetIsPositive(offset);
      return CurrentPage >= 0 + offset;
   }
   public bool HasPage (int pageIndex)
   {
      return 0 < pageIndex && pageIndex < TotalPages;
   }
   public IPage<T> Next (int offset = 1)
   {
      CheckOffsetIsPositive(offset);
      CurrentPage += offset;
      return this;
   }
   public IPage<T> Previous (int offset = 1)
   {
      CheckOffsetIsPositive(offset);
      CurrentPage -= offset;
      return this;
   }
   public IPage<T> To (int pageIndex)
   {
      CurrentPage = pageIndex;
      return this;
   }
   public IPage<T> ChangeSize (int newSize)
   {
      PageSize = newSize;
      CurrentPage = 0;
      return this;
   }
   private void CheckOffsetIsPositive(int offset)
   {
      if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset), "Offset must be positive");
   }
}