using Microsoft.AspNetCore.Mvc;
using UPXV.Models;
using UPXV.Services;

namespace UPXV_API.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase<Item>
{
   private ItemService _service => (ItemService) _serviceBase;

   public ItemController (ItemService service) : base (service)
   {
   }
}
