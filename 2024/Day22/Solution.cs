// ReSharper disable MemberCanBePrivate.Global

using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2024.Day22;

using System;

[ProblemName("Monkey Market")]
class Solution : Solver {
    public object PartOne(string input) {
        
        var secrets = input.Split(Environment.NewLine).Select(int.Parse).ToArray();
        return secrets.Select(i => SecretNumbers(i, 2000).Last()).Sum();
    }

    public object PartTwo(string input) {
        return 0;
    }

    public IEnumerable<long> SecretNumbers(long sn, int times) {
        for (int i = 0; i < times; i++) {
            sn = NextSecretNumber(sn);
            yield return sn;
        }
    }

    public long NextSecretNumber(long secret) {
        // Step 1: Multiply by 64, mix, prune
        secret ^= (secret * 64) % 16777216;
        secret %= 16777216;

        // Step 2: Divide by 32, round down, mix, prune
        secret ^= (secret / 32) % 16777216;
        secret %= 16777216;

        // Step 3: Multiply by 2048, mix, prune
        secret ^= (secret * 2048) % 16777216;
        secret %= 16777216;

        return secret;
    }
}
