using TgBotFramework;

namespace template.Infrastructure;

public class BotContext : UpdateContext
{
    public ChatStage ChatStage { get; set; }
}