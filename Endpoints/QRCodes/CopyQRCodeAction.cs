namespace UPXV.Backend.Endpoints.QRCodes;

//public static class CopyQRCodeAction
//{
//   public static IResult MapEndpoint (string id, UPXV_Context context)
//   {
//      return Execute(id, context).Either(
//         Results.Ok,
//         failure => failure switch
//         {
//            EntityNotFoundException<QRCode> e => Results.NotFound(e),
//            Exception e => Results.Problem(e.Message, statusCode: 500)
//         });
//   }
//   public static Attempt<QRCodeDetailDTO, Exception> Execute(string id, UPXV_Context context)
//   {
//      QRCode? qrcode = context.QRCodes.Find(id);

//      if (qrcode is null) return new EntityNotFoundException<QRCode>(id: id);

//      QRCode other = qrcode.Clone(Guid.NewGuid().ToString());

//      context.Add(other);
//      context.SaveChanges();
//      context.LoadRequirements(other);

//      return QRCodeDetailDTO.Of(other);
//   }
//}
