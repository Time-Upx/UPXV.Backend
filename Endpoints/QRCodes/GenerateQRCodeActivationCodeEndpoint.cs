
namespace UPXV.Backend.Endpoints.QRCodes;

public class GenerateQRCodeActivationCodeEndpoint : IEndpoint
{
   private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
   private const int _defaultLength = 4;
   private const int _charsLength = 36;
   private static readonly Random Random = Random.Shared;

   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapPost("activation-code", (int? length) => 
      {
         length ??= _defaultLength;

         if (length < 1)
            return Results.BadRequest($"Length must be at least 1.");

         char[] chars = new char[length.Value];
         for (int i = 0 ; i < length.Value ; i++)
         {
            chars[i] = _chars[Random.Next(_charsLength)];
         }

         return Results.Ok(new string(chars));
      })
      .WithDescription("Gera um código alfa-numérico do tamanho desejado (padrão é 4 símbolos)")
      .Produces<string>(StatusCodes.Status200OK)
      .Produces<string>(StatusCodes.Status400BadRequest);
}
