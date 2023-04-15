using kanban_websocket_back.Models;

namespace kanban_websocket_back.Interfaces
{
    public class ILeadUpdate
    {
        public int boardId { get; set; }
#pragma warning disable CS8618 // O propriedade não anulável 'title' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
        public string title { get; set; }
#pragma warning restore CS8618 // O propriedade não anulável 'title' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
#pragma warning disable CS8618 // O propriedade não anulável 'listBoard' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
        public Lead[] listBoard { get; set; }
#pragma warning restore CS8618 // O propriedade não anulável 'listBoard' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
#pragma warning disable CS8618 // O propriedade não anulável 'color' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
        public string color { get; set; }
#pragma warning restore CS8618 // O propriedade não anulável 'color' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.

#pragma warning disable CS8618 // O propriedade não anulável 'propsLead' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
        public string propsLead { get; set; }
#pragma warning restore CS8618 // O propriedade não anulável 'propsLead' precisa conter um valor não nulo ao sair do construtor. Considere declarar o propriedade como anulável.
    }
}
