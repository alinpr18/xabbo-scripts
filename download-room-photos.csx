/// @name Download room photos
/// @desc Downloads all photos placed in a room.
/// @author b7
/// @scripter 1.0.0-beta

// Whether to download extra information about each photo.
const bool DownloadMetadata = true; 

var photos = WallItems.Named("camera pic").ToArray();
if (!photos.Any()) throw Error("No photos found.");

string dir = $"io/photos/{Room.Id}";
Directory.CreateDirectory(dir);

for (int i = 0; i < photos.Length; i++) {
  Ct.ThrowIfCancellationRequested();
  var photoInfo = FromJson<PhotoInfo>(photos[i].Data);
  string imageFilePath = Path.Combine(dir, $"{photoInfo.Id}.png");
  string metadataFilePath = Path.Combine(dir, $"{photoInfo.Id}.json");
  if (File.Exists(imageFilePath)) continue;
  Log($"Downloading {i+1}/{photos.Length}...");
  try {
    if (DownloadMetadata) {
      var photoData = await H.GetPhotoDataAsync(photoInfo.Id);
      File.WriteAllText(metadataFilePath, ToJson(photoData));
    }
    byte[] imageData = await H.DownloadPhotoAsync(photoInfo.Id);
    File.WriteAllBytes(imageFilePath, imageData);
  } catch (Exception ex) {
    Log($"Failed to download: {ex.Message}");
  }
}

System.Diagnostics.Process.Start("explorer", Path.GetFullPath(dir));