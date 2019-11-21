using System;
using System.Collections.Generic;
using System.Text;

namespace Evento.Core.Domain
{
    public class Ticket : Entity
    {
        public Guid EventId { get; protected set; }
        public Guid? UserId { get; protected set; }
        public string Username { get; protected set; }
        public DateTime? PurchasedAt { get; protected set; }
        public decimal Price { get; protected set; }
        public int Seating { get; protected set; }
        public bool Purchased => UserId.HasValue;

        protected Ticket()
        {
        }

        public Ticket(Event @event, decimal price, int seating)
        {
            EventId = @event.Id;
            Price = price;
            Seating = seating;
        }

        public void Purchase(User user)
        {
            if (Purchased)
                throw new Exception($"Ticket was already purchaserd by user: '{Username}, at: '{PurchasedAt}'");

            PurchasedAt = DateTime.Now;
            Username = user.Name;
            UserId = user.Id;
        }

        public void Cancel()
        {
            if (!Purchased)
                throw new Exception($"Ticket was not already purchaserd and can not be canceled.");

            PurchasedAt = null;
            Username = null;
            UserId = null;
        }
    }
}
