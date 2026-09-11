using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackNewsApp.Application.DTOs
{
    public sealed class HackNewsQueryRequest
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public string? PublishedBy { get; init; }
        public int? MinimumScore { get; init; }
        public int? MaximumScore { get; init; }
        public string? SortByField { get; init; } = "score_desc";
    }
}
