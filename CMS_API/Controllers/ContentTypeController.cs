using AutoMapper;
using CMS_API.Dtos.OutputDto;
using CMS_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentTypeController : ControllerBase
    {
        public readonly PROJECT_PRN231Context _context;
        public readonly IMapper _mapper;

        public ContentTypeController(PROJECT_PRN231Context context, IMapper mapper)
        {
            _context = context; 
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var l = _context.ContentTypes.ToList();
            return Ok( _mapper.Map<List<ContentTypeOutputDto>>(l));
        }
    }
}
