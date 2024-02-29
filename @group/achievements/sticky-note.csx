/// @name Sticky Note AFK
/// @group Achievements
/// @desc Post and Remove Sticky Note
/// @author alinpr18
/// @scripter 1.0.0-beta.133

// write the name of the furni in your language
string furniname = "";
EnsureInventory();
var stickies = Inventory.GetWallItems().Named(furniname);

OnIntercept((
  In.AddItem, In.RemoveItem
), e => e.Block());

while (Run) {
  var roomId = Furni.GetWallItems().Where(a => a.GetName() == furniname).ToList();
  
  foreach (var item in stickies) {
    Send(Out.PlacePostIt, item.ItemId, WallLocation.Zero);
    Delay(100);
  }
  
  foreach (var item in roomId) {
    Send(Out.RemoveItem, item.Id);
    Delay(100);
  }
}
