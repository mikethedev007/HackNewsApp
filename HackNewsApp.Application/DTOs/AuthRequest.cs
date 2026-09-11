using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackNewsApp.Application.DTOs
{
    public sealed record AuthRequest
    (
        string Username,
        string Password
    );
}
