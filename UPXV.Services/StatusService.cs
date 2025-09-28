using FluentValidation;
using FluentValidation.Results;
using UPXV.Common;
using UPXV.Common.Page;
using UPXV.Data;
using UPXV.Data.Repositories;
using UPXV.DTOs.Statuses;
using UPXV.Models;
using UPXV.Services.Exceptions;

namespace UPXV.Services;

public sealed class StatusService
{
   private StatusRepository _repository;
   private IValidator<Status> _validator;
   public StatusService (
      StatusRepository repository,
      IValidator<Status> validator
   ) {
      _repository = repository;
      _validator = validator;
   }

   public Result<StatusDetailDTO, Exception> Create (StatusCreateDTO dto)
   {
      if (_repository.DoesValueExists(dto.Tid, e => e.Tid))
      {
         var failure = new ValidationFailure(nameof(dto.Tid), "O identificador desejado já foi utilizado", dto.Tid);
         return new ValidationException([failure]);
      }
      Status status = dto.BuildEntity();
      ValidationResult validationResult = _validator.Validate(status);
      if (!validationResult.IsValid)
      {
         return new ValidationException(validationResult.Errors);
      }
      _repository.Create(status);
      _repository.Save();
      return StatusDetailDTO.Of(status);
   }

   public Result<StatusDetailDTO, Exception> Update (StatusUpdateDTO dto)
   {
      if (_repository.DoesValueExists(dto.Tid, e => e.Tid))
      {
         var failure = new ValidationFailure(nameof(dto.Tid), "O identificador desejado já foi utilizado", dto.Tid);
         return new ValidationException([failure]);
      }
      Status? status = _repository.FindByNid(dto.Nid);
      if (status is null)
      {
         return new EntityNotFoundException<Status>(dto.Nid);
      }
      dto.UpdateEntity(status);
      _repository.Update(status);
      _repository.Save();
      return StatusDetailDTO.Of(status);
   }

   public Result<StatusDetailDTO, Exception> Delete (int nid)
   {
      Status? status = _repository.FindByNid(nid);
      if (status is null) return new EntityNotFoundException<Status>(nid);
      _repository.Delete(status);
      _repository.Save();
      return StatusDetailDTO.Of(status);
   }

   public PageDTO<StatusListDTO> List (int pageIndex, int pageSize)
   {
      PageDTO<Status> pageDto = new PageDTO<Status>(pageIndex, pageSize);
      Query<Status> query = new Query<Status>(pageDto);
      ICollection<Status> entities = _repository.ReadQuery(query);
      IPage<StatusListDTO> page = entities.Select(StatusListDTO.Of).ToPage(pageSize);
      return PageDTO<StatusListDTO>.Of(page);
   }

   public Result<StatusDetailDTO, Exception> Get (int nid)
   {
      Status? status = _repository.FindByNid(nid);
      if (status is null)
      {
         return new EntityNotFoundException<Status>(nid);
      }
      return StatusDetailDTO.Of(status);
   }
}