using System;
using System.Collections.Generic;
using System.Text;
using Logger.Data.EF.Common;

namespace Logger.Data.EF
{
    public class Log : BaseEntity
    {
        public string Message { get; set; }
        public string Logger { get; set; }
    }
}
