namespace UPXV.Common.Page;

public record PageDTO<T>
{
   public int CurrentPage { get; set; }
   public int PageSize { get; set; }
   public int TotalPages { get; set; }
   public int TotalCount { get; set; }
   public IEnumerable<T> Items { get; set; } = [];

   public PageDTO () { }
   public PageDTO (int pageIndex, int pageSize) => (CurrentPage, PageSize) = (pageIndex, pageSize);
   public PageDTO (IEnumerable<T> data, int pageIndex, int pageSize) {
      (CurrentPage, PageSize) = (pageIndex, pageSize);
      Items = data.Skip(CurrentPage * PageSize).Take(PageSize);
      TotalCount = data.Count();
      TotalPages = (int) Math.Ceiling(TotalCount / (double) PageSize);
   }
   public static PageDTO<U> Of<U> (IPage<U> page) => new PageDTO<U>
   {
      TotalPages = page.TotalPages,
      CurrentPage = page.CurrentPage,
      PageSize = page.PageSize,
      TotalCount = page.TotalCount,
      Items = page.Items.ToList(),
   };
}