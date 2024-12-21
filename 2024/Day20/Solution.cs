namespace AdventOfCode.Y2024.Day20;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Numerics;

[ProblemName("Race Condition")]
class Solution : Solver {

    public object PartOne(string input) {
        
        // var p = Parse(input);
	       //
        // var start = p.dic.FirstOrDefault(kvp => kvp.Value == 'S').Key;
        // var end = p.dic.FirstOrDefault(kvp => kvp.Value == 'E').Key;
        // var maxY = p.dic.Keys.MaxBy(v => v.Y);
        // var maxX = p.dic.Keys.MaxBy(v => v.X);
        //
        // var noWalls = p.dic.Where(kvp => kvp.Value != '#').Select(kvp => kvp.Key).ToList();
        // var stepCount = GetStepCount(noWalls, start, end);
	       //
        // return stepCount;

        return 0;
    }

    public object PartTwo(string input) {
        return 0;
    }

    public int GetStepCount(List<Vector2> validPositions, Vector2 start, Vector2 end) {
        var graph = GetGraph(validPositions);
        var paths = graph.FindAllPaths(start, end);
        return paths.Min(i => i.Count - 1);
    }

    public Graph GetGraph(List<Vector2> validPositions) {
        
        var graph = new Graph();
        foreach (var l in validPositions)
        {
            var up = l with { Y = l.Y - 1 };
            var down = l with { Y = l.Y + 1 };
            var left = l with { X = l.X - 1 };
            var right = l with { X = l.X + 1 };
            var filterList = new[] { up, down, left, right };
            var edges = validPositions.Where(i => filterList.Contains(i)).ToList();
            foreach (var edge in edges)
            {
                graph.AddEdge(l, edge);
            }
        }

        return graph;
    }
    
    public (char[,] map, Dictionary<Vector2, char> dic) Parse(string input)
    {
        var lines = input.Split(Environment.NewLine);	
        var map = new char[lines.Length,lines[0].Length];
        var dic = new Dictionary<Vector2, char>();
	
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[0].Length; x++)
            {
                var c =lines[y][x];
                map[y,x]=c;
                dic[new Vector2(x, y)]=c;
            }
        }
        return (map,dic);
    }
    
    public class Graph
    {
        public readonly Dictionary<Vector2, List<Vector2>> _adjacencyList;

        public Graph()
        {
            _adjacencyList = new Dictionary<Vector2, List<Vector2>>();
        }

        public void AddEdge(Vector2 source, Vector2 destination)
        {
            if (!_adjacencyList.ContainsKey(source))
            {
                _adjacencyList[source] = new List<Vector2>();
            }

            _adjacencyList[source].Add(destination);
        }

        public List<List<Vector2>> FindAllPaths(Vector2 start, Vector2 finish)
        {
            var paths = new List<List<Vector2>>();
            var visited = new HashSet<Vector2>();
            var path = new List<Vector2>();
            DFS(start, finish, visited, path, paths);
            return paths;
        }

        private void DFS(Vector2 current, Vector2 finish, HashSet<Vector2> visited, List<Vector2> path,
            List<List<Vector2>> paths)
        {
            visited.Add(current);
            path.Add(current);

            if (current == finish)
            {
                paths.Add(new List<Vector2>(path));
            }
            else if (_adjacencyList.TryGetValue(current, out var neighbors))
            {
                foreach (var neighbor in neighbors)
                {
                    if (!visited.Contains(neighbor))
                    {
                        DFS(neighbor, finish, visited, path, paths);
                    }
                }
            }

            path.RemoveAt(path.Count - 1);
            visited.Remove(current);
        }
    }
}
