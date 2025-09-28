using FluentValidation;
using FluentValidation.Results;
using UPXV.Common;
using UPXV.Common.Page;
using UPXV.Data;
using UPXV.Data.Repositories;
using UPXV.DTOs.Patrimonies;
using UPXV.Models;
using UPXV.Services.Exceptions;

namespace UPXV.Services;

public sealed class PatrimonyService
{
   private PatrimonyRepository _repository;
   private IValidator<Patrimony> _validator;

   public PatrimonyService (
      PatrimonyRepository repository,
      IValidator<Patrimony> validator
   )
   {
      _repository = repository;
      _validator = validator;
   }

   public Result<PatrimonyDetailDTO, Exception> Create (PatrimonyCreateDTO dto)
   {
      if (_repository.DoesValueExists(dto.Tid, e => e.Tid))
      {
         var failure = new ValidationFailure(nameof(dto.Tid), "O identificador desejado já foi utilizado", dto.Tid);
         return new ValidationException([failure]);
      }

      List<int> tagNids = dto.TagNids.ToList();
      ICollection<Tag> tags = _repository.ReadQuery(new Query<Tag>()
         .Filter(t => tagNids.Contains(t.Nid)));

      Patrimony Patrimony = dto.BuildEntity(tags);

      var validationResult = _validator.Validate(Patrimony);
      if (!validationResult.IsValid)
      {
         return new ValidationException(validationResult.Errors);
      }

      _repository.Create(Patrimony);
      _repository.Save();

      _repository.Load(Patrimony, c => c.Status);
      return PatrimonyDetailDTO.Of(Patrimony);
   }

   public Result<PatrimonyDetailDTO, Exception> Update (PatrimonyUpdateDTO dto)
   {
      if (_repository.DoesValueExists(dto.Tid, e => e.Tid))
      {
         ValidationFailure failure = new ValidationFailure(
            nameof(dto.Tid), "O identificador desejado já foi utilizado", dto.Tid);
         return new ValidationException([failure]);
      }

      ICollection<Tag>? tags = null;
      if (dto.TagNids != null)
      {
         var tagNids = dto.TagNids.ToList();
         tags = _repository.ReadQuery(new Query<Tag>().Filter(t => tagNids.Contains(t.Nid)));
      }

      Patrimony? Patrimony = _repository.FindByNid(dto.Nid);
      if (Patrimony is null) return new EntityNotFoundException<Patrimony>(dto.Nid);

      dto.UpdateEntity(Patrimony, tags);

      ValidationResult validationResult = _validator.Validate(Patrimony);
      if (!validationResult.IsValid)
         return new ValidationException(validationResult.Errors);

      _repository.Create(Patrimony);
      _repository.Save();

      _repository.Load(Patrimony, c => c.Status);

      return PatrimonyDetailDTO.Of(Patrimony);
   }

   public Result<PatrimonyDetailDTO, Exception> Delete (int nid)
   {
      Patrimony? Patrimony = _repository.FindByNid(nid);
      if (Patrimony is null) return new EntityNotFoundException<Patrimony>(nid);

      _repository.Delete(Patrimony);
      _repository.Save();

      return PatrimonyDetailDTO.Of(Patrimony);
   }

   public PageDTO<PatrimonyListDTO> List (int pageIndex, int pageSize)
   {
      PageDTO<Patrimony> pageDto = new PageDTO<Patrimony>(pageIndex, pageSize);

      Query<Patrimony> query = new Query<Patrimony>(pageDto);

      ICollection<Patrimony> entities = _repository.ReadQuery(query);

      IPage<PatrimonyListDTO> page = entities.Select(PatrimonyListDTO.Of).ToPage(pageSize);

      return PageDTO<PatrimonyListDTO>.Of(page);
   }

   public Result<PatrimonyDetailDTO, Exception> Get (int nid)
   {
      Patrimony? Patrimony = _repository.FindByNid(nid);
      if (Patrimony is null)
      {
         return new EntityNotFoundException<Patrimony>(nid);
      }
      return PatrimonyDetailDTO.Of(Patrimony);
   }
}