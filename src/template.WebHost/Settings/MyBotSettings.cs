using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace template.WebHost.Settings
{
    public static class MyBotSettings
    {
        public static string ApiToken { get; set; } = Environment.GetEnvironmentVariable("BOT_TOKEN") ?? default!;
        public static string WebhookDomain { get; set; } = Environment.GetEnvironmentVariable("BOT_WEBHOOK_DOMAIN") ?? default!;
        public static string WebhookPath { get; set; } = Environment.GetEnvironmentVariable("BOT_WEBHOOK_PATH") ?? default!;
        public static string LocalHost { get; set; } = Environment.GetEnvironmentVariable("BOT_LOCALHOST") ?? default!;
    }
}