﻿namespace DayNine;

public class Program
{
    public static void Main(string[] args)
    {
        var compressed = File.ReadAllText(@"Input/DayNine.txt");
        var decompressed = ExplosivesInCyberspace.Decompress(compressed);
        Console.WriteLine($"Part 1: the decompressed length is {decompressed.Length}");
    }
}