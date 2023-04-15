using kanban_websocket_back.Data;
using kanban_websocket_back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kanban_websocket_back.Controllers
{
    [Route("kanban")]
    public class KanbanController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Kanban>>> GetKanban([FromServices] postgresContext context
      )
        {
            try
            {
                var kanban = await context.Kanbans.Where(x => x.Id == 1).Include(x => x.Boards).ThenInclude(x => x.Leads.OrderBy(x => x.IndexNumber)).AsNoTracking().FirstOrDefaultAsync();
                if (kanban == null)
                    return NotFound();
                return Ok(kanban);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return StatusCode(500);
            };
        }
    }
}
