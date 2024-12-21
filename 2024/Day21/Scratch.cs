using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
// ReSharper disable UseCollectionExpression

namespace AdventOfCode.Y2024.Day21;

public class Scratch {
    public char[,] InputChars;

    public void Run(string input) {
        InputChars = ParseTo2DCharArray(input);
    }

    private static char[,] ParseTo2DCharArray(string input) {
        var lines = input.Split(Environment.NewLine);
        var result = new char[lines.Length, lines[0].Length];

        for (int y = 0; y < lines.Length; y++) {
            for (int x = 0; x < lines[y].Length; x++) {
                result[y, x] = lines[y][x];
            }
        }

        return result;
    }

    public static IEnumerable<Vector2> GetDirections(Vector2 l) {
        yield return l with { Y = l.Y - 1 };
        yield return l with { Y = l.Y + 1 };
        yield return l with { X = l.X - 1 };
        yield return l with { X = l.X + 1 };
    }
}

public class NumPad {
    public Dictionary<char, PadButton> Buttons { get; } = new() {
        { '7', new PadButton(new Vector2(0, 0), '7') },
        { '8', new PadButton(new Vector2(0, 1), '8') },
        { '9', new PadButton(new Vector2(0, 2), '9') },
        { '4', new PadButton(new Vector2(1, 0), '4') },
        { '5', new PadButton(new Vector2(1, 1), '5') },
        { '6', new PadButton(new Vector2(1, 2), '6') },
        { '1', new PadButton(new Vector2(2, 0), '1') },
        { '2', new PadButton(new Vector2(2, 1), '2') },
        { '3', new PadButton(new Vector2(2, 2), '3') },
        { '0', new PadButton(new Vector2(3, 1), '0') },
        { 'A', new PadButton(new Vector2(3, 2), 'A') }
    };

    public char CurrentButton { get; set; } = 'A';
    public Vector2 CurrentLocation { get; set; } = new(3, 2);
    public List<(char, List<char>)> History { get; } = new();
    
    public void Press(char c) {
        
        // calc the shortest path in vectors using a BFS  from CurrentLocation
        // then convert this path into a list of chars using ^ for up, v for down, < for left, > for right
        // and make this path ()
        // then add c with the path chars to History and set CurrentButton to c
        
        var targetButton = Buttons[c];
        var targetLocation = targetButton.Loc;
        var path = FindShortestPath(CurrentLocation, targetLocation);
        var directions = ConvertPathToDirections(path);

        History.Add((c, directions));
        CurrentButton = c;
        CurrentLocation = targetLocation;
    }

    private List<Vector2> FindShortestPath(Vector2 start, Vector2 end) {
        var queue = new Queue<(Vector2, List<Vector2>)>();
        var visited = new HashSet<Vector2>();
        queue.Enqueue((start, new List<Vector2> { start }));
        visited.Add(start);

        while (queue.Count > 0) {
            var (current, path) = queue.Dequeue();

            if (current == end) {
                return path;
            }

            foreach (var direction in Scratch.GetDirections(current)) {
                if (!visited.Contains(direction) && Buttons.Values.Any(b => b.Loc == direction)) {
                    visited.Add(direction);
                    var newPath = new List<Vector2>(path) { direction };
                    queue.Enqueue((direction, newPath));
                }
            }
        }

        return null; // No path found
    }

    private List<char> ConvertPathToDirections(List<Vector2> path) {
        var directions = new List<char>();

        for (int i = 1; i < path.Count; i++) {
            var diff = path[i] - path[i - 1];
            if (diff == new Vector2(0, -1)) directions.Add('^');
            else if (diff == new Vector2(0, 1)) directions.Add('v');
            else if (diff == new Vector2(-1, 0)) directions.Add('<');
            else if (diff == new Vector2(1, 0)) directions.Add('>');
        }

        return directions;
    }
}

public class KeyPad {
}

public record PadButton(Vector2 Loc, char C) {
}
