using System;
using BloodPressure.Application.Common.Constants;
using BloodPressure.Application.Common.Interfaces;
using BloodPressure.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;

namespace BloodPressure.Functions
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ApplicationOptions _applicationOptions;

        public CurrentUserService(IHttpContextAccessor contextAccessor, IOptions<ApplicationOptions> applicationOptions)
        {
            _contextAccessor = contextAccessor;
            _applicationOptions = applicationOptions.Value;
        }

        public string UserId => _contextAccessor.HttpContext?.User.GetObjectId() ?? GetLocalUser();

        private string GetLocalUser()
        {
            if (_applicationOptions.IsLocalEnvironment)
            {
                return LocalConstants.LocalUserId;
            }

            throw new ArgumentNullException(nameof(UserId), "Not found");
        }
    }
}
