using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace T2HackathonCase2.Dtos
{
    public class TeleramUpdateResponce
    {
        [JsonPropertyName("update_id")]
        public long? UpdateId { get; set; }

        [JsonPropertyName("message")]
        public Message? Message { get; set; }

        [JsonPropertyName("callback_query")]
        public CallbackQuery? CallbackQuery { get; set; } // Новый объект для обработки callback'ов
    }

    public class CallbackQuery
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("from")]
        public TelegramUser? From { get; set; }

        [JsonPropertyName("message")]
        public Message? Message { get; set; }

        [JsonPropertyName("chat")]
        public Chat? Chat { get; set; }

        [JsonPropertyName("date")]
        public int? Date { get; set; }

        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("chat_instance")]
        public string? ChatInstance { get; set; }

        [JsonPropertyName("data")]
        public string? Data { get; set; }
    }

    public class Message
    {
        [JsonPropertyName("message_id")]
        public int? MessageId { get; set; }

        [JsonPropertyName("from")]
        public TelegramUser? From { get; set; }

        [JsonPropertyName("chat")]
        public Chat? Chat { get; set; }

        [JsonPropertyName("date")]
        public int? Date { get; set; }

        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("entities")]
        public List<Entity>? Entities { get; set; }

        [JsonPropertyName("location")]
        public Location? Location { get; set; }

        [JsonPropertyName("reply_markup")]
        public ReplyMarkup? ReplyMarkup { get; set; }
    }

    public class ReplyMarkup
    {
        [JsonPropertyName("inline_keyboard")]
        public List<List<InlineKeyboardButtons>>? InlineKeyboard { get; set; }
    }

    public class InlineKeyboardButtons
    {
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("callback_data")]
        public string? CallbackData { get; set; }
    }

    public class TelegramUser
    {
        [JsonPropertyName("id")]
        public long? Id { get; set; }

        [JsonPropertyName("is_bot")]
        public bool? IsBot { get; set; }

        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("language_code")]
        public string? LanguageCode { get; set; }
    }

    public class Chat
    {
        [JsonPropertyName("id")]
        public long? Id { get; set; }

        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }

    public class Entity
    {
        [JsonPropertyName("offset")]
        public int? Offset { get; set; }

        [JsonPropertyName("length")]
        public int? Length { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("latitude")]
        public double? Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double? Longitude { get; set; }
    }

    public class Venue
    {
        [JsonPropertyName("location")]
        public Location? Location { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("foursquare_id")]
        public string? FoursquareId { get; set; }

        [JsonPropertyName("foursquare_type")]
        public string? FoursquareType { get; set; }
    }
}
