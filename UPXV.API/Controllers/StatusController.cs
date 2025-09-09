using Microsoft.AspNetCore.Mvc;
using UPXV.Services;

namespace UPXV_API.Controllers;

[ApiController]
[Route("[controller]")]
public class StatusController (StatusService statusService) : ControllerBase
{
   private StatusService _statusService = statusService;
}
