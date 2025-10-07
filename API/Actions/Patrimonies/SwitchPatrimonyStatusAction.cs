using UPXV.Backend.API.DTOs.Patrimonies;
using UPXV.Backend.API.Entities;
using UPXV.Backend.Common.Exceptions;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Patrimonies;

public static class SwitchPatrimonyStatusAction
{
   public static IResult MapEndpoint(SwitchPatrimonyStatusDTO dto, UPXV_Context context)
   {
      return Execute(dto, context).Either(
         Results.Ok,
         failure => failure switch
         {
            EntityNotFoundException<Patrimony> e => Results.NotFound(e),
            EntityNotFoundException<Status> e => Results.NotFound(e),
            Exception e => Results.Problem(e.Message, statusCode: 500)
         });
   }
   public static Attempt<PatrimonyDetailDTO, Exception> Execute (SwitchPatrimonyStatusDTO dto, UPXV_Context context)
   {
      Patrimony? patrimony = context.Patrimonies.Find(dto.Id);
      if (patrimony is null) return new EntityNotFoundException<Patrimony>(dto.Id);

      Status? status = context.Status.Find(dto.StatusId);
      if (status is null) return new EntityNotFoundException<Status>(dto.StatusId);

      patrimony.StatusId = dto.StatusId;
      patrimony.Status = status;

      context.Update(patrimony);
      context.SaveChanges();
      context.LoadRequirements(patrimony);

      return PatrimonyDetailDTO.Of(patrimony);
   }
}
