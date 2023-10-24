using template.Infrastructure;
using TgBotFramework;

namespace template.Handlers.CommonHandlers;

public class OnChatJoinRequestHandler : IUpdateHandler<BotContext>
{
    public Task HandleAsync(BotContext context, UpdateDelegate<BotContext> next, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}