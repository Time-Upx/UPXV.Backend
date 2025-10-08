using System.ComponentModel.DataAnnotations;
using System.Net;
using UPXV.Backend.API.DTOs.Statuses;
using UPXV.Backend.API.Entities;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Statuses;

public static class CreateStatusAction
{
   public static IResult MapEndpoint (StatusCreateDTO dto, UPXV_Context context)
   {
      return Execute(dto, context).Either(
         Microsoft.AspNetCore.Http.Results.Ok,
         failures => failures switch
         {
            ValidationException e => Microsoft.AspNetCore.Http.Results.BadRequest(e),
            Exception e => Microsoft.AspNetCore.Http.Results.Problem(e.Message, statusCode: 500)
         });
   }

   public static Attempt<StatusDetailDTO, Exception> Execute (StatusCreateDTO dto, UPXV_Context context)
   {
      if (context.Status.Any(c => c.Name == dto.Name))
      {
         return new ValidationException($"Já existe um status com o nome '{dto.Name}'");
      }

      Status status = dto.BuildEntity();

      context.Status.Add(status);
      context.SaveChanges();

      context.LoadRequirements(status);
      return StatusDetailDTO.Of(status);
   }
}
