using Godot;
using System;
using System.Collections.Generic;

public static class HelperFunctions
{

    /// <summary>
    /// for each loop with indexer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ie"></param>
    /// <param name="action"></param>
    public static void Each<T>(this IEnumerable<T> ie, Action<T, int> action)
    {
        var i = 0;
        foreach (var e in ie) action(e, i++);
    }
}
