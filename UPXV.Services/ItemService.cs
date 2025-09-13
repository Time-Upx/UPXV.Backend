using UPXV.Data.Repositories;
using UPXV.Models;

namespace UPXV.Services;

public class ItemService (ItemRepository repository) : ServiceBase<Item>(repository)
{
   protected new ItemRepository _repository => (ItemRepository) _repository;
}