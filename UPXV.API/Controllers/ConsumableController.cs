using Microsoft.AspNetCore.Mvc;
using UPXV.Models;
using UPXV.Services;

namespace UPXV_API.Controllers;

[ApiController]
[Route("[controller]")]
public class ConsumableController : ControllerBase<Consumable>
{
   private ConsumableService _service => (ConsumableService) _serviceBase;

   public ConsumableController (ConsumableService service) : base (service)
   {
   }
}
