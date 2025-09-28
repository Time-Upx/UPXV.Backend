using UPXV.Common.Extensions;
using UPXV.Common.Page;
using UPXV.Data;
using UPXV.Data.Repositories;
using UPXV.DTOs.Items;
using UPXV.Models;

namespace UPXV.Services;

public sealed class ItemService
{
   private ConsumableRepository _consumableRepository;
   private PatrimonyRepository _patrimonyRepository;

   public ItemService (ConsumableRepository consumableRepository, PatrimonyRepository patrimonyRepository)
   {
      _consumableRepository = consumableRepository;
      _patrimonyRepository = patrimonyRepository;
   }

   public PageDTO<ItemListDTO> List (int pageIndex, int pageSize)
   {
      PageDTO<Consumable> consumablesDto = new PageDTO<Consumable>(pageIndex, pageSize);
      Query<Consumable> consumableQuery = new Query<Consumable>(consumablesDto);
      ICollection<Consumable> consumables = _consumableRepository.ReadQuery(consumableQuery);

      PageDTO<Patrimony> patrimoniesDto = new PageDTO<Patrimony>(pageIndex, pageSize);
      Query<Patrimony> patrimonyQuery = new Query<Patrimony>(patrimoniesDto);
      ICollection<Patrimony> patrimonies = _consumableRepository.ReadQuery(patrimonyQuery);

      IEnumerable<ItemListDTO> items = consumables.Select(ItemListDTO.Of)
         .Concat(patrimonies.Select(ItemListDTO.Of))
         .Shuffle();

      return new PageDTO<ItemListDTO>(items, pageIndex, pageSize);
   }
}
