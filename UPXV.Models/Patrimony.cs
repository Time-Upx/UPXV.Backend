namespace UPXV.Models;

public class Patrimony : IEntityBase
{
   public int Nid { get; set; }
   public required string Tid { get; set; }
   public int ItemNid { get; set; }
   public Item? Item { get; set; }
   public DateTime RegisteredIn { get; set; } = DateTime.Now;
   public string RegisteredBy { get; set; } = string.Empty;
   public int StatusNid { get; set; }
   public Status? Status { get; set; }
}
