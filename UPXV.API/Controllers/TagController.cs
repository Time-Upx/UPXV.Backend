using Microsoft.AspNetCore.Mvc;
using UPXV.Models;
using UPXV.Services;

namespace UPXV_API.Controllers;

[ApiController]
[Route("[controller]")]
public class TagController : ControllerBase<Tag>
{
   private new readonly TagService _service;

   public TagController (TagService service) : base(service) 
   { 
      _service = service;
   }
}
