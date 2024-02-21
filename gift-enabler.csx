/// @name Gift enabler
/// @desc
///   Enables the buy as gift options on all items.
///   Only some items not normally giftable are able to be gifted.
/// @author b7
/// @scripter 1.0.0-beta

OnIntercept(In.CatalogPage, e => {
  var page = CatalogPage.Parse(e.Packet);
  foreach (var offer in page.Offers)
    offer.CanPurchaseAsGift = true;
  e.Packet = new Packet(e.Packet.Header, Client).Write(page);
});

Wait();
