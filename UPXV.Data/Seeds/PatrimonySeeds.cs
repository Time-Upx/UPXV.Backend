using UPXV.Models;

namespace UPXV.Data.Seeds;

public static class PatrimonySeeds
{
   public static Patrimony[] Data => [FreezerExposicao, CaixaRegistradora, MesaExterna, MaquinaEspresso];

   public static readonly Patrimony FreezerExposicao = new()
   {
      Nid = 1,
      Tid = "Freezer de Exposição Principal",
      Description = "Freezer de exposição para sorvetes com capacidade para 12 sabores.",
      RegisteredBy = "Alice",
      StatusNid = StatusSeeds.EmUso.Nid,
      Tags = [TagSeeds.Equipamento, TagSeeds.Refrigerado]
   };

   public static readonly Patrimony CaixaRegistradora = new()
   {
      Nid = 2,
      Tid = "Caixa Registradora (PDV)",
      Description = "Sistema de ponto de venda e gaveta de dinheiro.",
      RegisteredBy = "Alice",
      StatusNid = StatusSeeds.EmUso.Nid,
      Tags = [TagSeeds.Equipamento]
   };

   public static readonly Patrimony MesaExterna = new()
   {
      Nid = 3,
      Tid = "Mesa para Área Externa",
      RegisteredBy = "Bob",
      StatusNid = StatusSeeds.EmEstoque.Nid,
      Tags = [TagSeeds.Mobilia]
   };

   public static readonly Patrimony MaquinaEspresso = new()
   {
      Nid = 4,
      Tid = "Máquina de Espresso",
      Description = "Para fazer café e affogatos.",
      RegisteredBy = "Alice",
      StatusNid = StatusSeeds.EmManutencao.Nid,
      Tags = [TagSeeds.Equipamento]
   };
}