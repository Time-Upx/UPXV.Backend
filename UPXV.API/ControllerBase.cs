using Microsoft.AspNetCore.Mvc;
using UPXV.DTOs;
using UPXV.Models;
using UPXV.Services;

namespace UPXV_API;

public class ControllerBase<TEntity>(ServiceBase<TEntity> service) : ControllerBase where TEntity : class, IEntityBase
{
   protected ServiceBase<TEntity> _service = service;
   public virtual IActionResult Create(ICreateDTO<TEntity> model)
   {
      _service.Create()
   }
}
