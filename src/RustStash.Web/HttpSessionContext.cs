namespace RustStash.Web;

using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using RustStash.Core;

public class HttpSessionContext : ISessionContext
{
    private static AsyncLocal<bool> assumeSystemCurrent = new AsyncLocal<bool>();

    private readonly IHttpContextAccessor httpContextAccessor;

    public HttpSessionContext(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public int PartyId
    {
        get
        {
            if (assumeSystemCurrent.Value)
            {
                return Constants.SystemEntityId;
            }

            var httpContext = this.httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                var claimsIdentity = (ClaimsIdentity)httpContext.User.Identity!;

                var userIdClaim = claimsIdentity.Claims.SingleOrDefault(c => c.Type == Constants.CustomClaimUserId);
                if (userIdClaim != null)
                {
                    return int.Parse(userIdClaim.Value);
                }
            }

            throw new Exception("Unable to resolve partyId");
        }
    }

    public IDisposable DangerouslyAssumeSystemParty()
    {
        assumeSystemCurrent.Value = true;
        return new CleanUp(assumeSystemCurrent);
    }

    private sealed class CleanUp : IDisposable
    {
        private readonly AsyncLocal<bool> asyncLocal;

        public CleanUp(AsyncLocal<bool> asyncLocal)
        {
            this.asyncLocal = asyncLocal;
        }

        public void Dispose()
        {
            this.asyncLocal.Value = false;
        }
    }
}
