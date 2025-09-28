using FluentValidation;
using FluentValidation.Results;
using UPXV.Common;
using UPXV.Common.Page;
using UPXV.Data;
using UPXV.Data.Repositories;
using UPXV.DTOs.Units;
using UPXV.Models;
using UPXV.Services.Exceptions;

namespace UPXV.Services;

public sealed class UnitService
{
   private UnitRepository _repository;
   private IValidator<Unit> _validator;
   
   public UnitService (
      UnitRepository repository,
      IValidator<Unit> validator
   ) {
      _repository = repository;
      _validator = validator;
   }

   public Result<UnitDetailDTO, Exception> Create (UnitCreateDTO dto)
   {
      if (_repository.DoesValueExists(dto.Tid, e => e.Tid))
      {
         var failure = new ValidationFailure(nameof(dto.Tid), "O identificador desejado já foi utilizado", dto.Tid);
         return new ValidationException([failure]);
      }
      Unit unit = dto.BuildEntity();
      var validationResult = _validator.Validate(unit);
      if (!validationResult.IsValid)
      {
         return new ValidationException(validationResult.Errors);
      }
      _repository.Create(unit);
      _repository.Save();
      return UnitDetailDTO.Of(unit);
   }

   public Result<UnitDetailDTO, Exception> Update (UnitUpdateDTO dto)
   {
      if (_repository.DoesValueExists(dto.Tid, e => e.Tid))
      {
         var failure = new ValidationFailure(nameof(dto.Tid), "O identificador desejado já foi utilizado", dto.Tid);
         return new ValidationException([failure]);
      }
      Unit? unit = _repository.FindByNid(dto.Nid);
      if (unit is null)
      {
         return new EntityNotFoundException<Unit>(dto.Nid);
      }
      dto.UpdateEntity(unit);
      _repository.Update(unit);
      _repository.Save();
      return UnitDetailDTO.Of(unit);
   }

   public Result<UnitDetailDTO, Exception> Delete (int nid)
   {
      Unit? unit = _repository.FindByNid(nid);
      if (unit is null) return new EntityNotFoundException<Unit>(nid);
      _repository.Delete(unit);
      _repository.Save();
      return UnitDetailDTO.Of(unit);
   }

   public PageDTO<UnitListDTO> List (int pageIndex, int pageSize)
   {
      PageDTO<Unit> pageDto = new PageDTO<Unit>(pageIndex, pageSize);
      Query<Unit> query = new Query<Unit>(pageDto);
      ICollection<Unit> entities = _repository.ReadQuery(query);
      IPage<UnitListDTO> page = entities.Select(UnitListDTO.Of).ToPage(pageSize);
      return PageDTO<UnitListDTO>.Of(page);
   }

   public Result<UnitDetailDTO, Exception> Get (int nid)
   {
      Unit? unit = _repository.FindByNid(nid);
      if (unit is null)
      {
         return new EntityNotFoundException<Unit>(nid);
      }
      return UnitDetailDTO.Of(unit);
   }
}