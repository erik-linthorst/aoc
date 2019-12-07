using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2015.Day13 {

    class Solution : Solver {

        public string GetName() => "Knights of the Dinner Table";

        public IEnumerable<object> Solve(string input) {
            yield return PartOne(input);
            yield return PartTwo(input);
        }

        int PartOne(string input) => Happiness(input, false).Max();
        int PartTwo(string input) => Happiness(input, true).Max();

        IEnumerable<int> Happiness(string input, bool includeMe) {
            var dh = new Dictionary<(string, string), int>();
            foreach (var line in input.Split('\n')) {
                var m = Regex.Match(line, @"(.*) would (.*) (.*) happiness units by sitting next to (.*).");
                var a = m.Groups[1].Value;
                var b = m.Groups[4].Value;
                var happiness = int.Parse(m.Groups[3].Value) * (m.Groups[2].Value == "gain" ? 1 : -1);
                if (!dh.ContainsKey((a, b))) {
                    dh[(a, b)] = 0;
                    dh[(b, a)] = 0;
                }
                dh[(a, b)] += happiness;
                dh[(b, a)] += happiness;
            }

            var people = dh.Keys.Select(k => k.Item1).Distinct().ToList();
            if (includeMe) {
                people.Add("me");
            }
            return Permutations(people.ToArray()).Select(order =>
                order.Zip(order.Skip(1).Append(order[0]), (a, b) => dh.TryGetValue((a, b), out var v) ? v : 0).Sum()
            );
        }

        IEnumerable<T[]> Permutations<T>(T[] rgt) {
            void Swap(int i, int j) {
                (rgt[i], rgt[j]) = (rgt[j], rgt[i]);
            }

            IEnumerable<T[]> PermutationsRec(int i) {
                yield return rgt.ToArray();

                for (var j = i; j < rgt.Length; j++) {
                    Swap(i, j);
                    foreach (var perm in PermutationsRec(i + 1)) {
                        yield return perm;
                    }
                    Swap(i, j);
                }
            }

            return PermutationsRec(0);
        }
    }
}