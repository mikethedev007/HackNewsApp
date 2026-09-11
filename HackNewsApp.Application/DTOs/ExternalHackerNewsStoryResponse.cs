using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace HackNewsApp.Application.DTOs
{
    public sealed class ExternalHackerNewsStoryResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Uri { get; set; } = string.Empty;

        [JsonPropertyName("by")]
        public string PublishedBy { get; set; } = string.Empty;

        [JsonPropertyName("time")]
        public long PublishedUnixTime { get; set; }

        [JsonPropertyName("descendants")]
        public int CommentsCount { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }
    }
}
