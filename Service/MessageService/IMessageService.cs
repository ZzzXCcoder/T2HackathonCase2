using Telegram.Bot.Types.ReplyMarkups;

namespace T2HackathonCase2.Service.MessageService
{
    public interface IMessageService
    {
        public string GetMessage(string key);

        public InlineKeyboardMarkup GetKeyboard(string key);

        public InlineKeyboardMarkup GetKeyboard(long key);
        public ReplyKeyboardMarkup GetReplyKeyboard(string key);
    }
}
