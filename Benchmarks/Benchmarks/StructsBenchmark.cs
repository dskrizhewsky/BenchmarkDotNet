using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace Benchmarks;

/*
|                Method |      Mean |     Error |    StdDev |    Median |
|---------------------- |----------:|----------:|----------:|----------:|
|            ClassUsage | 0.0046 ns | 0.0047 ns | 0.0044 ns | 0.0050 ns |
|           StructUsage | 0.0024 ns | 0.0033 ns | 0.0029 ns | 0.0004 ns |
| SequentialStructUsage | 0.0027 ns | 0.0039 ns | 0.0037 ns | 0.0004 ns |
|   ExplicitStructUsage | 0.0017 ns | 0.0032 ns | 0.0028 ns | 0.0001 ns |
|       AutoStructUsage | 0.0025 ns | 0.0044 ns | 0.0041 ns | 0.0000 ns |
|  DateFieldStructUsage | 0.0034 ns | 0.0048 ns | 0.0045 ns | 0.0001 ns |
|   ObjFieldStructUsage | 0.0114 ns | 0.0151 ns | 0.0141 ns | 0.0071 ns |

 */
public class StructsBenchmark
{
    private ClassA _classA;
    private StructA _structA;
    private StructB _structB;
    private StructC _structC;
    private StructD _structD;
    private StructE _structE;
    private StructF _structF;

    public void Print()
    {
        Console.WriteLine(_classA.Val3);
        Console.WriteLine(_structA.Val3);
        Console.WriteLine(_structB.Val3);
        Console.WriteLine(_structC.Val3);
        Console.WriteLine(_structD.Val3);
        Console.WriteLine(_structE.Val3);
        Console.WriteLine(_structF.Val3);
    }

    [GlobalSetup]
    public void Setup()
    {
        _classA = new ClassA()
        {
            Val1 = 1,
            Val2 = 2
        };

        _structA = new StructA()
        {
            Val1 = 1,
            Val2 = 2
        };
        _structB = new StructB()
        {
            Val1 = 1,
            Val2 = 2
        };
        _structC = new StructC()
        {
            Val1 = 1,
            Val2 = 2
        };
        _structD = new StructD()
        {
            Val1 = 1,
            Val2 = 2
        };
        _structE = new StructE()
        {
            Date = DateTime.Now,
            Val1 = 1,
            Val2 = 2
        };
        _structF = new StructF()
        {
            Obj = new object(),
            Val1 = 1,
            Val2 = 2
        };
    }

    [Benchmark]
    public void ClassUsage()
    {
        _classA.Val3 = _classA.Val1 + _classA.Val2;
    }

    [Benchmark]
    public void StructUsage()
    {
        _structA.Val3 = _structA.Val1 + _structA.Val2;
    }

    [Benchmark]
    public void SequentialStructUsage()
    {
        _structB.Val3 = _structB.Val1 + _structB.Val2;
    }

    [Benchmark]
    public void ExplicitStructUsage()
    {
        _structC.Val3 = _structC.Val1 + _structC.Val2;
    }

    [Benchmark]
    public void AutoStructUsage()
    {
        _structD.Val3 = _structD.Val1 + _structD.Val2;
    }

    [Benchmark]
    public void DateFieldStructUsage()
    {
        _structE.Val3 = _structE.Val1 + _structE.Val2;
    }

    [Benchmark]
    public void ObjFieldStructUsage()
    {
        _structF.Val3 = _structF.Val1 + _structF.Val2;
    }
}

public class ClassA
{
    public int Val1 { get; set; }
    public int Val2 { get; set; }
    public int Val3 { get; set; }
}

public struct StructA
{
    public int Val1;
    public int Val2;
    public int Val3;
}

[StructLayout(LayoutKind.Sequential)]
public struct StructB
{
    public int Val1;
    public int Val2;
    public int Val3;
}

[StructLayout(LayoutKind.Explicit)]
public struct StructC
{
    [FieldOffset(0)] public int Val1;
    [FieldOffset(8)] public int Val2;
    [FieldOffset(16)] public int Val3;
}

[StructLayout(LayoutKind.Auto)]
public struct StructD
{
    public int Val1;
    public int Val2;
    public int Val3;
}

public struct StructE
{
    public DateTime Date;
    public int Val1;
    public int Val2;
    public int Val3;
}

public struct StructF
{
    public object Obj;
    public int Val1;
    public int Val2;
    public int Val3;
}