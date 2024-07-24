namespace RustStash.Core;

using System.Reflection;

public static class AssemblyInfo
{
    /// <summary> Gets the git hash value from the assembly
    /// or null if it cannot be found. </summary>
    /// <returns>The current git version.</returns>
    public static string GetGitHash()
    {
        var asm = typeof(AssemblyInfo).Assembly;
        var attrs = asm.GetCustomAttributes<AssemblyMetadataAttribute>();
        return attrs.FirstOrDefault(a => a.Key == "GitHash")?.Value ?? "No hash set";
    }
}