using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackNewsApp.Application.Validators.Common
{
    public static class ValidationMessages
    {
        public const string PageNumberMustBeGreaterThanZero = "Page Number must be greater than 0 !";
        public const string PageSizeMustBeWithinRange = "Page Size must be between 1 and 100 !";
        public const string MinimumScoreMustBeValid = "Minimum Score must be greater than or equal to 0 !";
        public const string MaximumScoreMustBeValid = "Maximum Score must be greater than or equal to 0 !";

        public const string MinimumScoreMustBeLessThanOrEqualToMaximumScore =
            "Minimum Score must be less than or equal to Maximum Score!";

        public const string InvalidSortByField = "Invalid sort field! The Following fields are allowed: score, score_desc, " +
            "publishedtime, publishedtime_desc, title, title_desc";

        public const string UsernameRequired = "Username is required!";
        public const string PasswordRequired = "Password is required!";
        public const string UsernameMinimumLength = "Username must be at least 8 characters!";
        public const string PasswordMinimumLength = "Password must be at least 8 characters!";
        public const string UsernameMaximumLength = "Username cannot exceed 100 characters!";
        public const string PasswordMaximumLength = "Password cannot exceed 128 characters!";


    }
}
