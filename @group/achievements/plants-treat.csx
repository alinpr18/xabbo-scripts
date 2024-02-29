/// @name Treat Monster Plants
/// @group Achievements
/// @author alinpr18
/// @scripter 1.0.0-beta.124

var plants = Pets.Where(e => e.Posture != "rip");
int n = 0;

foreach (var plant in plants) {
  Send(Out.RespectPet, plant.Id);
  Status(n++);
  Delay(600);
}