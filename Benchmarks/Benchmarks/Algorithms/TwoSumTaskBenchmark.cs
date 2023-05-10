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

namespace Dmytro.Skryzhevskyi.Benchmarks.Algorithms;
#region Algorithm description
/*
 Given an array of integers nums and an integer target, return indices of the two numbers such that they add up to target.

You may assume that each input would have exactly one solution, and you may not use the same element twice.

You can return the answer in any order.
 */
#endregion

[SimpleJob(RuntimeMoniker.Net70)]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[MemoryDiagnoser()]
public class TwoSumTaskBenchmark
{
    [Params(new[]
    {
        -3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
        4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
        4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
        4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
        4, 4, 4, 3
    })]
    public int[] Nums { get; set; }

    [Params(0)] public int Target { get; set; }

    [Benchmark()]
    public int[] BestRealLifeSpeed()
    {
        return TwoSum_BestRealLifeSpeed(Nums, Target);
    }


    [Benchmark()]
    public int[] BetterTheoreticalComplexity()
    {
        return TwoSum_BetterTheoreticalComplexity(Nums, Target);
    }


    private static int[] TwoSum_BestRealLifeSpeed(int[] nums, int target)
    {
        var lnums = nums;
        var lnums_copy = nums;
        var ltarget = target;
        for (int i = 0; i <= lnums.Length - 2; i++)
        {
            for (int j = i + 1; j <= lnums_copy.Length - 1; j++)
            {
                if (lnums[i] + lnums_copy[j] == ltarget)
                {
                    return new[] { i, j };
                }
            }
        }

        return null;
    }

    private static int[] TwoSum_BetterTheoreticalComplexity(int[] nums, int target)
    {
        var map = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++)
        {
            map.Add(i, nums[i]);
        }

        for (int i = 0; i < nums.Length; i++)
        {
            int complement = target - nums[i];
            var idx = map.FirstOrDefault(x => x.Value == complement).Key;
            if (map.ContainsValue(complement) && idx != i)
            {
                return new[] { i, idx };
            }
        }

        return null;
    }
}