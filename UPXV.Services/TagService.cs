using FluentValidation;
using UPXV.Data;
using UPXV.Data.Repositories;
using UPXV.Models;

namespace UPXV.Services;

public class TagService : ServiceBase<Tag>
{
   private TagRepository _repository => (TagRepository) _repositoryBase;
   public TagService (TagRepository repository, IValidator<Tag> validator) : base(repository, validator)
   {
   }



}
