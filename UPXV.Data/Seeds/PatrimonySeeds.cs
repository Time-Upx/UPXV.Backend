using UPXV.Models;

namespace UPXV.Data.Seeds;

public static class PatrimonySeeds
{
   public static readonly Patrimony[] Data = [Patrimony1!, Patrimony2!, Patrimony3!, Patrimony4!];

   public static readonly Patrimony Patrimony1 = new()
   {
      Tid = "Patrimônio 1",
      RegisteredBy = "José",
      Status = StatusSeeds.None,
   };
   public static readonly Patrimony Patrimony2 = new ()
   {
      Tid = "Patrimônio 2",
      Status = StatusSeeds.None,
   };
   public static readonly Patrimony Patrimony3 = new ()
   {
      Tid = "Patrimônio 3",
      RegisteredBy = "Maria",
      Status = StatusSeeds.None,
   };
   public static readonly Patrimony Patrimony4 = new ()
   {
      Tid = "Patrimônio 4",
      Status = StatusSeeds.None,
   };
}
