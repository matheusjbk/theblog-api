using Microsoft.AspNetCore.Mvc;
using TheBlog.API.Filters;

namespace TheBlog.API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter)) { }
}
