using System;
using System.Collections.Generic;
using System.Text;

namespace ForeignExchange.Models
{
    public class Rate
    {
        public int RateId { get; set; }
        public string Code { get; set; }
        public string TextRate { get; set; }
        public string Name { get; set; }
    }
}
