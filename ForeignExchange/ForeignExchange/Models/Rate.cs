using System;
using System.Collections.Generic;
using System.Text;

namespace ForeignExchange.Models
{
    public class Rate
    {
        public Rate(int rateId, string code, double taxRate, string name)
        {
            RateId = rateId;
            Code = code;
            TaxRate = taxRate;
            Name = name;
        }

        public int RateId { get; set; }
        public string Code { get; set; }
        public double TaxRate { get; set; }
        public string Name { get; set; }
    }
}
