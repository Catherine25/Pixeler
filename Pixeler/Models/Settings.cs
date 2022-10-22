namespace Pixeler.Models;

public class Settings : ISettings
{
    public Size BitmapSize => new(10, 10);
    public string SoundPath => "demo-2.wav";
}