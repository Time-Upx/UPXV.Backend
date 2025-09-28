using UPXV.Models;

namespace UPXV.Data.Seeds;

public static class StatusSeeds
{
   public static Status[] Data => [EmUso, EmManutencao, ComDefeito, EmEstoque];

   public static readonly Status EmUso = new()
   {
      Nid = 1,
      Tid = "Em Uso",
      Description = "Item está atualmente em operação."
   };
   public static readonly Status EmManutencao = new()
   {
      Nid = 2,
      Tid = "Em Manutenção",
      Description = "Item está passando por manutenção."
   };
   public static readonly Status ComDefeito = new()
   {
      Nid = 3,
      Tid = "Com Defeito",
      Description = "Item está fora de serviço e precisa de reparo."
   };
   public static readonly Status EmEstoque = new()
   {
      Nid = 4,
      Tid = "Em Estoque",
      Description = "Item está funcional, mas não está em uso no momento."
   };
}