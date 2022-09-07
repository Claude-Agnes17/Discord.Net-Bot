using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H12.Logger
{
    public interface ILogger
    {
        public Task Log(LogMessage message);
    }
}
