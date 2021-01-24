using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace TaskTracker.WebUI.Authorization
{
    public static class ProtectedOperations
    {
        public static OperationAuthorizationRequirement AccessContext = new OperationAuthorizationRequirement { Name = Constants.AccessContextOperationName };
    }
}
