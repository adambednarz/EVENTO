﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Evento.Infrastructure.Dto
{
    public class JwtDto
    {
        public string Token { get; set; }
        public long Expires { get; set; }
    }
}
