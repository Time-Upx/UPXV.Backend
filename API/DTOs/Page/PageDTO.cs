namespace UPXV.Backend.API.DTOs.Page;

public record class PageDTO<T>
{
   public int CurrentPage { get; set; }
   public int PageSize { get; set; }
   public int TotalCount { get; set; }
   public int TotalPages { get; set; }
   public IEnumerable<T> Items { get; set; } = [];

   public PageDTO () { }
   public PageDTO (int pageIndex, int pageSize) => (CurrentPage, PageSize) = (pageIndex, pageSize);
   public PageDTO (IEnumerable<T> data, int pageIndex, int pageSize)
   {
      var items = data.ToList();
      (CurrentPage, PageSize) = (pageIndex, pageSize);
      Items = items.Skip(CurrentPage * PageSize).Take(PageSize).ToList();
      TotalCount = items.Count();
      TotalPages = (int) Math.Ceiling(TotalCount / (double) PageSize);
   }
}
