using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace TestTimerTrigger
{
    public static class Function1
    {
        private static readonly Random rand = new();

        [FunctionName("Function1")]
        public static async Task Run(
            [TimerTrigger("10 * * * * *", RunOnStartup = true)] TimerInfo myTimer,
            ILogger log)
        {
            log.LogInformation("C# timer triggerd.");
            await Task.Delay(TimeSpan.FromSeconds(1));
            int nxt = rand.Next(100);
            if (nxt % 2 == 1)
            {
                log.LogWarning("1/2 chance to get warning");
            }
            if (nxt % 3 == 1)
            {
                log.LogError("1/3 chance to get error");
                throw new Exception("Gen a error message");
            }
        }
    }
}
