using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackNewsApp.Application.DTOs;
using HackNewsApp.Domain.Entities;

namespace HackNewsApp.Application.Mappers
{
    public static class HackNewsMapper
    {
        public static HackNewsItemDto ToDto(HackNewsItem entity)
        => new(entity.Id, entity.Title, entity.Uri, entity.PublishedBy, entity.PublishedTime,
            entity.CommentsCount, entity.Score);

        public static IEnumerable<HackNewsItemDto> ToDtoCollection(IEnumerable<HackNewsItem> entities)
            => entities.Select(ToDto);
    }
}
