namespace UPXV.Models;

public class Patrimony : Item, IBaseModel
{
   public DateTime RegisteredIn { get; set; } = DateTime.Now;
   public string RegisteredBy { get; set; } = string.Empty;
   public int StatusId { get; set; }
   public Status? Status { get; set; }
}
