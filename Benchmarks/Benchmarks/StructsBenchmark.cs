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


using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;

namespace Dmytro.Skryzhevskyi.Benchmarks;

[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net80)]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
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