using UPXV.Backend.API.Entities;

namespace UPXV.Backend.Data.Seeds;

public static class PatrimonySeeds
{
   public static Patrimony[] Data => [FreezerExposicao, CaixaRegistradora, MesaExterna, MaquinaEspresso];

   public static readonly Patrimony FreezerExposicao = new()
   {
      Id = 1,
      Name = "Freezer de Exposição Principal",
      Description = "Freezer de exposição para sorvetes com capacidade para 12 sabores.",
      RegisteredBy = "Alice",
      StatusId = StatusSeeds.EmUso.Id,
      Tags = [TagSeeds.Equipamento, TagSeeds.Refrigerado]
   };

   public static readonly Patrimony CaixaRegistradora = new()
   {
      Id = 2,
      Name = "Caixa Registradora (PDV)",
      Description = "Sistema de ponto de venda e gaveta de dinheiro.",
      RegisteredBy = "Alice",
      StatusId = StatusSeeds.EmUso.Id,
      Tags = [TagSeeds.Equipamento]
   };

   public static readonly Patrimony MesaExterna = new()
   {
      Id = 3,
      Name = "Mesa para Área Externa",
      RegisteredBy = "Bob",
      StatusId = StatusSeeds.EmEstoque.Id,
      Tags = [TagSeeds.Mobilia]
   };

   public static readonly Patrimony MaquinaEspresso = new()
   {
      Id = 4,
      Name = "Máquina de Espresso",
      Description = "Para fazer café e affogatos.",
      RegisteredBy = "Alice",
      StatusId = StatusSeeds.EmManutencao.Id,
      Tags = [TagSeeds.Equipamento]
   };
}