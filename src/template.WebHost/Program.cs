using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types.Enums;
using template.Handlers;
using template.Handlers.CommonHandlers;
using template.Handlers.Stages;
using template.Infrastructure;
using template.WebHost.Settings;
using TgBotFramework;
using TgBotFramework.Webhook;
using TgBotFramework.WrapperExtensions;

var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseInMemoryDatabase("in-mem-prod-database");
    });
    builder.Services.AddScoped<StageHandler>();
    
    builder.Services.AddScoped<EmptyStage>();
    builder.Services.AddScoped<InterviewStage>();
    builder.Services.AddScoped<OnMessageHandler>();
    builder.Services.AddScoped<OnEditedMessageHandler>();
    builder.Services.AddScoped<OnPollHandler>();
    builder.Services.AddScoped<OnPollAnswerHandler>();
    builder.Services.AddScoped<OnChannelPostHandler>();
    builder.Services.AddScoped<OnEditedChannelPostHandler>();
    builder.Services.AddScoped<OnPreCheckoutQueryHandler>();
    builder.Services.AddScoped<OnShippingQueryHandler>();
    builder.Services.AddScoped<OnChatMemberHandler>();
    builder.Services.AddScoped<OnChatJoinRequestHandler>();
    builder.Services.AddScoped<OnMyChatMemberHandler>();
    builder.Services.AddScoped<OnCallbackQueryHandler>();
    builder.Services.AddScoped<OnInlineQueryHandler>();
    builder.Services.AddScoped<OnChosenInlineResultHandler>();
    builder.Services.AddScoped<StartCommand>();

builder.Services.AddBotService<BotContext>(new BotSettings() { ApiToken = MyBotSettings.ApiToken}, 
            builder => builder
        .UseWebhook(new WebhookSettings() { WebhookDomain = MyBotSettings.WebhookDomain, WebhookPath = MyBotSettings.WebhookPath, DebugOutput = true })
        .UseMiddleware<StageHandler>()
        .SetPipeline(pipeBuilder => pipeBuilder
                //small dialog example
            .MapWhen(x=>x.ChatStage.Stage != Stage.User && x.Update.GetChat()?.Type == ChatType.Private,
                pipelineBuilder =>pipelineBuilder 
                    .MapWhen<EmptyStage>(x=>x.ChatStage.Stage == Stage.Empty)
                    .MapWhen<InterviewStage>(x=> x.ChatStage.Stage == Stage.Interview)
                )
            //message related 
            .UseWhen(In.PrivateChat, pipelineBuilder => pipelineBuilder
                .UseCommand<StartCommand>("start")
            )
            .MapWhen<OnMessageHandler>(On.Message)
            .MapWhen<OnEditedMessageHandler>(On.EditedMessage)
            // poll related
            .MapWhen<OnPollHandler>(On.Poll)
            .MapWhen<OnPollAnswerHandler>(On.PollAnswer)
            // channel related
            .MapWhen<OnChannelPostHandler>(On.ChannelPost)
            .MapWhen<OnEditedChannelPostHandler>(On.EditedChannelPost)
            // payments
            .MapWhen<OnPreCheckoutQueryHandler>(On.PreCheckoutQuery)
            .MapWhen<OnShippingQueryHandler>(On.ShippingQuery)
            // chats
            .MapWhen<OnChatMemberHandler>(On.ChatMember)
            .MapWhen<OnChatJoinRequestHandler>(On.ChatJoinRequest)
            .MapWhen<OnMyChatMemberHandler>(On.MyChatMember)
            // common 
            .MapWhen<OnCallbackQueryHandler>(On.CallbackQuery)
            .MapWhen<OnInlineQueryHandler>(On.InlineQuery)
            .MapWhen<OnChosenInlineResultHandler>(On.ChosenInlineResult)
            )
        );
    builder.Services.AddSingleton<StartCommand>();

var app = builder.Build();
app.UseTelegramBotWebhook();

await app.RunAsync(MyBotSettings.LocalHost);