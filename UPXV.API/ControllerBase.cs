using Microsoft.AspNetCore.Mvc;
using UPXV.DTOs;
using UPXV.Models;
using UPXV.Services;
using UPXV.Services.Exceptions;

namespace UPXV_API;

[ApiController]
[Route("[controller]")]
public class ControllerBase<TEntity> : ControllerBase where TEntity : class, IEntityBase
{
   protected ServiceBase<TEntity> _serviceBase;
   public ControllerBase (ServiceBase<TEntity> service)
   {
      _serviceBase = service;
   }

   [HttpGet]
   public virtual IActionResult List <TListDTO>(PageDTO<TEntity> query) 
      where TListDTO : IListDTO<TEntity>, new()
   {
      IPage<TListDTO> results = _serviceBase.List<TListDTO>(query);
      return Ok(results.ToDTO());
   }

   [HttpGet("{nid}")]
   public virtual IActionResult Detail <TDetailDTO>(int nid)
      where TDetailDTO : IDetailDTO<TEntity>, new()
   {
      var result = _serviceBase.Get<TDetailDTO>(nid);
      if (result.IsFailure && result.Problem is EntityNotFoundException<TEntity> e)
      {
         return NotFound(e.Message);
      }
      return Ok(result.Value!);
   }

   [HttpPost]
   public virtual IActionResult Create<TDetailDTO>(ICreateDTO<TEntity> dto)
      where TDetailDTO : IDetailDTO<TEntity>, new()
   {
      var result = _serviceBase.Create<TDetailDTO>(dto);
      if (result.IsFailure && result.Problem is ValidationException v)
      {
         return BadRequest(v.Failures);
      }
      return Ok(result.Value);
   }

   [HttpPut]
   public virtual IActionResult Update<TDetailDTO> (IUpdateDTO<TEntity> dto)
      where TDetailDTO : IDetailDTO<TEntity>, new()
   {
      var result = _serviceBase.Update<TDetailDTO>(dto);
      if (result.IsFailure && result.Problem is ValidationException v)
      {
         return BadRequest(v.Failures);
      }
      return Ok(result.Value);
   }

   [HttpDelete("{nid}")]
   public virtual IActionResult Delete<TDetailDTO> (int nid)
      where TDetailDTO : IDetailDTO<TEntity>, new()
   {
      var result = _serviceBase.Delete<TDetailDTO>(nid);
      if (result.IsFailure && result.Problem is EntityNotFoundException<TEntity> e)
      {
         return NotFound(e.Message);
      }
      return Ok(result.Value);
   }
}
