#region Copyright Notice
// ----------------------------------------------------------------------------
// <copyright file="Program.cs" company="Dmytro Skryzhevskyi">
// </copyright>
// ----------------------------------------------------------------------------
#endregion

using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
