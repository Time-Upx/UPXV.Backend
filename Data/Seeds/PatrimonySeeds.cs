using UPXV.Backend.Entities;

namespace UPXV.Backend.Data.Seeds;

public static class PatrimonySeeds
{
   public static Patrimony[] Data => [CadeiraOdontologica, Autoclave, ComputadorRecepcao, RaioXPortatil];

   public static readonly Patrimony CadeiraOdontologica = new()
   {
      Id = 1,
      Name = "Cadeira Odontológica Principal",
      Description = "Cadeira e unidade de tratamento completa no consultório 1.",
      RegisteredBy = "Dr. João",
      StatusId = StatusSeeds.EmUso.Id,
      Tags = [TagSeeds.Equipamento, TagSeeds.Esterilizacao]
   };

   public static readonly Patrimony Autoclave = new()
   {
      Id = 2,
      Name = "Autoclave Digital",
      Description = "Equipamento para esterilização de instrumentos.",
      RegisteredBy = "Dra. Maria",
      StatusId = StatusSeeds.EmUso.Id,
      Tags = [TagSeeds.Equipamento, TagSeeds.Esterilizacao]
   };

   public static readonly Patrimony ComputadorRecepcao = new()
   {
      Id = 3,
      Name = "Computador da Recepção",
      Description = "Usado para agendamentos e fichas de pacientes.",
      RegisteredBy = "Secretária Ana",
      StatusId = StatusSeeds.EmEstoque.Id, // Sugestão: "Em Uso" seria mais comum, mas mantendo a lógica de estoque
      Tags = [TagSeeds.Equipamento]
   };

   public static readonly Patrimony RaioXPortatil = new()
   {
      Id = 4,
      Name = "Máquina de Raio X Portátil",
      Description = "Para radiografias intra-orais.",
      RegisteredBy = "Dr. João",
      StatusId = StatusSeeds.EmManutencao.Id,
      Tags = [TagSeeds.Equipamento]
   };
}