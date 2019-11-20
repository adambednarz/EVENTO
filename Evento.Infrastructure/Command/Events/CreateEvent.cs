using System;
using System.Collections.Generic;
using System.Text;

namespace Evento.Infrastructure.Command.Events
{
    public class CreateEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TicketsAmount { get; set; }
        public decimal TiketPrice { get; set; }
    }
}
