using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H12.Logger
{
    public class ConsoleLogger : Logger
    {
        public override async Task Log(LogMessage message)
        {
            // Using Task.Run() in case there are any long running actions, to prevent blocking gateway
            Task.Run(() => LogToConsoleAsync(this, message));
        }

        private async Task LogToConsoleAsync<T>(T logger, LogMessage message) where T : ILogger
        {
            Console.WriteLine($"guid:{_guid} : " + message);
        }
    }
}
