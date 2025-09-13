using Microsoft.AspNetCore.Mvc;
using UPXV.Models;
using UPXV.Services;

namespace UPXV_API.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase<Item>
{
   private new readonly ItemService _service;

   public ItemController (ItemService service) : base (service)
   {
      _service = service;
   }
}
