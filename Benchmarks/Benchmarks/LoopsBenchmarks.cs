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

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;

namespace Dmytro.Skryzhevskyi.Benchmarks;

[SimpleJob(RuntimeMoniker.Net70)]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class LoopsBenchmarks
{
    private int[] _arr;
    private List<int> _lst;
    private const int count = 1000;

    [GlobalSetup]
    public void Setup()
    {
        _arr = new int[count];
        _lst = new List<int>(count);
        for (var i = 0; i < count; i++)
        {
            _arr[i] = 1;
            _lst.Add(1);
        }
    }

    [Benchmark()]
    public int ForLoopArray()
    {
        var result = 0;
        for (int i = 0; i < count; i++)
        {
            result += _arr[i];
        }

        return result;
    }

    [Benchmark()]
    public int ForLoopList()
    {
        var result = 0;
        for (int i = 0; i < count; i++)
        {
            result += _lst[i];
        }

        return result;
    }

    [Benchmark()]
    public int ForEachLoopArray()
    {
        var result = 0;
        foreach (var val in _arr)
        {
            result += val;
        }

        return result;
    }

    [Benchmark()]
    public int ForEachLoopList()
    {
        var result = 0;
        foreach (var val in _lst)
        {
            result += val;
        }

        return result;
    }
}