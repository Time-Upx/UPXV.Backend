using Microsoft.AspNetCore.Mvc;
using UPXV.DTOs.Identity;

namespace UPXV_API.Controllers;

[Route("[controller]")]
[ApiController]
public class IdentityController : ControllerBase
{
   private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(1);

   [HttpGet]
   public IActionResult GenerateToken (TokenRequestDTO request)
   {
      return Ok();
   }
}
