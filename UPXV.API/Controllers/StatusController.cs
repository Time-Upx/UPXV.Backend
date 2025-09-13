using Microsoft.AspNetCore.Mvc;
using UPXV.Models;
using UPXV.Services;

namespace UPXV_API.Controllers;

[ApiController]
[Route("[controller]")]
public class StatusController : ControllerBase<Status>
{
   private new readonly StatusService _service;

   public StatusController (StatusService service) : base (service)
   {
      _service = service;
   }
}
