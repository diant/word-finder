﻿namespace WordFinder.Core;

public static class LetterPoints
{
    //Tile Values
    //Below are the point values for each letter that is used in a Scrabble game.

    //0 Points - Blank tile.
    //1 Point - A, E, I, L, N, O, R, S, T and U.
    //2 Points - D and G.
    //3 Points - B, C, M and P.
    //4 Points - F, H, V, W and Y.
    //5 Points - K.
    //8 Points - J and X.
    //10 Points - Q and Z.
    internal readonly static Dictionary<char, int> Points_SOWPODS = new()
    {
        { '*', 0 },
        { 'A', 1 },
        { 'B', 3 },
        { 'C', 3 },
        { 'D', 2 },
        { 'E', 1 },
        { 'F', 4 },
        { 'G', 2 },
        { 'H', 4 },
        { 'I', 1 },
        { 'J', 8 },
        { 'K', 5 },
        { 'L', 1 },
        { 'M', 3 },
        { 'N', 1 },
        { 'O', 1 },
        { 'P', 3 },
        { 'Q', 10 },
        { 'R', 1 },
        { 'S', 1 },
        { 'T', 1 },
        { 'U', 1 },
        { 'V', 4 },
        { 'W', 4 },
        { 'X', 8 },
        { 'Y', 4 },
        { 'Z', 10 },
        { 'a', 1 },
        { 'b', 3 },
        { 'c', 3 },
        { 'd', 2 },
        { 'e', 1 },
        { 'f', 4 },
        { 'g', 2 },
        { 'h', 4 },
        { 'i', 1 },
        { 'j', 8 },
        { 'k', 5 },
        { 'l', 1 },
        { 'm', 3 },
        { 'n', 1 },
        { 'o', 1 },
        { 'p', 3 },
        { 'q', 10 },
        { 'r', 1 },
        { 's', 1 },
        { 't', 1 },
        { 'u', 1 },
        { 'v', 4 },
        { 'w', 4 },
        { 'x', 8 },
        { 'y', 4 },
        { 'z', 10 }
    };

    public static ISet<KeyValuePair<char, int>> GetPoints_SOWPODS() =>
        Points_SOWPODS.Where(x => char.IsUpper(x.Key)).ToHashSet();
}
