using Microsoft.AspNetCore.Mvc;
using UPXV.Services;

namespace UPXV_API.Controllers;

[ApiController]
[Route("[controller]")]
public class ConsumableController (ConsumableService consumableService) : ControllerBase
{
   private ConsumableService _consumableService = consumableService;
}
