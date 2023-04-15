using kanban_websocket_back.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kanban_websocket_back.Controllers
{
    public class FormController : ControllerBase
    {
        [HttpGet]
        [Route("/form/userId/{id:int}")]
        public async Task<ActionResult> GetFormProps(
         int id, [FromServices] postgresContext context
      )
        {
            try
            {
                var form = await context.Formmodels.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == id);
                if (form == null)
                {
                    return NotFound();
                }
                return Ok(form);

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return StatusCode(500);
            };
        }
    }
}
