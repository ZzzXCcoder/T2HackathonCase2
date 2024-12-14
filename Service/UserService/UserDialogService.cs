using T2HackathonCase2.Data;
using T2HackathonCase2.Dtos;
using T2HackathonCase2.Service.UserService;
using Telegram.Bot;
using T2HackathonCase2.Service.MessageService;

public class UserDialogService : IUserDialogService
{
    private readonly ITelegramBotClient _botClient;
    private readonly WeekendWayDbContext _dbcontext;
    private readonly IMessageService _messageService; // Добавляем зависимость от MessageService
    private readonly IUserService _userService;

    public UserDialogService(ITelegramBotClient botClient, WeekendWayDbContext dbcontext, IMessageService messageService, IUserService userService)
    {
        _botClient = botClient;
        _dbcontext = dbcontext;
        _messageService = messageService;
        _userService = userService; 
    }

    public async Task HeadleUserInputAsync(TeleramUpdateResponce update)
    {
        if (update.CallbackQuery != null)
        {
            await HandleCallbackQueryAsync(update);
        }
        if (update.Message != null)
        {
            await HeandleUserTextMessageAsync(update);
        }
    }

    public async Task HandleCallbackQueryAsync(TeleramUpdateResponce update)
    {
        if (update.CallbackQuery.Data == "start_markup")
        {
            await _botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id,
                _messageService.GetMessage("event_type_prompt"), replyMarkup: _messageService.GetKeyboard("event_type_keyboard"));
        }
        if (update.CallbackQuery.Data.StartsWith("for"))
        {
            await _botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id,
                _messageService.GetMessage("days_prompt"));
            await _userService.SetAtributeAsync((long)update.CallbackQuery.Message.Chat.Id, update.CallbackQuery.Data);
        }
    }

    public async Task HeandleUserTextMessageAsync(TeleramUpdateResponce update)
    {

        switch (update.Message.Text)
        {
            case "/start":
                // Используем сервис сообщений для получения текста и клавиатуры
                await _botClient.SendTextMessageAsync(update.Message.Chat.Id,
                    _messageService.GetMessage("start_intro"), replyMarkup: _messageService.GetKeyboard("start_keyboard"));
                await _userService.Register(update);
                break;
        }
        if (int.TryParse(update.Message.Text, out int result))
        {
            await _userService.SetAtributeAsync((long)update.Message.Chat.Id, result);
            await _botClient.SendTextMessageAsync(update.Message.Chat.Id, _messageService.GetMessage("send_location_promt"), replyMarkup: _messageService.GetReplyKeyboard("location_keyboard"));
        }
        if (update.Message.Location != null)
        {
            await _userService.SetAtributeAsync((long)update.Message.Chat.Id, (double)update.Message.Location.Latitude, (double)update.Message.Location.Longitude);
            await _botClient.SendTextMessageAsync(update.Message.Chat.Id, "Заебись");
        }
    }

    public Task HandleLocationAsync(TeleramUpdateResponce update)
    {
        throw new NotImplementedException();
    }
}
