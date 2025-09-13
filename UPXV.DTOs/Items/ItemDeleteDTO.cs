using UPXV.Models;

namespace UPXV.DTOs.Items;

public record struct ItemDeleteDTO : IDeleteDTO<Item>
{
   public int Nid { get; init; }
}
