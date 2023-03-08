using Pixeler.Configuration;
using Plugin.Maui.Audio;

namespace Pixeler.Services;

public class AudioService : IAudioService
{
    private readonly ISettings _settings;
    private readonly IAudioManager _audioManager;
    private readonly IAudioPlayer _player;

    public AudioService(ISettings settings,
		IAudioManager audioManager)
    {
        _settings = settings;
        _audioManager = audioManager;
        _player = _audioManager.CreatePlayer(FileSystem.OpenAppPackageFileAsync(_settings.SoundPath).Result);
    }

    public void Play() => _player.Play();
}