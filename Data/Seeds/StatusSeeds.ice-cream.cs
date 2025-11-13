using UPXV.Backend.Entities;

namespace UPXV.Backend.Data.Seeds;

public static class StatusSeedsIceCream
{
   public static Status[] Data => [EmUso, EmManutencao, ComDefeito, EmEstoque];

   public static readonly Status EmUso = new()
   {
      Id = 1,
      Name = "Em Uso",
      Description = "Item está atualmente em operação."
   };
   public static readonly Status EmManutencao = new()
   {
      Id = 2,
      Name = "Em Manutenção",
      Description = "Item está passando por manutenção."
   };
   public static readonly Status ComDefeito = new()
   {
      Id = 3,
      Name = "Com Defeito",
      Description = "Item está fora de serviço e precisa de reparo."
   };
   public static readonly Status EmEstoque = new()
   {
      Id = 4,
      Name = "Em Estoque",
      Description = "Item está funcional, mas não está em uso no momento."
   };
}