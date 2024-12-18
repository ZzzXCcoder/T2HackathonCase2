using T2HackathonCase2.Data;
using T2HackathonCase2.Dtos;
using T2HackathonCase2.Service.UserService;
using Telegram.Bot;
using T2HackathonCase2.Service.MessageService;
using Telegram.Bot.Types.Enums;

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
        if (update.CallbackQuery.Data == "get_place")
        {
            var location = await _userService.FindUserLocation((long)update.CallbackQuery.Message.Chat.Id, 0);
            if (location != null) // Проверяем, что локация найдена
            {
                // Отправляем точку на карте
                await _botClient.SendLocationAsync(
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    latitude: location.Latitude,   // Широта
                    longitude: location.Longitude, // Долгота
                    replyMarkup: _messageService.GetKeyboard("received_location_foruser") // Клавиатура
                );
            }
            if (location != null) // Проверяем, что локация найдена
            {
                if (!string.IsNullOrEmpty(location.ImageURL)) // Если URL изображения не пустой
                {
                    await _botClient.SendPhotoAsync(
                        chatId: update.CallbackQuery.Message.Chat.Id,
                        photo: location.ImageURL, // Ссылка на изображение
                        caption: $"<b>Название:</b> {location.Name}\n" +
                                 $"<i>Адрес:</i> {location.Description}\n" +
                                 $"<i>Категория:</i> {location.Category}", // Текст с HTML форматированием
                        parseMode: ParseMode.Html, // Используем HTML для форматирования текста 
                        replyMarkup: _messageService.GetKeyboard("received_location_foruser") // Клавиатура с кнопками
                    );
                }
                else // Если URL изображения пустой
                {
                    await _botClient.SendTextMessageAsync(
                        chatId: update.CallbackQuery.Message.Chat.Id,
                        text: $"<b>Название:</b> {location.Name}\n" +
                              $"<i>Адрес:</i> {location.Description}\n" +
                              $"<i>Категория:</i> {location.Category}", // Текст с HTML форматированием
                        parseMode: ParseMode.Html, // Используем HTML для форматирования текста
                        replyMarkup: _messageService.GetKeyboard("received_location_foruser") // Клавиатура с кнопками
                    );
                }
            }
            else
            {
                // Обрабатываем случай, когда локация не найдена
                await _botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id,
                    "Локация не найдена.",
                    replyMarkup: _messageService.GetKeyboard("received_location_foruser"));
            }
        }
        if (update.CallbackQuery.Data == "received_location_foruser")
        {
            var location = await _userService.FindUserLocation((long)update.CallbackQuery.Message.Chat.Id, 0);
            if (location != null) // Проверяем, что локация найдена
            {
                if (!string.IsNullOrEmpty(location.ImageURL)) // Если URL изображения не пустой
                {
                    await _botClient.SendPhotoAsync(
                        chatId: update.CallbackQuery.Message.Chat.Id,
                        photo: location.ImageURL, // Ссылка на изображение
                        caption: $"<b>Название:</b> {location.Name}\n" +
                                 $"<i>Адрес:</i> {location.Description}\n" +
                                 $"<i>Категория:</i> {location.Category}", // Текст с HTML форматированием
                        parseMode: ParseMode.Html, // Используем HTML для форматирования текста
                        replyMarkup: _messageService.GetKeyboard("received_location_foruser") // Клавиатура с кнопками
                    );
                }
                else // Если URL изображения пустой
                {
                    await _botClient.SendTextMessageAsync(
                        chatId: update.CallbackQuery.Message.Chat.Id,
                        text: $"<b>Название:</b> {location.Name}\n" +
                              $"<i>Адрес:</i> {location.Description}\n" +
                              $"<i>Категория:</i> {location.Category}", // Текст с HTML форматированием
                        parseMode: ParseMode.Html, // Используем HTML для форматирования текста
                        replyMarkup: _messageService.GetKeyboard("received_location_foruser") // Клавиатура с кнопками
                    );
                }
            }
            else
            {
                // Обрабатываем случай, когда локация не найдена
                await _botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id,
                    "Локация не найдена.",
                    replyMarkup: _messageService.GetKeyboard("received_location_foruser"));
            }
        }
        if (update.CallbackQuery.Data == "next_location")
        {
            var nextLocation = await _userService.FindUserLocation((long)update.CallbackQuery.Message.Chat.Id, 1); // Получаем следующую локацию
            if (nextLocation != null) // Проверяем, что локация найдена
            {
                if (!string.IsNullOrEmpty(nextLocation.ImageURL)) // Если URL изображения не пустой
                {
                    await _botClient.SendPhotoAsync(
                        chatId: update.CallbackQuery.Message.Chat.Id,
                        photo: nextLocation.ImageURL, // Ссылка на изображение
                        caption: $"<b>Название:</b> {nextLocation.Name}\n" +
                                 $"<i>Адрес:</i> {nextLocation.Description}\n" +
                                 $"<i>Категория:</i> {nextLocation.Category}", // Текст с HTML форматированием
                        parseMode: ParseMode.Html, // Используем HTML для форматирования текста
                        replyMarkup: _messageService.GetKeyboard("received_location_foruser") // Клавиатура с кнопками
                    );
                }
                else // Если URL изображения пустой
                {
                    await _botClient.SendTextMessageAsync(
                        chatId: update.CallbackQuery.Message.Chat.Id,
                        text: $"<b>Название:</b> {nextLocation.Name}\n" +
                              $"<i>Адрес:</i> {nextLocation.Description}\n" +
                              $"<i>Категория:</i> {nextLocation.Category}", // Текст с HTML форматированием
                        parseMode: ParseMode.Html, // Используем HTML для форматирования текста
                        replyMarkup: _messageService.GetKeyboard("received_location_foruser") // Клавиатура с кнопками
                    );
                }
            }
            else
            {
                        // Обрабатываем случай, когда следующая локация не найдена
                await _botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id,
                "Следующая локация не найдена.",
                 replyMarkup: _messageService.GetKeyboard("received_location_foruser"));
            }
        }
        if (update.CallbackQuery.Data == "back_location")
        {
            var backLocation = await _userService.FindUserLocation((long)update.CallbackQuery.Message.Chat.Id, -1); // Получаем следующую локацию
            if (backLocation != null) // Проверяем, что локация найдена
            {
                if (!string.IsNullOrEmpty(backLocation.ImageURL)) // Если URL изображения не пустой
                {
                    await _botClient.SendPhotoAsync(
                        chatId: update.CallbackQuery.Message.Chat.Id,
                        photo: backLocation.ImageURL, // Ссылка на изображение
                        caption: $"<b>Название:</b> {backLocation.Name}\n" +
                                 $"<i>Адрес:</i> {backLocation.Description}\n" +
                                 $"<i>Категория:</i> {backLocation.Category}", // Текст с HTML форматированием
                        parseMode: ParseMode.Html, // Используем HTML для форматирования текста
                        replyMarkup: _messageService.GetKeyboard("received_location_foruser") // Клавиатура с кнопками
                    );
                }
                else // Если URL изображения пустой
                {
                    await _botClient.SendTextMessageAsync(
                        chatId: update.CallbackQuery.Message.Chat.Id,
                        text: $"<b>Название:</b> {backLocation.Name}\n" +
                              $"<i>Адрес:</i> {backLocation.Description}\n" +
                              $"<i>Категория:</i> {backLocation.Category}", // Текст с HTML форматированием
                        parseMode: ParseMode.Html, // Используем HTML для форматирования текста
                        replyMarkup: _messageService.GetKeyboard("received_location_foruser") // Клавиатура с кнопками
                    );
                }
            }
            else
            {
                    // Обрабатываем случай, когда следующая локация не найдена
                    await _botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id,
                        "Следующая локация не найдена.",
                        replyMarkup: _messageService.GetKeyboard("received_location_foruser"));
            }
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

            await _userService.SetLocationForUser((long)update.Message.Chat.Id);

            await _botClient.SendTextMessageAsync(update.Message.Chat.Id,
               _messageService.GetMessage("get_location_foruser"),
               replyMarkup: _messageService.GetKeyboard("get_location_foruser"));

        }
    }

    public Task HandleLocationAsync(TeleramUpdateResponce update)
    {
        throw new NotImplementedException();
    }
}
