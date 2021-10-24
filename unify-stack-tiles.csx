/// @name Unify stack tiles
/// @group Building
/// @desc Updates all stack tiles in room when setting stack height
/// @author b7
/// @scripter 1.0.0-beta

OnIntercept(Out.StackingHelperSetCaretHeight, async e => {
  e.Block();
  double height = e.Packet.ReadInt(4) / 100.0;
  foreach (var stack in FloorItems.NamedLike("stack magic")) {
    UpdateStackTile(stack, height);
    await DelayAsync(35);
  }
});

Wait();