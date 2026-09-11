using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackNewsApp.Application.DTOs
{
    public sealed record HackNewsItemDto
    (
        int? Id,
        string Title,
        string Uri,
        string PublishedBy,
        DateTime PublishedTime,
        int CommentsCount,
        int Score
    );
}
