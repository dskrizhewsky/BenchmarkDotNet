#region Copyright Notice
/*
 * The MIT License (MIT)
 *
 * Copyright (c) 2023 Dmytro Skryzhevskyi
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
*/
#endregion
using System.Text;

namespace Dmytro.Skryzhevskyi.Benchmarks;

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