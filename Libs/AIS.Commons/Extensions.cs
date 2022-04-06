using Hero;
using Hero.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Ride;
using System.Net;

namespace AIS
{
    public static class Extensions
    {
        public static IActionResult ToContentJson<T>(this ICommandResult<T> result)
        {
            if (result.IsNull())
                return new NoContentResult();

            var json = result.ToJson();
            result.Dispose();
            return new ContentResult
            {
                Content = json,
                ContentType = "application/json",
                StatusCode = HttpStatusCode.OK.GetHashCode()
            };
        }
    }
}