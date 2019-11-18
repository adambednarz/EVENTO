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
        //public int TicketAmount { get; protected set; }
        public IEnumerable<Ticket> AvaliableTickets => Tickets.Except(PurchasedTickets);
        public IEnumerable<Ticket> PurchasedTickets => Tickets.Where(x => x.Purchased == true);


        protected Event()
        {
        }
        public Event(Guid id, string name, string description, 
            DateTime startDate, DateTime endDate)
        {
            Id = id;
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Name can not be empty");

            Name = name;
        }

        public void SetDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new Exception("Name can not be empty");

            Description = description;
        }

        public void AddTickets(int amount, decimal price)
        {
            var seating = _tickets.Count + 1;
            for (int i = 0; i < amount; i++)
            {
                _tickets.Add(new Ticket(this, price, seating));
                seating++;
            }
        }
    }
}
