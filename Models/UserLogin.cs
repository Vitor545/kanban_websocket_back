using System.ComponentModel.DataAnnotations;


namespace kanban_websocket_back.Models.Login
{
    public class UserLogin
    {
        [Required]
#pragma warning disable CS8618 // O propriedade não anulável 'Email' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
        public string Email { get; set; }
#pragma warning restore CS8618 // O propriedade não anulável 'Email' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.

        [Required]
#pragma warning disable CS8618 // O propriedade não anulável 'Password' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
        public string Password { get; set; }
#pragma warning restore CS8618 // O propriedade não anulável 'Password' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
    }

    public class SendEmailResetPassword
    {
#pragma warning disable CS8618 // O propriedade não anulável 'email' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
        public string email { get; set; }
#pragma warning restore CS8618 // O propriedade não anulável 'email' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.

    }

    public class ResetPassword
    {
#pragma warning disable CS8618 // O propriedade não anulável 'newPassword' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
        public string newPassword { get; set; }
#pragma warning restore CS8618 // O propriedade não anulável 'newPassword' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
#pragma warning disable CS8618 // O propriedade não anulável 'userId' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
        public string userId { get; set; }
#pragma warning restore CS8618 // O propriedade não anulável 'userId' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.

    }

}
