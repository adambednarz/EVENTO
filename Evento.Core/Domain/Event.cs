using System;
using System.Collections.Generic;
using System.Linq;

namespace Evento.Core.Domain
{
    public class Event : Entity
    {
        private ISet<Ticket> _tickets = new HashSet<Ticket>();
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime StartDate { get; protected set; }
        public DateTime EndDate { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public IEnumerable<Ticket> Tickets => _tickets;
        public IEnumerable<Ticket> PurchasedTickets => _tickets.Where(x => x.Purchased);
        public IEnumerable<Ticket> AvailableTickets => _tickets.Except(PurchasedTickets);
        //public int TicketAmount { get; protected set; }

        protected Event()
        {
        }
        public Event(Guid id, string name, string description, 
            DateTime startDate, DateTime endDate)
        {
            Id = id;
            SetName(name);
            SetDescription(description);
            SetDate(startDate, endDate);
            CreatedAt = DateTime.UtcNow;
        }

        //========== Setting and validation methods ==========
        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Value of name can not be null or empty");

            Name = name;
        }

        public void SetDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new Exception("Value of name can not be null or empty");

            Description = description;
        }

        public void SetDate(DateTime start, DateTime end)
        {
            if (end <= start)
                throw new Exception("Start date can not be grater than end date");

            StartDate = start;
            EndDate = end;
        }

        //========== Other methods ==========
        public void AddTickets(int amount, decimal price)
        {
            var seating = _tickets.Count + 1;
            for (int i = 0; i < amount; i++)
            {
                _tickets.Add(new Ticket(this, price, seating));
                seating++;
            }
        }

        public void  PurchaseTickets(User user, int amount)
        {
            if (AvailableTickets.Count() < amount)
                throw new Exception("Not enough available tickets to purchase.");

            var tickets = AvailableTickets.Take(amount);
            foreach (var ticket in tickets)
            {
                ticket.Purchase(user);
            }
        }

        public void CancelPurchasedTickets(User user, int amount)
        {
            var tickets = GetTicketsPurchasedByUser(user);
            if (tickets.Count() < amount)
                throw new Exception($"Not enough purchased tickets to be canceled '{amount}' by; '{user.Name}'.");
            tickets = tickets.TakeLast(amount);

            foreach (var ticket in tickets)
            {
                ticket.Cancel();
            }
        }

        public IEnumerable<Ticket> GetTicketsPurchasedByUser(User user)
            => PurchasedTickets.Where(x => x.UserId == user.Id);
    }
}
