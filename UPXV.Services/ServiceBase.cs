using FluentValidation;
using UPXV.Common;
using UPXV.Data;
using UPXV.DTOs;
using UPXV.Models;
using UPXV.Services.Exceptions;
using ValidationException = UPXV.Services.Exceptions.ValidationException;

namespace UPXV.Services;

public abstract class ServiceBase<TEntity> where TEntity : class, IEntityBase
{
   protected RepositoryBase<TEntity> _repository;
   protected IValidator<TEntity> _validator;

   public ServiceBase (RepositoryBase<TEntity> repository, IValidator<TEntity> validator) 
   {
      _repository = repository;
      _validator = validator;
   }

   public virtual Result<TDetailDTO, Exception> Create<TDetailDTO>
      (ICreateDTO<TEntity> dto, IValidator<ICreateDTO<TEntity>>? dtoValidator = default)
      where TDetailDTO : IDetailDTO<TEntity>, new()
   {
      var result = dtoValidator?.Validate(dto);

      if (result is not null && !result.IsValid)
         return new ValidationException(result.Errors);

      TEntity entity = dto.BuildEntity();
      result = _validator.Validate(entity);

      if (!result.IsValid)
         return new ValidationException(result.Errors);

      _repository.Create (ref entity);
      _repository.Save();
      return (TDetailDTO) new TDetailDTO().From(entity);
   }

   public virtual Result<TDetailDTO, Exception> Update<TDetailDTO>
      (IUpdateDTO<TEntity> dto, IValidator<IUpdateDTO<TEntity>>? dtoValidator = default)
      where TDetailDTO : IDetailDTO<TEntity>, new()
   {
      TEntity? entity = _repository.FindByNid(dto.Nid);
      if (entity is null)
      {
         return new EntityNotFoundException<TEntity>(dto.Nid);
      } 
      _repository.Update(ref entity);
      _repository.Save();
      return (TDetailDTO) new TDetailDTO().From(entity);
   }

   public virtual Result<int, Exception> Delete (QueryDTO<TEntity> queryDto)
   {
      Query<TEntity> query = queryDto.ToQuery();
      ICollection<TEntity> entities = _repository.ReadQuery(query);
      _repository.DeleteMultiple(entities);
      return _repository.Save();
   }

   public virtual ICollection<TListDTO> List<TListDTO> (QueryDTO<TEntity> queryDto)
      where TListDTO : IListDTO<TEntity>, new()
   {
      Query<TEntity> query = queryDto.ToQuery();
      ICollection<TEntity> entities = _repository.ReadQuery(query);
      return entities.Select(e => (TListDTO) new TListDTO().From(e)).ToList();
   }
}