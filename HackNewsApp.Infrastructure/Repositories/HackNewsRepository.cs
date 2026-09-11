using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using HackNewsApp.Application.Contracts.Repositories;
using HackNewsApp.Domain.Entities;
using HackNewsApp.Infrastructure.Http;

using HackNewsApp.Application.DTOs;
using Microsoft.Extensions.Options;

namespace HackNewsApp.Infrastructure.Repositories
{
    public sealed class HackNewsRepository : IHackNewsRepository
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HackNewsRepository> _logger;
        private readonly ExternalApiOptions _options;

        public HackNewsRepository(HttpClient httpClient, ILogger<HackNewsRepository> logger,
            IOptions<ExternalApiOptions> options)
        {
            _httpClient = httpClient;
            _logger = logger;
            _options = options.Value;
        }


        public async Task<IEnumerable<HackNewsItem>> GetHackNewsAsync(HackNewsQueryRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Retrieving top {StoryLimit} Hacker News stories.....",
            request.PageSize);

            var storyIds = await GetStoryIdsAsync(request.PageSize, cancellationToken);

            var tasks = storyIds.Select(id =>
                    GetStoryAsync(id, cancellationToken));

            var stories = await Task.WhenAll(tasks);

            return stories
                .Where(x => x is not null)!
                .Cast<HackNewsItem>();
        }

        public async Task<List<int>> GetStoryIdsAsync(int numberOfStories, CancellationToken cancellationToken)
        {
            StringBuilder storyIdsUrl = new StringBuilder();
            //storyUrl.Append("/v0/beststories.json?orderBy=\"$priority\"&limitToFirst={numberOfStories}")
            storyIdsUrl.Append(_options.StoryIdsUrl);
            storyIdsUrl.Append(numberOfStories.ToString());



            var response = await _httpClient.GetAsync(
                storyIdsUrl.ToString(), cancellationToken);

            response.EnsureSuccessStatusCode();

            var json =
                await response.Content
                    .ReadAsStringAsync(cancellationToken);

            return JsonSerializer.Deserialize<List<int>>(json) ?? [];
        }

        public async Task<HackNewsItem?> GetStoryAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/v0/item/{id}.json", cancellationToken);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync(cancellationToken);

                var story = JsonSerializer.Deserialize<ExternalHackerNewsStoryResponse>(json);

                if (story is null)
                {
                    return null;
                }

                return MapToDomain(story);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed retrieving story {id}", id);

                return null;
            }
        }


        public HackNewsItem MapToDomain(ExternalHackerNewsStoryResponse responseSource)
        {
            return new HackNewsItem
            {
                Id = responseSource.Id,
                Title = responseSource.Title,
                Uri = responseSource.Uri,
                PublishedBy = responseSource.PublishedBy,
                PublishedTime = DateTimeOffset.FromUnixTimeSeconds(responseSource.PublishedUnixTime).UtcDateTime,
                CommentsCount = responseSource.CommentsCount,
                Score = responseSource.Score
            };
        }

    }
}
