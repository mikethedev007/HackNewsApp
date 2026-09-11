using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackNewsApp.Infrastructure.Http
{
    public sealed class ExternalApiOptions
    {
        public const string SectionName = "ExternalApi";
        public string BaseUrl { get; set; } = string.Empty;
        public string StoryIdsUrl { get; set; } = string.Empty;
        public string StoryDetailsUrl { get; set; } = string.Empty;
        public int TimeoutSeconds { get; set; } = 30;
        public int MaxConcurrentRequests { get; set; } = 5;
        public int StoryLimit { get; set; } = 10;

    }
}
