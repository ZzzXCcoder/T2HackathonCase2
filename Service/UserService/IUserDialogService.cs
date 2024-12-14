using T2HackathonCase2.Dtos;

namespace T2HackathonCase2.Service.UserService
{
    public interface IUserDialogService
    {
        public Task HeadleUserInputAsync(TeleramUpdateResponce update);
        Task HeandleUserTextMessageAsync(TeleramUpdateResponce update);

        Task HandleCallbackQueryAsync(TeleramUpdateResponce update);

        Task HandleLocationAsync(TeleramUpdateResponce update);
    }
}
