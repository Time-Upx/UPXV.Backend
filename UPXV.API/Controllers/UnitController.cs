using Microsoft.AspNetCore.Mvc;
using UPXV.Services;

namespace UPXV_API.Controllers;

[ApiController]
[Route("[controller]")]
public class UnitController (UnitService unitService) : ControllerBase
{
   private UnitService _unitService = unitService;
}
