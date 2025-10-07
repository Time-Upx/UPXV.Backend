using UPXV.Backend.API.DTOs.Items;
using UPXV.Backend.API.DTOs.Page;
using UPXV.Backend.Common.Extensions;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Items;

public static class ItemListAction
{
   public static IResult MapEndpoint (int pageIndex, int pageSize, UPXV_Context context)
   {
      PageDTO<ItemListDTO> page = Execute(pageIndex, pageSize, context);
      return Results.Ok(page);
   }

   public static PageDTO<ItemListDTO> Execute (int pageIndex, int pageSize, UPXV_Context context)
   {
      IEnumerable<ItemListDTO> consumables = context.Consumables
         .ToList()
         .Peek(context.LoadRequirements)
         .Select(ItemListDTO.Of);

      IEnumerable<ItemListDTO> patrimonies = context.Patrimonies
         .ToList()
         .Peek(context.LoadRequirements)
         .Select(ItemListDTO.Of);

      IEnumerable<ItemListDTO> items = consumables
         .Concat(patrimonies)
         .OrderBy(i =>
         {
            if (i.Consumable is not null) return i.Consumable.Name;
            if (i.Patrimony is not null) return i.Patrimony.Name;
            return "";
         })
         .Paging(pageIndex, pageSize);

      return new PageDTO<ItemListDTO>(items, pageIndex, pageSize);
   }
}
