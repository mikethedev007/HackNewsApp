using HackNewsApp.Application.Common;
using HackNewsApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackNewsApp.Application.Contracts.Services
{
    public interface IHackNewsService
    {
        Task<Result<PagedResult<HackNewsItemDto>>> GetNewsAsync(HackNewsQueryRequest request, CancellationToken cancellationToken);
    }
}
