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
    [HttpGet("User")]
    public async Task<IActionResult> GetUser(long id)
    {
        var user = await _userService.GetUser(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
    [HttpGet ("GetPlaces")]
    public async Task<List<HerePlaceDto>> GetSuggestedPlacesAsync(string query, double latitude, double longitude, double radius, int limit = 1)
    {
        return await _herePlaceService.GetSuggestedPlacesAsync(query, latitude, longitude, radius, limit);
    }
    [HttpPost("SetLocatiionForUser")]
    public async Task<IActionResult> PostUserPlace(long ChatId, string query, double latitude, double longitude, double radius, int limit = 1)
    {
        await _userService.SetLocationForUser(ChatId);
        return Ok();
    }
    [HttpPost("GetUserLocation")]
    public async Task<IActionResult> GetUserLocation(long ChatId)
    {
        await _userService.FindUserLocation(ChatId, 0);
        return Ok();
    }
}
