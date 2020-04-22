using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSF.Models;

namespace SSF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveriesController : ControllerBase
    {
        private readonly Context _context;
        Delivery toXml = new Delivery();


        public DeliveriesController(Context context)
        {
            _context = context;
        }

        // GET: api/Deliveries
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Delivery>>> GetDeliveries()
        {
            return await _context.LeveransXML.ToListAsync();
        }

        // GET: api/Deliveries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Delivery>> GetDelivery(int id)
        {
            var toXml = await _context.LeveransXML.FindAsync(id);

            if (toXml == null)
            {
                return NotFound();
            }

            var xml = $"<Etikettdata><Moviename>{toXml.MovieName}</Moviename><City>{toXml.CityOfStudio}</City><Orderdate>{toXml.OrderDate}</Orderdate></Etikettdata>";
            return new ContentResult
            {
                Content = xml,
                ContentType = "application/xml"
            };

           
        }

        // PUT: api/Deliveries/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDelivery(int id, Delivery delivery)
        {
            if (id != delivery.Id)
            {
                return BadRequest();
            }

            _context.Entry(delivery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Deliveries
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Delivery>> PostDelivery(Delivery delivery)
        {
            _context.LeveransXML.Add(delivery);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDelivery", new { id = delivery.Id }, delivery);
        }

        // DELETE: api/Deliveries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Delivery>> DeleteDelivery(int id)
        {
            var delivery = await _context.LeveransXML.FindAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }

            _context.LeveransXML.Remove(delivery);
            await _context.SaveChangesAsync();

            return delivery;
        }

        private bool DeliveryExists(int id)
        {
            return _context.LeveransXML.Any(e => e.Id == id);
        }
    }
}
