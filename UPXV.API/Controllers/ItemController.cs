using Microsoft.AspNetCore.Mvc;
using UPXV.Services;

namespace UPXV_API.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemController (ItemService itemService) : ControllerBase
{
   private ItemService _itemService = itemService;
}
