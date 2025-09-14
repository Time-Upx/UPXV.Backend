using Microsoft.AspNetCore.Mvc;
using UPXV.Models;
using UPXV.Services;

namespace UPXV_API.Controllers;

[ApiController]
[Route("[controller]")]
public class TagController : ControllerBase<Tag>
{
   private TagService _service => (TagService) _serviceBase;

   public TagController (TagService service) : base(service) 
   { 
   }
}
