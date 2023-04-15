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
                    Properties = "{\r\n  \"Informações do Card\": [\r\n    {\r\n      \"label\": \"Título do Card\",\r\n      \"props\": {\r\n        \"required\": true\r\n      }\r\n    },\r\n    {\r\n      \"label\": \"Descrição do Card\",\r\n      \"props\": {\r\n        \"required\": true\r\n      }\r\n    }\r\n  ],\r\n  \"Informações Sobre a Viagem\": [\r\n    {\r\n      \"label\": \"Aeroporto ou Cidade de Origem\",\r\n      \"props\": {\r\n        \"required\": true\r\n      }\r\n    },\r\n    {\r\n      \"label\": \"Aeroporto de Destino\",\r\n      \"props\": {\r\n        \"required\": true\r\n      }\r\n    },\r\n    {\r\n      \"type\": \"date\",\r\n      \"label\": \"Data Prevista ao Embarque\"\r\n    }\r\n  ],\r\n  \"Identificação do Solicitante\": [\r\n    {\r\n      \"label\": \"E-mail\",\r\n      \"props\": {\r\n        \"type\": \"email\",\r\n        \"required\": true\r\n      }\r\n    },\r\n    {\r\n      \"label\": \"Selecione o Serviço Desejado\",\r\n      \"props\": {\r\n        \"required\": true\r\n      }\r\n    },\r\n    {\r\n      \"label\": \"Como você conheceu a Petwork Travel?\",\r\n      \"props\": {\r\n        \"required\": true\r\n      }\r\n    },\r\n    {\r\n      \"label\": \"Nome Completo\",\r\n      \"props\": {\r\n        \"required\": true\r\n      }\r\n    },\r\n    {\r\n      \"type\": \"number\",\r\n      \"label\": \"Número de Telefone Para Contato\",\r\n      \"props\": {\r\n        \"required\": true\r\n      }\r\n    }\r\n  ],\r\n  \"Informações Sobre o(s) Animal(ais)\": [\r\n    {\r\n      \"label\": \"Quantidade de Animais\",\r\n      \"props\": {\r\n        \"required\": true\r\n      }\r\n    },\r\n    {\r\n      \"label\": \"Nome(s) do(s) Animal(ais)\"\r\n    },\r\n    {\r\n      \"label\": \"Espécie(s)\",\r\n      \"props\": {\r\n        \"required\": true\r\n      }\r\n    },\r\n    {\r\n      \"label\": \"Raça(s)\",\r\n      \"props\": {\r\n        \"required\": true\r\n      }\r\n    },\r\n    {\r\n      \"label\": \"Sexo(s)\"\r\n    },\r\n    {\r\n      \"label\": \"Data(s) de Nascimento\"\r\n    },\r\n    {\r\n      \"label\": \"Peso (kg)\"\r\n    },\r\n    {\r\n      \"label\": \"Medidas do(s) Animal(ais)\",\r\n      \"props\": {\r\n        \"required\": true\r\n      }\r\n    },\r\n    {\r\n      \"label\": \"Medidas da(s) Caixa(s) de Transporte\"\r\n    },\r\n    {\r\n      \"label\": \"Endereço do(s) Animal(ais)\"\r\n    }\r\n  ]\r\n}",
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
