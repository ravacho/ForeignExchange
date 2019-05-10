﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ForeignExchange.Models
{
    public class Response
    {
        public bool IsSuccess
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }
        public string Result
        {
            get;
            set;
        }

    }
}
