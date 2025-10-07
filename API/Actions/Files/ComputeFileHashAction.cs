using System.Buffers.Text;
using UPXV.Backend.Common.Configuration;
using UPXV.Backend.Common.Extensions;

namespace UPXV.Backend.API.Actions.Files;

public static class ComputeFileHashAction
{
   public static Attempt<string, Exception> Execute(IFormFile file, FileConfiguration config)
   {
      char[] chars = file.FileName.Concat(file.FileName).Shuffle().ToArray();
      return new String(chars);
   }
}
