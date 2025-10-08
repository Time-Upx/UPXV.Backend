using IRouter = UPXV.Backend.API.Routes.IRouter;

namespace UPXV.Backend.Common.Extensions;

public static class RouteExtensions
{
   public static void Configure (this IEndpointRouteBuilder endpoints, IRouter router) => router.ConfigureEndpoints(endpoints);
}
