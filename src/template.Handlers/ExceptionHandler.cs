using TgBotFramework;
using Microsoft.Extensions.Logging;
using template.Infrastructure;

namespace template.Handlers;

public class ExceptionHandler : IUpdateHandler<BotContext>
{
    private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(ILogger<ExceptionHandler> logger)
    {
        _logger = logger;
    }
    public async Task HandleAsync(BotContext context, UpdateDelegate<BotContext> next, CancellationToken cancellationToken)
    {
        try
        {
            await next(context, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Top level handler");
        }
    }
}