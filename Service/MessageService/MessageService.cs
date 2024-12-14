using T2HackathonCase2.Service.MessageService;
using Telegram.Bot.Types.ReplyMarkups;

public class MessageService : IMessageService
{
    private readonly Dictionary<string, string> _messages;
    private readonly Dictionary<string, InlineKeyboardMarkup> _keyboards;
    private readonly Dictionary<string, ReplyKeyboardMarkup> _replyKeyboards;

    public MessageService()
    {
        // Инициализация коллекции сообщений
        _messages = new Dictionary<string, string>
        {
            { "start_intro",
                "Привет! Я — твой помощник по планированию идеальных выходных. " +
                               "🚶‍♂️🌍\r\n\r\nРасскажи мне, с кем ты собираешься провести день, какие у тебя предпочтения, и сколько времени " +
                               "у тебя есть — а я подберу лучшие маршруты для вашего отдыха. Кроме того, могу предложить интересные локации и " +
                               "добавить их вручную. Готов начать? 😎" },

            { "event_type_prompt",
                "Выберите тип мероприятия:" },

            { "days_prompt",
                "Отлично! Теперь, расскажи, сколько дней ты готов посвятить своему маршруту? 📅\r\n" +
                             "Не переживай, я подберу идеальный план в зависимости от времени, которое у тебя есть! ⏳\r\n" +
                             "Напиши, сколько дней у тебя в запасе, и я помогу с выбором подходящего маршрута! 😎 ВВЕДИТЕ ПРОСТО ЧИСЛО(считаю в днях)" },
            { "send_location_promt",
                 "Чтобы я мог подобрать для вас лучшие маршруты и интересные локации рядом с вами, пожалуйста, отправьте вашу текущую геопозицию. " +
                 "Просто нажмите на кнопку ниже! 📍\r\n\r\n" +
                 "Не переживайте, ваша локация используется только для построения маршрутов и никак не сохраняется. 🌟" }
        };

        // Инициализация коллекции клавиатур
        _keyboards = new Dictionary<string, InlineKeyboardMarkup>
        {
            { "start_keyboard", new InlineKeyboardMarkup(new[]
                {
                    new[] // Первая строка с кнопками
                    {
                        InlineKeyboardButton.WithCallbackData("✅ Готов", "start_markup"),
                    }
                })
            },
            { "event_type_keyboard", new InlineKeyboardMarkup(new[]
                {
                    new[] // Первая строка с кнопками
                    {
                        InlineKeyboardButton.WithCallbackData("Для семьи", "for_family"),
                        InlineKeyboardButton.WithCallbackData("Для включённой пары", "for_loving_couple")
                    },
                    new[] // Вторая строка с кнопками
                    {
                        InlineKeyboardButton.WithCallbackData("Для группы друзей", "for_group_of_friends"),
                        InlineKeyboardButton.WithCallbackData("В гордом одиночестве", "for_solo")
                    }
                })
            }
        };
        _replyKeyboards = new Dictionary<string, ReplyKeyboardMarkup>
        {
            {
                "location_keyboard", new ReplyKeyboardMarkup(new[]
                {
                    new[]
                    {
                        new KeyboardButton("📍 Отправить местоположение") { RequestLocation = true }
                    }

                })
                {
                    ResizeKeyboard = true,
                    OneTimeKeyboard = true
                }
            }
        };
    }

    // Получение сообщения по ключу
    public string GetMessage(string key)
    {
        if (_messages.ContainsKey(key))
        {
            return _messages[key];
        }
        else
        {
            throw new KeyNotFoundException($"Сообщение с ключом {key} не найдено.");
        }
    }

    // Получение клавиатуры по ключу
    public InlineKeyboardMarkup GetKeyboard(string key)
    {
        if (_keyboards.ContainsKey(key))
        {
            return _keyboards[key];
        }
        else
        {
            throw new KeyNotFoundException($"Клавиатура с ключом {key} не найдена.");
        }
    }
    public ReplyKeyboardMarkup GetReplyKeyboard(string key)
    {
        if (_replyKeyboards.ContainsKey(key))
        {
            return _replyKeyboards[key];
        }
        else
        {
            throw new KeyNotFoundException($"Клавиатура с ключом {key} не найдена.");
        }
    }
}
