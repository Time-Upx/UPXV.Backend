using UPXV.Models;

namespace UPXV.Data.Seeds;

public static class StatusSeeds
{
   public static readonly Status[] Data = [None!, Status1!, Status2!, Status3!, Status4!];

   public static readonly Status None = new() 
   {
      Tid = "Nenhum",
      Description = "Nenhum status foi dado para este item até o momento"
   };

   public static readonly Status Status1 = new()
   {
      Tid = "Status1",
      Description = "Status1 Description"
   };
   public static readonly Status Status2 = new()
   {
      Tid = "Status2",
   };
   public static readonly Status Status3 = new()
   {
      Tid = "Status3",
      Description = "Status3 Description"
   };
   public static readonly Status Status4 = new()
   {
      Tid = "Status1",
   };
}
