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
               celestialObject.Satellites = _context.CelestialObjects.Where(o => o.OrbitedObjectId == id).ToList();
                return Ok(celestialObject);
           }else
           {
                return NotFound();
           }
        }

       [HttpGet("{name}")]
       public IActionResult GetByName(string name)
       {
           var celestialObjects = _context.CelestialObjects.Where(o => o.Name == name).ToList();
           if (!celestialObjects.Any())
           {
               return NotFound();
           }
            foreach (var celestialObject in celestialObjects)
            {
                celestialObject.Satellites = _context.CelestialObjects.Where(o => o.OrbitedObjectId == celestialObject.Id).ToList();
               
            }
            return Ok(celestialObjects);
           
        }

       [HttpGet()]
       public IActionResult GetAll()
       {
           var celestialObjects = _context.CelestialObjects.ToList();
          foreach(var celestialObject in celestialObjects)
          {
              celestialObject.Satellites = _context.CelestialObjects.Where(o => o.OrbitedObjectId == celestialObject.Id).ToList();
          }
            return Ok(celestialObjects);
       }
    }
}
