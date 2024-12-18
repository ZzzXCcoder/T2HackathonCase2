using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using T2HackathonCase2.Data;
using T2HackathonCase2.Repository.UserRepository;
using T2HackathonCase2.Repository.UsersRepository;
using T2HackathonCase2.Service.HerePlaceService;
using T2HackathonCase2.Service.MessageService;
using T2HackathonCase2.Service.UserService;
using T2HackathonCase2.WebhookSetup;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WeekendWayDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<BotConfiguration>(builder.Configuration.GetSection("BotConfiguration"));
builder.Services.AddSingleton<ITelegramBotClient>(sp =>
{
    var botConfig = sp.GetRequiredService<IOptions<BotConfiguration>>().Value;
    return new TelegramBotClient(botConfig.BotToken);
});


builder.Services.AddHttpClient();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddSingleton<IMessageService, MessageService>();

builder.Services.AddScoped<IUserDialogService, UserDialogService>();

builder.Services.AddScoped<IHerePlaceService, HerePlaceService>();

builder.Services.AddSingleton<TelegramBotClient>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var botToken = configuration["Telegram:BotToken"];
    return new TelegramBotClient(botToken);
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin() // Разрешает все источники
        .AllowAnyMethod() // Разрешает все методы (GET, POST, PUT и т.д.)
        .AllowAnyHeader(); // Разрешает все заголовки
    });
});


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.AddDebug();
});
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v2");
        options.RoutePrefix = string.Empty;
    });
}

app.UseRouting();
// Настройка маршрутов
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Use(async (context, next) =>
{
    Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
    await next.Invoke();
});

app.Run();
