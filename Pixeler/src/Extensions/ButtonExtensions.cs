using Pixeler.Services;

namespace Pixeler.Extensions;

public static class ButtonExtensions
{
    public static void SetClickSound(this Button button, IAudioService audioService)
    {
        button.Clicked += (_, _) => audioService.Play();
    }
}
