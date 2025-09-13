using Microsoft.AspNetCore.Mvc;
using UPXV.Models;
using UPXV.Services;

namespace UPXV_API.Controllers;

[ApiController]
[Route("[controller]")]
public class ConsumableController : ControllerBase<Consumable>
{
   private new readonly ConsumableService _service;

   public ConsumableController (ConsumableService service) : base (service)
   {
      _service = service;
   }
}
