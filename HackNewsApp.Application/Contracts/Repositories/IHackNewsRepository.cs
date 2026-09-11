//using HackNewsApp.Infrastructure.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HackNewsApp.Domain.Entities;
using HackNewsApp.Application.DTOs;


namespace HackNewsApp.Application.Contracts.Repositories
{
    public interface IHackNewsRepository
    {
        Task<IEnumerable<HackNewsItem>> GetHackNewsAsync(HackNewsQueryRequest request, CancellationToken cancellationToken = default);
        Task<List<int>> GetStoryIdsAsync(int numberOfStories, CancellationToken cancellationToken);
        Task<HackNewsItem?> GetStoryAsync(int id, CancellationToken cancellationToken);
        HackNewsItem MapToDomain(ExternalHackerNewsStoryResponse responseSource);

    }
}
