using FluentValidation;
using UPXV.Common;
using UPXV.Data;
using UPXV.DTOs;
using UPXV.Models;
using UPXV.Services.Exceptions;
using UPXV_API;
using ValidationException = UPXV.Services.Exceptions.ValidationException;

namespace UPXV.Services;

public abstract class ServiceBase<TEntity> where TEntity : class, IEntityBase
{
   protected RepositoryBase<TEntity> _repositoryBase;
   protected IValidator<TEntity> _validator;

   public ServiceBase (RepositoryBase<TEntity> repository, IValidator<TEntity> validator) 
   {
      _repositoryBase = repository;
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

      _repositoryBase.Create (ref entity);
      _repositoryBase.Save();
      return (TDetailDTO) new TDetailDTO().From(entity);
   }

   public virtual Result<TDetailDTO, Exception> Update<TDetailDTO>
      (IUpdateDTO<TEntity> dto, IValidator<IUpdateDTO<TEntity>>? dtoValidator = default)
      where TDetailDTO : IDetailDTO<TEntity>, new()
   {
      TEntity? entity = _repositoryBase.FindByNid(dto.Nid);
      if (entity is null)
      {
         return new EntityNotFoundException<TEntity>(dto.Nid);
      } 
      _repositoryBase.Update(ref entity);
      _repositoryBase.Save();
      return (TDetailDTO) new TDetailDTO().From(entity);
   }

   public virtual Result<TDetailDTO, Exception> Delete <TDetailDTO>(int nid)
      where TDetailDTO : IDetailDTO<TEntity>, new()
   {
      TEntity? entity = _repositoryBase.FindByNid(nid);
      if (entity is null)
      {
         return new EntityNotFoundException<TEntity>(nid);
      }
      _repositoryBase.Delete(ref entity);
      _repositoryBase.Save();
      return (TDetailDTO) new TDetailDTO().From(entity);
   }

   public virtual IPage<TListDTO> List<TListDTO> (PageDTO<TEntity> dto)
      where TListDTO : IListDTO<TEntity>, new()
   {
      Query<TEntity> query = Query<TEntity>.From(dto);
      ICollection<TEntity> entities = _repositoryBase.ReadQuery(query);
      return entities.Select(e => (TListDTO) new TListDTO().From(e)).ToPage(dto.PageSize);
   }

   public virtual Result<TDetailDTO, Exception> Get<TDetailDTO> (int nid)
      where TDetailDTO : IDetailDTO<TEntity>, new()
   {
      TEntity? entity = _repositoryBase.FindByNid(nid);
      if (entity is null)
      {
         return new EntityNotFoundException<TEntity>(nid);
      }
      return (TDetailDTO) new TDetailDTO().From(entity);
   }
}