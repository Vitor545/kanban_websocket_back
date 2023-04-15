using kanban_websocket_back.Data;
using kanban_websocket_back.Hubs;
using kanban_websocket_back.Hubs.Clients;
using kanban_websocket_back.Interfaces;
using kanban_websocket_back.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace kanban_websocket_back.Controllers
{
    [Route("lead")]
    public class LeadController : ControllerBase
    {
        private readonly IHubContext<KanbanHub, IKanbanClient> _chatHub;

        public LeadController(IHubContext<KanbanHub, IKanbanClient> chatHub)
        {
            _chatHub = chatHub;
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<ActionResult<List<Lead>>> GetLead(int id,
        [FromServices] postgresContext context
      )
        {
            try
            {
                var lead = await context.Leads.Where(x => x.UserId == id).AsNoTracking().OrderBy(x => x.IndexNumber).ToListAsync();
                return Ok(lead);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return StatusCode(500);
            };
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> PutFramework([FromServices] postgresContext context, [FromBody] ILeadUpdate leadBody
     )
        {
            try
            {
                var count = 0;
                foreach (var lead in leadBody.listBoard)
                {
                    var findLead = await context.Leads.AsNoTracking().FirstOrDefaultAsync(x => x.Id == lead.Id);
                    if (findLead == null)
                    {
                        return NotFound();
                    }
                    findLead.IndexNumber = count;
                    findLead.BoardId = leadBody.boardId;
                    context.Entry(findLead).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    count++;
                }
                await _chatHub.Clients.All.ReceiveMessage("Recriado");
                return Ok();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return StatusCode(500);
            };
        }

        [HttpPut]
        [Route("update/propsLead/taskId/{id:int}")]
        public async Task<ActionResult> PutPropsLeadFramework(int id, [FromServices] postgresContext context, [FromBody] ILeadUpdate leadBody
     )
        {
            try
            {
                var findLead = await context.Leads.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (findLead == null)
                {
                    return NotFound();
                }
                findLead.PropsLead = leadBody.propsLead;
                context.Entry(findLead).State = EntityState.Modified;
                await context.SaveChangesAsync();
                await _chatHub.Clients.All.ReceiveMessage("Recriado");
                return Ok();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return StatusCode(500);
            };
        }

        [HttpPut]
        [Route("update/color/leadId/{id:int}")]
        public async Task<ActionResult> PutFrameworkColor(int id, [FromServices] postgresContext context, [FromBody] ILeadUpdate leadBody
    )
        {
            try
            {
                var findLead = await context.Leads.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (findLead == null)
                {
                    return NotFound();
                }
                findLead.Color = leadBody.color;
                context.Entry(findLead).State = EntityState.Modified;
                await context.SaveChangesAsync();
                await _chatHub.Clients.All.ReceiveMessage("Recriado");
                return Ok();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return StatusCode(500);
            };
        }


        [HttpPut]
        [Route("update/name/leadId/{id:int}")]
        public async Task<ActionResult> PutFrameworkName(int id, [FromServices] postgresContext context, [FromBody] ILeadUpdate leadBody
    )
        {
            try
            {
                var findLead = await context.Leads.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (findLead == null)
                {
                    return NotFound();
                }
                findLead.Title = leadBody.title;
                context.Entry(findLead).State = EntityState.Modified;
                await context.SaveChangesAsync();
                await _chatHub.Clients.All.ReceiveMessage("Recriado");
                return Ok();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return StatusCode(500);
            };
        }

        [HttpPost]
        [Route("create/userId/{id:int}")]
        public async Task<ActionResult> PostFramework(
        uint id, [FromServices] postgresContext context, [FromBody] Lead leadBody
     )
        {
            try
            {
#pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
                User findUser = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
#pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
#pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
                Board findBoard = await context.Boards.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == findUser.Id);
#pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
#pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.
#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
                leadBody.UserId = findUser.Id;
#pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.
                leadBody.BoardId = leadBody.BoardId;
                context.Leads.Add(leadBody);
                await context.SaveChangesAsync();
                var lead = await context.Leads.Where(x => x.UserId == id).OrderBy(x => x.IndexNumber).AsNoTracking().ToListAsync();
                await _chatHub.Clients.All.ReceiveMessage(lead);
                return Ok(leadBody);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return StatusCode(500);
            };
        }
    }


}
