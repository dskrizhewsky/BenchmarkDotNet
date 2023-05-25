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

[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net80)]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class MatrixBenchmark
{
    private int[,] _mtx;
    private int[][] _jmtx;
    const int _dim = 100;

    [GlobalSetup]
    public void Setup()
    {
        _mtx = new int[_dim, _dim];
        _jmtx = new int[_dim][];
        for (int i = 0; i < _dim; i++)
        {
            _jmtx[i] = new int[_dim];
            for (int j = 0; j < _dim; j++)
            {
                _jmtx[i][j] = i * j;
                _mtx[i, j] = i * j;
            }
        }
    }

    [Benchmark()]
    public void DirectOrder()
    {
        for (int i = 0; i < _dim; i++)
        {
            for (int j = 0; j < _dim; j++)
            {
                _mtx[i, j] = 1;
            }
        }
    }
    
    [Benchmark()]
    public void DirectOrderJagged()
    {
        for (int i = 0; i < _dim; i++)
        {
            for (int j = 0; j < _dim; j++)
            {
                _jmtx[i][j] = 1;
            }
        }
    }

    [Benchmark()]
    public void OppositeOrder()
    {
        for (int i = 0; i < _dim; i++)
        {
            for (int j = 0; j < _dim; j++)
            {
                _mtx[j, i] = 0;
            }
        }
    }
    
    [Benchmark()]
    public void OppositeOrderJagged()
    {
        for (int i = 0; i < _dim; i++)
        {
            for (int j = 0; j < _dim; j++)
            {
                _jmtx[j][i] = 1;
            }
        }
    }
}