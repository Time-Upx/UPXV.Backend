using Microsoft.AspNetCore.Mvc;
using UPXV.Services;

namespace UPXV_API.Controllers;

[ApiController]
[Route("[controller]")]
public class TagController (TagService tagService) : ControllerBase
{
   private TagService _tagService = tagService;
}
