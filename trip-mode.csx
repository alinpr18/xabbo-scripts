/// @name Trip mode
/// @author b7
/// @scripter 1.0.0-beta

const double Amplitude = 1.5;

int phase = 0;

while (Run) {
  foreach (var item in FloorItems) {
    Send(In.ActiveObjectUpdate, new FloorItem(item) {
      Location = item.Location.Add(0, 0, (float)(Math.Sin((phase+item.X*item.Y)/100.0) * Amplitude))
    });
  }
  phase++;
  Delay(16);
}