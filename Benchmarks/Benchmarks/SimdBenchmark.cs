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
using System.Numerics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;

namespace Dmytro.Skryzhevskyi.Benchmarks
{
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net70)]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class SimdBenchmark
    {
        private int[] _arr;

        [GlobalSetup]
        public void Setup()
        {
            _arr = new int[100];
            var val = 100;
            for (var i = 0; i < _arr.Length; i++)
            {
                val--;
                _arr[i] = val;
            }
        }

        [Benchmark()]
        public int MinNaiveBenchmark()
        {
            return Min(_arr);
        }

        [Benchmark()]
        public int MinSimdBenchmark()
        {
            return MinSimd(_arr);
        }

        private static int Min(int[] arr)
        {
            var min = int.MaxValue;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                }
            }

            return min;
        }

        private static int MinSimd(int[] arr)
        {
            var minVector = new Vector<int>(int.MaxValue);
            for (int i = 0; i < arr.Length - Vector<int>.Count; i += Vector<int>.Count)
            {
                var sub = new Vector<int>(arr, i);
                minVector = Vector.Min(sub, minVector);
            }

            var tail = new Vector<int>(arr, arr.Length - Vector<int>.Count);
            minVector = Vector.Min(tail, minVector);

            int min = Int32.MaxValue;
            for (int j = 0; j < Vector<int>.Count; j++)
            {
                min = Math.Min(min, minVector[j]);
            }

            return min;
        }
    }
}