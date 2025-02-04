using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistroVisitantesApi.Models;

namespace RegistroVisitantesApi.Controllers
{
    [ApiController]
    [Route("visitantes")]
    public class VisitanteController : ControllerBase
    {
        private readonly ModelContext _context;

        public VisitanteController(ModelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Visitante>> GetAllVisitors()
        {
            return await _context.Visitantes.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Visitante>> AddNewVisitor(Visitante visitor)
        {
            try
            {
                _context.Visitantes.Add(visitor);
                await _context.SaveChangesAsync();

                visitor.Id = await _context.Visitantes.MaxAsync(x => x.Id);

                return visitor;
            }  catch (Exception ex)
            {
                return StatusCode(500, new { title = "Internal Server Error", message = "El error es: " + ex.InnerException?.Message });
            }
        }
    }
}
