using UPXV.Backend.API.Entities;

namespace UPXV.Backend.Data.Seeds;

public static class TagSeeds
{
   public static Tag[] Data => [Laticinio, Cobertura, Refrigerado, Equipamento, Mobilia];

   public static readonly Tag Laticinio = new()
   {
      Id = 1,
      Name = "Laticínio",
      Description = "Contém leite ou produtos à base de leite."
   };
   public static readonly Tag Cobertura = new()
   {
      Id = 2,
      Name = "Cobertura",
      Description = "Usado como cobertura para sorvete."
   };
   public static readonly Tag Refrigerado = new()
   {
      Id = 3,
      Name = "Refrigerado",
      Description = "Requer armazenamento refrigerado."
   };
   public static readonly Tag Equipamento = new()
   {
      Id = 4,
      Name = "Equipamento",
      Description = "Ativos mecânicos ou elétricos da loja."
   };
   public static readonly Tag Mobilia = new()
   {
      Id = 5,
      Name = "Mobília",
      Description = "Mobília da loja, como mesas e cadeiras."
   };
}