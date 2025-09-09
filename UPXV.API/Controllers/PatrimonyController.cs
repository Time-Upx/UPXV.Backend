using Microsoft.AspNetCore.Mvc;
using UPXV.Services;

namespace UPXV_API.Controllers;

[ApiController]
[Route("[controller]")]
public class PatrimonyController (PatrimonyService patrimonyService) : ControllerBase
{
   private PatrimonyService _patrimonyService = patrimonyService;
}
