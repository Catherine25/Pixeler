using System.Diagnostics;

namespace Pixeler.Services;

public static class Logger
{
    public static void Log<T>(this T classToLog, string message) => Debug.WriteLine("\t" + message, typeof(T).Name);
    public static void Log<T>(this T classToLog, object o) => Log(classToLog, o.ToString());
}
