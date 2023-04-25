using BenchmarkDotNet.Attributes;

namespace Benchmarks;

public class LinqBenchmarks
{
    private int[] _arr;
    [Params(100)] public int ArrSize { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _arr = new int[ArrSize];
        for (int i = 0; i < ArrSize - 1; i++)
        {
            _arr[i] = 1;
        }
    }

    [Benchmark]
    public int SumLinq()
    {
        return _arr.Sum();
    }

    [Benchmark()]
    public int SumPure()
    {
        int sum = 0;
        for (int i = 0; i < ArrSize - 1; i++)
        {
            sum += _arr[i];
        }

        return sum;
    }
}