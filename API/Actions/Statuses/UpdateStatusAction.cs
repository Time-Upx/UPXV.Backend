using System.ComponentModel.DataAnnotations;
using UPXV.Backend.Data;
using UPXV.Backend.Common.Exceptions;
using UPXV.Backend.API.Entities;
using UPXV.Backend.API.DTOs.Statuses;

namespace UPXV.Backend.API.Actions.Statuses;

public static class UpdateStatusAction
{
   public static IResult MapEndpoint (StatusUpdateDTO dto, UPXV_Context context)
   {
      return Execute(dto, context).Either(
         Microsoft.AspNetCore.Http.Results.Ok,
failures => failures switch
         {
            ValidationException e => Microsoft.AspNetCore.Http.Results.BadRequest(e),
            EntityNotFoundException<Status> e => Microsoft.AspNetCore.Http.Results.NotFound(e),
            Exception e => Microsoft.AspNetCore.Http.Results.Problem(e.Message, statusCode: 500)
         });
   }

   public static Attempt<StatusDetailDTO, Exception> Execute (StatusUpdateDTO dto, UPXV_Context context)
   {
      if (context.Status.Any(s => s.Id != dto.Id && s.Name == dto.Name))
      {
         return new ValidationException($"Já existe um status com o nome '{dto.Name}'");
      }

      Status? status = context.Status.Find(dto.Id);
      if (status is null)
      {
         return new EntityNotFoundException<Status>(dto.Id);
      }

      context.LoadRequirements(status);

      dto.UpdateEntity(status);
      context.Update(status);
      context.SaveChanges();

      return StatusDetailDTO.Of(status);
   }
}
