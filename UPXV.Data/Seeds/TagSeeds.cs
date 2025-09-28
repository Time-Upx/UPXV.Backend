using UPXV.Models;

namespace UPXV.Data.Seeds;

public static class TagSeeds
{
   public static Tag[] Data => [Laticinio, Cobertura, Refrigerado, Equipamento, Mobilia];

   public static readonly Tag Laticinio = new()
   {
      Nid = 1,
      Tid = "Laticínio",
      Description = "Contém leite ou produtos à base de leite."
   };
   public static readonly Tag Cobertura = new()
   {
      Nid = 2,
      Tid = "Cobertura",
      Description = "Usado como cobertura para sorvete."
   };
   public static readonly Tag Refrigerado = new()
   {
      Nid = 3,
      Tid = "Refrigerado",
      Description = "Requer armazenamento refrigerado."
   };
   public static readonly Tag Equipamento = new()
   {
      Nid = 4,
      Tid = "Equipamento",
      Description = "Ativos mecânicos ou elétricos da loja."
   };
   public static readonly Tag Mobilia = new()
   {
      Nid = 5,
      Tid = "Mobília",
      Description = "Mobília da loja, como mesas e cadeiras."
   };
}