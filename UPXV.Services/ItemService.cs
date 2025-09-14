using FluentValidation;
using UPXV.Data;
using UPXV.Data.Repositories;
using UPXV.Models;

namespace UPXV.Services;

public class ItemService : ServiceBase<Item>
{
   private ItemRepository _repository => (ItemRepository) _repositoryBase;
   public ItemService (ItemRepository repository, IValidator<Item> validator) : base(repository, validator)
   {
   }

}