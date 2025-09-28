using FluentValidation;
using FluentValidation.Results;
using UPXV.Common;
using UPXV.Common.Page;
using UPXV.Data;
using UPXV.Data.Repositories;
using UPXV.DTOs.Tags;
using UPXV.Models;
using UPXV.Services.Exceptions;

namespace UPXV.Services;

public sealed class TagService
{
   private TagRepository _repository;
   private IValidator<Tag> _validator;
   public TagService (
      TagRepository repository,
      IValidator<Tag> validator
   )
   {
      _repository = repository;
      _validator = validator;
   }

   public Result<TagDetailDTO, Exception> Create (TagCreateDTO dto)
   {
      if (_repository.DoesValueExists(dto.Tid, e => e.Tid))
      {
         var failure = new ValidationFailure(nameof(dto.Tid), "O identificador desejado já foi utilizado", dto.Tid);
         return new ValidationException([failure]);
      }
      Tag unit = dto.BuildEntity();
      var validationResult = _validator.Validate(unit);
      if (!validationResult.IsValid)
      {
         return new ValidationException(validationResult.Errors);
      }
      _repository.Create(unit);
      _repository.Save();
      return TagDetailDTO.Of(unit);
   }

   public Result<TagDetailDTO, Exception> Update (TagUpdateDTO dto)
   {
      if (_repository.DoesValueExists(dto.Tid, e => e.Tid))
      {
         var failure = new ValidationFailure(nameof(dto.Tid), "O identificador desejado já foi utilizado", dto.Tid);
         return new ValidationException([failure]);
      }
      Tag? unit = _repository.FindByNid(dto.Nid);
      if (unit is null)
      {
         return new EntityNotFoundException<Tag>(dto.Nid);
      }
      dto.UpdateEntity(unit);
      _repository.Update(unit);
      _repository.Save();
      return TagDetailDTO.Of(unit);
   }

   public Result<TagDetailDTO, Exception> Delete (int nid)
   {
      Tag? unit = _repository.FindByNid(nid);
      if (unit is null) return new EntityNotFoundException<Tag>(nid);
      _repository.Delete(unit);
      _repository.Save();
      return TagDetailDTO.Of(unit);
   }

   public PageDTO<TagListDTO> List (int pageIndex, int pageSize)
   {
      PageDTO<Tag> pageDto = new PageDTO<Tag>(pageIndex, pageSize);
      Query<Tag> query = new Query<Tag>(pageDto);
      ICollection<Tag> entities = _repository.ReadQuery(query);
      IPage<TagListDTO> page = entities.Select(TagListDTO.Of).ToPage(pageSize);
      return PageDTO<TagListDTO>.Of(page);
   }

   public Result<TagDetailDTO, Exception> Get (int nid)
   {
      Tag? unit = _repository.FindByNid(nid);
      if (unit is null)
      {
         return new EntityNotFoundException<Tag>(nid);
      }
      return TagDetailDTO.Of(unit);
   }
}