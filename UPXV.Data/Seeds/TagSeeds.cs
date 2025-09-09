using UPXV.Models;

namespace UPXV.Data.Seeds;

public static class TagSeeds
{
   public static readonly Tag[] Data = [Tag1!, Tag2!, Tag3!, Tag4!];

   public static readonly Tag Tag1 = new()
   {
      Tid = "Tag1",
      Description = "Tag1 Description"
   };
   public static readonly Tag Tag2 = new()
   {
      Tid = "Tag2",
   };
   public static readonly Tag Tag3 = new()
   {
      Tid = "Tag3",
      Description = "Tag3 Description"
   };
   public static readonly Tag Tag4 = new()
   {
      Tid = "Tag1",
   };
}
