/// @name Compost Monster Plants
/// @group Achievements
/// @author alinpr18
/// @scripter 1.0.0-beta.124

var plants = Pets.Where(e => e.Posture.Equals("rip"));
int n = 0;

foreach (var plant in plants) {
  Send(Out.CompostPlant, plant.Id);
  Status(n++);
  Delay(600);
}