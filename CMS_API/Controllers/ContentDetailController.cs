using AutoMapper;
using CMS_API.Dtos.InputDto;
using CMS_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentDetailController : ControllerBase
    {
        public readonly PROJECT_PRN231Context _context;
        public readonly IMapper _mapper;

        public ContentDetailController(PROJECT_PRN231Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet()]
        public IActionResult Get()
        {

            var x = _context.ContentDetails.ToList();

            return Ok(x);
        }

        [HttpPost]
        public IActionResult Add(ContentDetailInputDto detail)
        {
            try
            {
                var input = _mapper.Map<ContentDetail>(detail);

                _context.ContentDetails.Add(input);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return Conflict();
                throw;
            }
        }

        [HttpPut]
        public IActionResult Update(ContentDetail input)
        {
            _context.ContentDetails.Update(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var x = _context.ContentDetails.FirstOrDefault(x => x.ContentDetailId == id);
            _context.ContentDetails.Remove(x);
            return Ok();
        }
    }
}
