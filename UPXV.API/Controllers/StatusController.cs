using Microsoft.AspNetCore.Mvc;
using UPXV.Models;
using UPXV.Services;

namespace UPXV_API.Controllers;

[ApiController]
[Route("[controller]")]
public class StatusController : ControllerBase<Status>
{
   private StatusService _service => (StatusService) _serviceBase;

   public StatusController (StatusService service) : base (service)
   {
   }
}
