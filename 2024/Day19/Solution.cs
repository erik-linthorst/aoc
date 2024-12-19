namespace AdventOfCode.Y2024.Day19;

using System;
using System.Collections.Generic;
using System.Linq;

[ProblemName("Linen Layout")]
class Solution : Solver {
    
    public object PartOne(string input) {
        var p = Parse(input);
        var results = new List<bool>();
        var memo = new Dictionary<string, bool>();

        foreach (var r in p.requests) {
            results.Add(CanFormPattern(r, p.designs, memo));
        }

        return results.Count(r => r);
    }

    public object PartTwo(string input) {
        
        var p = Parse(input);

        var results = new List<long>();
        var memo = new Dictionary<string, long>();
        foreach (var r in p.requests)
        {
            results.Add(CountCombinations(r, p.designs, memo));
        }

        return results.Sum(r => r);
    }

    static (List<string> designs, List<string> requests) Parse(string input) {
        string[] lines = input.Split("\n");
        List<string> designs = lines[0].Split(",").Select(x => x.Trim()).ToList();
        List<string> requests = lines.Skip(2).ToList();
        return (designs, requests);
    }

    private static bool CanFormPattern(string wanted, List<string> available, Dictionary<string, bool> memo) {
        
        if (string.IsNullOrEmpty(wanted)) return true;
        if (memo.TryGetValue(wanted, out var possible)) return possible;

        foreach (var availablePattern in available) {
            
            if (!wanted.StartsWith(availablePattern)) {
                continue;
            }

            var remainingPattern = wanted[availablePattern.Length..];
            
            if (!CanFormPattern(remainingPattern, available, memo)) {
                continue;
            }

            memo[wanted] = true;
            return true;
        }

        memo[wanted] = false;
        return false;
    }
    
    private long CountCombinations(string pattern, List<string> availablePatterns, Dictionary<string, long> memo)
    {
        if (string.IsNullOrEmpty(pattern)) return 1;
        if (memo.TryGetValue(pattern, out var countCombinations)) return countCombinations;

        long count = 0;
        foreach (var availablePattern in availablePatterns) {
            
            if (!pattern.StartsWith(availablePattern)) {
                continue;
            }

            var remainingPattern = pattern[availablePattern.Length..];
            count += CountCombinations(remainingPattern, availablePatterns, memo);
        }

        memo[pattern] = count;
        return count;
    }
}
