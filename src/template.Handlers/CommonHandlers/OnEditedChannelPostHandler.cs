using TgBotFramework;
using template.Infrastructure;

namespace template.Handlers.CommonHandlers;

public class OnEditedChannelPostHandler : IUpdateHandler<BotContext>
{
    public Task HandleAsync(BotContext context, UpdateDelegate<BotContext> next, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}