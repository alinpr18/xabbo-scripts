/// @name Quiet mode
/// @desc Turns everyone excluding friends into ghosts and mutes them.
/// @author b7
/// @scripter 1.0.0-beta

OnIntercept((In.Chat, In.Shout), e => {
  var user = GetUser(e.Packet.ReadInt());
  if (user != null && user != Self && !IsFriend(user))
    e.Block();
});

OnEntityAdded(async e => {
  if (e.Entity is IRoomUser user && user.Id != UserData.Id && !IsFriend(user)) {
    await DelayAsync(100);
    Send(In.RoomAvatarEffect, user.Index, 13, 0, 0);
  }
});

OnIntercept(In.RoomAvatarEffect, e => {
  var user = GetUser(e.Packet.ReadInt());
  if (user != null && user != Self && !IsFriend(user))
    e.Block();
});

foreach (var user in Users) {
  if (user != Self && !IsFriend(user)) {
    Send(In.RoomAvatarEffect, user.Index, 13, 0, 0);
  } else {
    Send(In.RoomAvatarEffect, user.Index, user.Effect, 0, 0);
  }
}

try { Wait(); }
catch {
  foreach (var user in Users)
    Send(In.RoomAvatarEffect, user.Index, user.Effect, 0, 0);
}