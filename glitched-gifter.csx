/// @name Glitched gifter
/// @desc Sends a glitched gift
/// @author b7
/// @scripter 1.0.0-beta

const string
  Recipient = "",
  Message = "";
const string Gift = GiftFurni.WrapGray;
const GiftBox Box = GiftBox.Cardboard;
const GiftDecor Decor = GiftDecor.Chains;
const int GroupId = -1; // Use an invalid group id

var catalog = GetCatalog();
var page = GetCatalogPage(catalog.FindNode("Group Furni"));
var offer = page.Offers.First(o =>
  o.PriceInCredits == 1 &&
  o.PriceInActivityPoints == 0 &&
  o.Products.OfKind("gld_stool2").Any()
);
PurchaseAsGift(offer, Recipient, Message, $"{GroupId}", Gift, Box, Decor);