using Microsoft.AspNetCore.Mvc;
using UPXV.Models;
using UPXV.Services;

namespace UPXV_API.Controllers;

[ApiController]
[Route("[controller]")]
public class UnitController : ControllerBase<Unit>
{
   private UnitService _service => (UnitService) _serviceBase;

   public UnitController (UnitService service) : base (service)
   {
   }
}
