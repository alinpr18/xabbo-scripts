/// @name Builder's Club AFK
/// @group Achievements
/// @desc Placing Builder's Club furni.
/// @author alinpr18
/// @scripter 1.0.0-beta.133

int pageId = GetBcCatalog().ToList().Find(a => a.Name == "set_posters").Id;
int offerId = GetBcCatalogPage(pageId).Offers[0].Id;

OnIntercept((
  In.AddItem, In.RemoveItem, In.Notification
), e => {
  e.Block(); 
  if (e.Packet.ReadString() == "furni_placement_error") {
    Talk(":pickallbc");
  }
});

OnIntercept((
  In.BuildersClubFurniCount
), e => {
  Status(e.Packet.ReadInt());
});

try {
  while(Run) {
    Send(Out.BuildersClubPlaceWallItem, pageId, offerId, "", WallLocation.Zero, true);
    Delay(100); 
  }
} finally {
  Talk(":pickallbc");
}