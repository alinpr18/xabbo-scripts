/// @name Export inventory
/// @group Inventory
/// @desc Exports your inventory to a searchable web page.
/// @author b7
/// @scripter 1.0.0-beta

using System.Diagnostics;

const bool IncludeUntradeable = false;

string templatePath = Path.Combine("io", "inventory-template.html");
if (!File.Exists(templatePath))
  throw new FileNotFoundException("Template file not found.");

string GetUrlIdentifier(ItemDescriptor desc) {
  string identifier = desc.GetIdentifier().Replace('*', '_');
  if (identifier == "poster") identifier += desc.Variant;
  return identifier;
}

EnsureInventory();
string json = ToJson(Inventory
  .Where(x => x.IsTradeable || IncludeUntradeable)
  .GroupBy(x => x.GetDescriptor())
  .Select(x => new {
    Name = x.Key.GetName(),
    x.Key.GetInfo().Revision,
    Identifier = GetUrlIdentifier(x.Key),
    x.Key.Variant,
    Count = x.Count()
  })
  .OrderByDescending(x => x.Count)
);

File.WriteAllText(
  $"io/inventory.html",
  File.ReadAllText(templatePath)
    .Replace("$jsonInventory", json)
);

Process.Start(new ProcessStartInfo {
  FileName = "io\\inventory.html",
  UseShellExecute = true
});