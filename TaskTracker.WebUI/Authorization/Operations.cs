using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace TaskTracker.WebUI.Authorization
{
    public static class Operations
    {
        public static OperationAuthorizationRequirement AccessContext = new OperationAuthorizationRequirement { Name = Constants.AccessContextOperationName };

        public static OperationAuthorizationRequirement AccessItem = new OperationAuthorizationRequirement { Name = Constants.AccessItemOperationName };
    }
}
