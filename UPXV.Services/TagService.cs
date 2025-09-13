using UPXV.Data.Repositories;
using UPXV.Models;

namespace UPXV.Services;

public class TagService (TagRepository repository) : ServiceBase<Tag>(repository)
{
   protected new TagRepository _repository => (TagRepository) _repository;
}
