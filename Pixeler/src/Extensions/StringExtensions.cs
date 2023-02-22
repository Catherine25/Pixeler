namespace Pixeler.Extensions;

public static class StringExtensions
{
    public static string Remove(this string str, string toRemove) => str.Replace(toRemove, string.Empty);
}
