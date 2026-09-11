using HackNewsApp.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HackNewsApp.Application.Contracts.ResultExecution
{
    public interface IResultExecutor
    {
        IActionResult Execute<T>(ControllerBase controller, Result<T> result);

    }
}
