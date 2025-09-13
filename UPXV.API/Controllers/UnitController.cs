using Microsoft.AspNetCore.Mvc;
using UPXV.Models;
using UPXV.Services;

namespace UPXV_API.Controllers;

[ApiController]
[Route("[controller]")]
public class UnitController : ControllerBase<Unit>
{
   private new readonly UnitService _service;

   public UnitController (UnitService service) : base (service)
   {
      _service = service;
   }
}
