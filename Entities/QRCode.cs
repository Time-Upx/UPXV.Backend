using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using UPXV.Backend.Common.Configuration;
using UPXV.Backend.Common.Extensions;
using UPXV.Backend.Data;
using ZXing.QrCode.Internal;

namespace UPXV.Backend.Entities;

public class QRCode : IHasRequirements
{
   public int Id { get; set; }
   public int IntentId { get; set; }
   public Intent? Intent { get; set; }
   public string? Name { get; set; }
   public string? Description { get; set; }
   public DateTime CreatedAt { get; set; } = DateTime.Now;
   public DateTime? Expiration { get; set; }
   public bool HasExpired => DateTime.Now > Expiration;
   public string? ActivationCode { get; set; }
   public int? UsageLimit { get; set; }
   public int TimesUsed { get; set; }
   public bool HasReachedUsageLimit => TimesUsed > UsageLimit;  
   public List<QRCodeArgument> Arguments { get; set; } = [];

   public QRCode CopyToNew () => new QRCode()
   {
      IntentId = IntentId,
      Intent = Intent,
      Name = Name,
      Description = Description,
      Expiration = Expiration is null ? null : DateTime.Now + (Expiration - CreatedAt),
      ActivationCode = ActivationCode,
      UsageLimit = UsageLimit,
      TimesUsed = 0,
      Arguments = Arguments.CopyToNew()
   };

   public void LoadRequirements (DbContext context)
   {
      context.Load(this, qr => qr.Intent);

      Intent!.Parameters = ((UPXV_Context) context).IntentParameters
         .Where(ip => ip.IntentId == IntentId)
         .ToList();

      Arguments = ((UPXV_Context) context).QRCodeArguments
         .Where(qa => qa.QRCodeId == Id)
         .ToList();
   }
}

public static class QRCodeExtensions
{

   public static IDictionary<string, string> ToDictionary(this IEnumerable<QRCodeArgument> arguments)
   {
      Dictionary<string, string> dict = [];
      foreach (QRCodeArgument arg in arguments)
      {
         dict.Add(arg.Parameter, arg.Value);
      }
      return dict;
   }

   public static List<QRCodeArgument> ToEntities (this IDictionary<string, string> arguments, int qrcodeId, QRCode? qrcode = null)
   {
      return arguments.Select(kv => new QRCodeArgument()
      {
         QRCodeId = qrcodeId,
         QRCode = qrcode,
         Parameter = kv.Key,
         Value = kv.Value
      }).ToList();
   }
   public static List<QRCodeArgument> CopyToNew (this List<QRCodeArgument> arguments)
   {
      return arguments.Select(arg => new QRCodeArgument()
      {
         Parameter = arg.Parameter,
         Value = arg.Value
      }).ToList();
   }

   public static bool TryGetUrl (this QRCode qrcode, ApplicationConfiguration appConfig, out string outUrl, out ValidationResult result)
   {
      if (qrcode.Intent is null)
      {
         outUrl = null!;
         result = new ValidationResult([new ValidationFailure("Intent", "Intenção não carregada")]);
         return false;
      }

      string baseUrl = qrcode.Intent.Type switch
      {
         IntentType.Redirect => appConfig.ClientBaseURL,
         IntentType.Get => appConfig.ServerBaseURL,
         IntentType.Post => appConfig.ServerBaseURL,
         IntentType.Put => appConfig.ServerBaseURL,
         IntentType.Delete => appConfig.ServerBaseURL,
         IntentType.Patch => appConfig.ServerBaseURL,
         _ => throw new NotImplementedException(),
      };
      string url = baseUrl + "/" + qrcode.Intent!.UrlTemplate;
      IDictionary<string, string> arguments = qrcode.Arguments.ToDictionary();

      List<ValidationFailure> paramsNotFound = [];

      foreach (string parameter in qrcode.Intent.Parameters.Select(ip => ip.Parameter))
      {
         if (!arguments.TryGetValue(parameter, out string? value))
         {
            string msg = $"Argumento obrigatório '{parameter}' não existe";
            paramsNotFound.Add(new(parameter, msg));
            continue;
         }
         url = url.Replace("{" + parameter + "}", value);
      }

      if (paramsNotFound.Count > 0)
      {
         result = new ValidationResult(paramsNotFound);
         outUrl = null!;
         return false;
      }

      result = null!;
      outUrl = url;
      return true;
   }

   public static bool TryBuildUrl (Intent intent, ApplicationConfiguration appConfig, IDictionary<string, string> arguments, out string outUrl, out ValidationResult result)
   {
      string baseUrl = intent.Type switch
      {
         IntentType.Redirect => appConfig.ClientBaseURL,
         IntentType.Get => appConfig.ServerBaseURL,
         IntentType.Post => appConfig.ServerBaseURL,
         IntentType.Put => appConfig.ServerBaseURL,
         IntentType.Delete => appConfig.ServerBaseURL,
         IntentType.Patch => appConfig.ServerBaseURL,
         _ => throw new NotImplementedException(),
      };
      string url = baseUrl + "/" + intent.UrlTemplate;

      List<ValidationFailure> paramsNotFound = [];

      foreach (string parameter in intent.Parameters.Select(ip => ip.Parameter))
      {
         if (!arguments.TryGetValue(parameter, out string? value))
         {
            string msg = $"Argumento obrigatório '{parameter}' não existe";
            paramsNotFound.Add(new(parameter, msg));
            continue;
         }
         url = url.Replace("{" + parameter + "}", value);
      }

      if (paramsNotFound.Count > 0)
      {
         result = new ValidationResult(paramsNotFound);
         outUrl = null!;
         return false;
      }

      result = null!;
      outUrl = url;
      return true;
   }
}