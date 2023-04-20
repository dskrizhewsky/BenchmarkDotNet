#region Copyright Notice
// ----------------------------------------------------------------------------
// <copyright file="MatrixBenchmark.cs" company="Dmytro Skryzhevskyi">
// </copyright>
// ----------------------------------------------------------------------------
#endregion
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;

namespace Benchmarks;

[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class MatrixBenchmark
{
    private int[,] _mtx;

    [GlobalSetup]
    public void Setup()
    {
        _mtx = new int[100, 100];
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                _mtx[i, j] = i * j;
            }
        }
    }

    [Benchmark()]
    public void DirectOrder()
    {
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                _mtx[i, j] = 1;
            }
        }
    }

    [Benchmark()]
    public void OppositeOrder()
    {
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                _mtx[j, i] = 0;
            }
        }
    }
}