using Pixeler.Source.Services;

namespace Pixeler.Source.Extensions;

public static class ButtonExtensions
{
    public static void SetClickSound(this Button button, IAudioService audioService)
    {
        button.Clicked += (_, _) => audioService.Play();
    }
}
