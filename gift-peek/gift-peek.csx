/// @name Gift peek
/// @desc Exports information of gifts in the room to a web page
/// @author b7
/// @script 1.0.0-beta

using System.IO;
using System.Diagnostics;

var gifts = FloorItems
  .OfCategory(FurniCategory.Gift)
  .Where(x => x.Data is IMapData)
  .OrderBy(it => it.Y)
  .ThenBy(it => it.X)
  .ThenBy(it => it.Z);

if (!gifts.Any())
  throw Error("No gifts found.");

DirectoryInfo dir = Directory.CreateDirectory($"io/gifts");

class GiftInfo {
  public string Product { get; set; }
  public string ProductName { get; set; }
  public string ProductDescription { get; set; }
  public string Name { get; set; }
  public int Revision { get; set; }
  public string Identifier { get; set; }
  public string Message { get; set; }
  public string Sender { get; set; }
  public string Figure { get; set; }
  public string Extra { get; set; }
}

GiftInfo GetGiftInfo(IFloorItem item) {
  if (!(item.Data is IMapData map))
    throw new Exception("No map found in item data");

  GiftInfo info = new GiftInfo {
    Product = map["PRODUCT_CODE"],
    Message = H.RenderText(map["MESSAGE"])
  };
 
  if (ProductData.TryGetValue(info.Product, out var product)) {
    info.ProductName = product.Name;
    info.ProductDescription = H.RenderText(product.Description);
  }
 
  Xabbo.Core.GameData.FurniInfo furniInfo = FurniData[info.Product];
  if (furniInfo == null && product != null) {
    var matches = FurniData.Where(x => x.Name == product.Name);
    if (matches.Count() == 1) {
      furniInfo = matches.First();
    }
  }
 
  if (furniInfo != null) {
    info.Identifier = furniInfo.Identifier;
    info.Revision = furniInfo.Revision;
    info.Name = furniInfo.Name;
  }
  
  if (map.TryGetValue("PURCHASER_NAME", out string sender))
    info.Sender = H.RenderText(sender);
  
  if (map.TryGetValue("PURCHASER_FIGURE", out string figure))
    info.Figure = figure;
  
  if (map.TryGetValue("EXTRA_PARAM", out string extra))
    info.Extra = H.RenderText(extra);
  
  return info;
}

string json = ToJson(gifts.Select(GetGiftInfo), false);
string html = File.ReadAllText("io/gifts/template.html")
  .Replace("$json", json);

string filePath = Path.Combine(dir.FullName, $"{RoomId}.html");
File.WriteAllText(filePath, html);
Process.Start(new ProcessStartInfo {
  FileName = filePath,
  UseShellExecute = true
});