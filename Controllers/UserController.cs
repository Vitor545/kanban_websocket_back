using kanban_websocket_back.Data;
using kanban_websocket_back.Models;
using kanban_websocket_back.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kanban_websocket_back.Controllers
{
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly Token _token = new Token();


        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<User>> PostCreateUser(
         int id, [FromBody] User userData, [FromServices] postgresContext context
      )
        {
            try
            {
                User? findUserEmail = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == userData.Email);
                if (findUserEmail != null)
                {
                    return Ok("Email já cadastrado!");
                }
                User user = new()
                {
                    Email = userData.Email,
                    Password = userData.Password,
                    Name = userData.Name,
                };
                context.Users.Add(user);
                await context.SaveChangesAsync();
                Kanban kanban = new()
                {
                    KanbanName = "Initial Kanban",
                    UserId = user.Id,
                };
                context.Kanbans.Add(kanban);
                await context.SaveChangesAsync();
                Board board = new()
                {
                    BoardName = "Initial Board",
                    UserId = user.Id,
                    KanbanId = kanban.Id,
                    Color = "#CD8DE5",
                };
                context.Boards.Add(board);
                await context.SaveChangesAsync();
                Formmodel form = new()
                {
                    UserId = user.Id
                };
                context.Formmodels.Add(form);
                await context.SaveChangesAsync();
                return StatusCode(201, "created");
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return StatusCode(200, "Não foi possivel criar o usuário");
            };
        }

    }
}
