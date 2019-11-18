﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Evento.Infrastructure.Dto
{
    public class TicketDto
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string Username { get; set; }
        public DateTime? PurchasedAt { get; set; }
        public decimal Price { get; set; }
        public int Seating { get; set; }
        public bool Purchased { get; set; }

    }
}
