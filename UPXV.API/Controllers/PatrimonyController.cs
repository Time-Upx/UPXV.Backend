using Microsoft.AspNetCore.Mvc;
using UPXV.Models;
using UPXV.Services;

namespace UPXV_API.Controllers;

[ApiController]
[Route("[controller]")]
public class PatrimonyController : ControllerBase<Patrimony>
{
   private new readonly PatrimonyService _service;

   public PatrimonyController (PatrimonyService service) : base (service)
   {
      _service = service;
   }
}
