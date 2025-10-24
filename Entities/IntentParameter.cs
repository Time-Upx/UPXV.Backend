
namespace UPXV.Backend.Entities;

public class IntentParameter
{
   public int Id { get; set; }
   public int IntentId { get; set; }
   public string Parameter { get; set; }
   public Intent? Intent { get; set; }

   public IntentParameter (int intentId, string parameter)
   {
      IntentId = intentId;
      Parameter = parameter;
   }

   public void Deconstruct (out int id, out int intentId, out string requiredParameter, out Intent? intent)
   {
      id = Id;
      intentId = IntentId;
      requiredParameter = Parameter;
      intent = Intent;
   }
}
