using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using T2HackathonCase2.Dtos;
using T2HackathonCase2.Entities;
using T2HackathonCase2.Service.HerePlaceService;
using T2HackathonCase2.Service.UserService;
using T2HackathonCase2.WebhookSetup;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

[ApiController]
[Route("Bot")]
public class BotController : ControllerBase
{
    private readonly BotConfiguration _config;
    private readonly ITelegramBotClient _botClient;
    public readonly IUserDialogService _userDialogService;
    public readonly IUserService _userService;
    public readonly IHerePlaceService _herePlaceService;

    public BotController(IOptions<BotConfiguration> config, ITelegramBotClient botClient, IUserDialogService userDialogService, IHerePlaceService herePlaceService, IUserService userService)
    {
        _config = config.Value;
        _botClient = botClient;
        _userDialogService = userDialogService;
        _herePlaceService = herePlaceService;
        _userService = userService;
    }

    [HttpGet("setWebhook")]
    public async Task<string> SetWebhook(CancellationToken ct)
    {
        var webhookUrl = _config.BotWebhookUrl.AbsoluteUri + "Bot";
        await _botClient.SetWebhookAsync(webhookUrl, cancellationToken: ct);
        return $"Webhook set to {webhookUrl}";
    }

    [HttpPost()]
    public async Task<IActionResult> Post([FromBody] TeleramUpdateResponce update)
    {
        await _userDialogService.HeadleUserInputAsync(update);
        return Ok();
    }
}
