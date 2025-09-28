using Microsoft.AspNetCore.Mvc;
using UPXV.Services;

namespace UPXV_API;

[ApiController]
[Route("[controller]")]
public sealed class ItemController : ControllerBase
{
   private ItemService _service;
   public ItemController (ItemService service)
   {
      _service = service;
   }

   [HttpGet]
   public IActionResult List (int page, int size) => Ok(_service.List(page, size));
}