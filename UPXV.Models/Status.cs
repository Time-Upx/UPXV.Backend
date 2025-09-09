namespace UPXV.Models;

public class Status : IBaseModel
{
   public int Nid { get; set; }
   public required string Tid { get; set; }
   public string Description { get; set; } = string.Empty;
}
