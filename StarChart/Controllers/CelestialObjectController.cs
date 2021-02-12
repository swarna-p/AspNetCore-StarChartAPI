using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using StarChart.Data;


namespace StarChart.Controllers
{

    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
       public CelestialObjectController(ApplicationDbContext context)
       {
            _context = context;
       }

       [HttpGet("{id:int}")]
       public IActionResult GetById(int id)
       {
           var celestialObject = _context.CelestialObjects.FirstOrDefault(o => o.Id == id);
           if(celestialObject !=null)
           {
                return Ok(celestialObject);
           }else
           {
                return NotFound();
           }
        }

       [HttpGet("{name}")]
       public IActionResult GetByName(string name)
       {
           var celestialObject = _context.CelestialObjects.FirstOrDefault(o => o.Name == name);
           if (celestialObject != null)
           {
               return Ok(celestialObject);
           }else
           {
               return NotFound();
           }
        }

       [HttpGet()]
       public IActionResult GetAll()
       {
           var celestialObjects = _context.CelestialObjects.ToList();
           foreach(var obj  in celestialObjects)
           {
                obj.Satellites = celestialObjects;
           }
            return Ok(celestialObjects);
       }
    }
}
