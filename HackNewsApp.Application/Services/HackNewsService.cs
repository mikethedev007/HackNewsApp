using HackNewsApp.Application.Common;
using HackNewsApp.Application.Contracts.Caching;
using HackNewsApp.Application.Contracts.Repositories;
using HackNewsApp.Application.Contracts.Services;
using HackNewsApp.Application.DTOs;
using HackNewsApp.Application.Mappers;
using HackNewsApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace HackNewsApp.Application.Services
{
    public sealed class HackNewsService : ServiceBase, IHackNewsService
    {
        private readonly IHackNewsRepository _repository;
        private readonly ICacheService _cache;
        private const string CacheKey = "hacknews-items";
        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5);


        public HackNewsService(IHackNewsRepository repository, ICacheService cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<Result<PagedResult<HackNewsItemDto>>> GetNewsAsync(HackNewsQueryRequest request,
            CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async () =>
            {
                IEnumerable<HackNewsItem> newsItems;

                //Try to retrieve data from cache instead of from external api if it has not expired
                if (!_cache.TryGetValue(CacheKey, out newsItems))
                {
                    newsItems = await _repository.GetHackNewsAsync(request, cancellationToken);

                    _cache.Set(CacheKey, newsItems, CacheDuration);
                }

                var query = HackNewsMapper
                                .ToDtoCollection(newsItems)
                                .AsQueryable();

                //filter the data

                //filter on published by value
                if (!string.IsNullOrWhiteSpace(request.PublishedBy))
                {
                    query = query.Where(x =>
                        x.PublishedBy.Equals(request.PublishedBy, StringComparison.OrdinalIgnoreCase)
                    );
                }

                //filter on minimum score value
                if (request.MinimumScore.HasValue)
                {
                    query = query.Where(x =>
                    x.Score >= request.MinimumScore.Value);
                }

                //filter on maximum score value
                if (request.MaximumScore.HasValue)
                {
                    query = query.Where(x =>
                    x.Score <= request.MaximumScore.Value);
                }

                //Sorting

                query = request.SortByField?.ToLowerInvariant()
                switch
                {
                    "score" => query.OrderBy(x => x.Score),
                    "score_desc" => query.OrderByDescending(x => x.Score),
                    "title" => query.OrderBy(x => x.Title),
                    "title_desc" => query.OrderByDescending(x => x.Title),
                    "publishedtime" => query.OrderBy(x => x.PublishedTime),
                    "publishedtime_desc" => query.OrderByDescending(x => x.PublishedTime),
                    _ => query.OrderByDescending(x => x.Score)
                };


                //Pagination

                var totalCount = query.Count();

                var items = query
                                .Skip((request.PageNumber - 1) * request.PageSize)
                                .Take(request.PageSize)
                                .ToList();

                var response = new PagedResult<HackNewsItemDto>
                {
                    Items = items,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
                };

                return response;

            });
        }
    }
}
