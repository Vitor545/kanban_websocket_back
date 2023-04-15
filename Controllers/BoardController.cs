using kanban_websocket_back.Data;
using kanban_websocket_back.Hubs;
using kanban_websocket_back.Hubs.Clients;
using kanban_websocket_back.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace kanban_websocket_back.Controllers
{
    [Route("board")]
    public class BoardController : ControllerBase
    {
        private readonly IHubContext<KanbanHub, IKanbanClient> _chatHub;

        public BoardController(IHubContext<KanbanHub, IKanbanClient> chatHub)
        {
            _chatHub = chatHub;
        }

        [HttpPost]
        [Route("delete/{id:int}")]
        public async Task<ActionResult> PostDeleteFramework(
      int id, [FromServices] postgresContext context
   )
        {
            try
            {
                var leads = await context.Leads.Where(x => x.BoardId == id).AsNoTracking().ToListAsync();
                foreach (var lead in leads)
                {
                    context.Remove(lead);
                    await context.SaveChangesAsync();

                }
#pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
                Board findBoard = await context.Boards.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
#pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
#pragma warning disable CS8634 // O tipo 'kanban_websocket_back.Models.Board?' não pode ser usado como parâmetro de tipo 'TEntity' no tipo ou método genérico 'DbContext.Remove<TEntity>(TEntity)'. A anulabilidade do argumento de tipo 'kanban_websocket_back.Models.Board?' não corresponde à restrição 'class'.
                context.Remove(findBoard);
#pragma warning restore CS8634 // O tipo 'kanban_websocket_back.Models.Board?' não pode ser usado como parâmetro de tipo 'TEntity' no tipo ou método genérico 'DbContext.Remove<TEntity>(TEntity)'. A anulabilidade do argumento de tipo 'kanban_websocket_back.Models.Board?' não corresponde à restrição 'class'.
                await context.SaveChangesAsync();
                await _chatHub.Clients.All.ReceiveMessage("Apagado");
                return Ok("Deletado");
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return StatusCode(500);
            };
        }


        [HttpPut]
        [Route("update/color/{id:int}")]
        public async Task<ActionResult> PutColorFramework(
      int id, [FromServices] postgresContext context, [FromBody] Board boardBody
   )
        {
            try
            {
#pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
                Board findBoard = await context.Boards.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
#pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
                findBoard.Color = boardBody.Color;
#pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.
                context.Entry(findBoard).State = EntityState.Modified;
                await context.SaveChangesAsync();
                await _chatHub.Clients.All.ReceiveMessage("Color");
                return Ok("Color");
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return StatusCode(500);
            };
        }

        [HttpPut]
        [Route("update/boardName/{id:int}")]
        public async Task<ActionResult> PutBoardNameFramework(
      int id, [FromServices] postgresContext context, [FromBody] Board boardBody
   )
        {
            try
            {
                Board? findBoard = await context.Boards.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (findBoard == null)
                {
                    return NotFound();
                }
                var listBoards = await context.Boards.AsNoTracking().Where(x => x.KanbanId == boardBody.KanbanId).ToListAsync();

                foreach (var board in listBoards)
                {
                    if (board.BoardName == boardBody.BoardName)
                    {
                        return Unauthorized("Board ja existente!");
                    }

                }

                findBoard.BoardName = boardBody.BoardName;
                context.Entry(findBoard).State = EntityState.Modified;
                await context.SaveChangesAsync();
                await _chatHub.Clients.All.ReceiveMessage(findBoard);
                return Ok(findBoard);
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
      uint id, [FromServices] postgresContext context, [FromBody] Board boardBody
   )
        {
            try
            {
                Board newBoard = new()
                {
                    Color = boardBody.Color,
                    BoardName = boardBody.BoardName,
                    KanbanId = boardBody.KanbanId,
                    UserId = id,
                };
                context.Add(newBoard);
                await context.SaveChangesAsync();
                await _chatHub.Clients.All.ReceiveMessage(newBoard);
                return Ok(newBoard);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return StatusCode(500);
            };
        }
    }
}
