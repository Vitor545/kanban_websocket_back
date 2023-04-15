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
                Board board = new()
                {
                    BoardName = "Initial Board",
                    UserId = user.Id,
                    KanbanId = 1,
                    Color = "#CD8DE5",
                };
                context.Boards.Add(board);
                await context.SaveChangesAsync();
                Formmodel form = new()
                {
                    Properties = "{\"Informações do Card\":[{\"label\":\"Título do Card\",\"props\":{\"required\":true}},{\"label\":\"Descrição do Card\",\"props\":{\"required\":true}}],\"Informações Sobre a Viagem\":[{\"label\":\"Aeroporto ou Cidade de Origem\",\"props\":{\"required\":true}},{\"label\":\"Aeroporto de Destino\",\"props\":{\"required\":true}},{\"type\":\"date\",\"label\":\"Data Prevista ao Embarque\"}],\"Identificação do Solicitante\":[{\"label\":\"E-mail\",\"props\":{\"type\":\"email\",\"required\":true}},{\"label\":\"Selecione o Serviço Desejado\",\"props\":{\"required\":true}},{\"label\":\"Como você conheceu a Petwork Travel?\",\"props\":{\"required\":true}},{\"label\":\"Nome Completo\",\"props\":{\"required\":true}},{\"type\":\"number\",\"label\":\"Número de Telefone Para Contato\",\"props\":{\"required\":true}}],\"Informações Sobre o(s) Animal(ais)\":[{\"label\":\"Quantidade de Animais\",\"props\":{\"required\":true}},{\"label\":\"Nome(s) do(s) Animal(ais)\"},{\"label\":\"Espécie(s)\",\"props\":{\"required\":true}},{\"label\":\"Raça(s)\",\"props\":{\"required\":true}},{\"label\":\"Sexo(s)\"},{\"label\":\"Data(s) de Nascimento\"},{\"label\":\"Peso (kg)\"},{\"label\":\"Medidas do(s) Animal(ais)\",\"props\":{\"required\":true}},{\"label\":\"Medidas da(s) Caixa(s) de Transporte\"},{\"label\":\"Endereço do(s) Animal(ais)\"}]}",
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
