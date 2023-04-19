using System.Text;

namespace Benchmarks;

public static class StringBuilderCache
{
    private const int MaxBuilderSize = 384;

    [ThreadStatic] private static StringBuilder CachedInstance;

    public static StringBuilder? Acquire(int capacity = MaxBuilderSize)
    {
        if (capacity <= MaxBuilderSize)
        {
            var sb = CachedInstance;
            if (sb != null)
            {
                if (capacity <= sb.Capacity)
                {
                    CachedInstance = null;
                    sb.Clear();
                    return sb;
                }
            }
        }

        return new StringBuilder(capacity);
    }

    public static string GetStringAndRelease(StringBuilder sb)
    {
        var result = sb.ToString();
        if (sb.Capacity <= MaxBuilderSize)
        {
            CachedInstance = sb;
        }

        return result;
    }
}