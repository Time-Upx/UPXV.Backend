using UPXV.Backend.Entities;

namespace UPXV.Backend.Data.Seeds;

public static class TagSeeds
{
   public static Tag[] Data => [Descartavel, Esterilizacao, Equipamento, Mobiliario, Clinico];

   public static readonly Tag Descartavel = new()
   {
      Id = 1,
      Name = "Descartável",
      Description = "Item de uso único (e.g., luvas, máscaras)."
   };
   public static readonly Tag Esterilizacao = new()
   {
      Id = 2,
      Name = "Esterilização",
      Description = "Item que requer esterilização após o uso ou está em ambiente estéril."
   };
   public static readonly Tag Clinico = new()
   {
      Id = 3,
      Name = "Clínico",
      Description = "Usado diretamente no tratamento do paciente (e.g., resinas, anestésicos)."
   };
   public static readonly Tag Equipamento = new()
   {
      Id = 4,
      Name = "Equipamento",
      Description = "Ativos mecânicos, elétricos ou eletrônicos (e.g., Autoclave, Raio-X)."
   };
   public static readonly Tag Mobiliario = new()
   {
      Id = 5,
      Name = "Mobiliário",
      Description = "Móveis da clínica, como cadeiras de espera e armários."
   };
}