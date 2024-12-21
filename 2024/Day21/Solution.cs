namespace AdventOfCode.Y2024.Day21;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Numerics;

[ProblemName("Keypad Conundrum")]
class Solution : Solver {

    public object PartOne(string input) {
        return 0;
    }

    public object PartTwo(string input) {
        return 0;
    }
    
    public char[,] ParseTo2DCharArray(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var result = new char[lines.Length, lines[0].Length];

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                result[y, x] = lines[y][x];
            }
        }

        return result;
    }
}
