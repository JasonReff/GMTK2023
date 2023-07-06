using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public static class GameExtensions
{
    public static T Rand<T>(this List<T> list)
    {
        var random = new System.Random();
        T item = (T)list.OrderBy(t => random.Next()).ToList().First();
        return item;
    }

    public static T Rand<T>(this List<T> list, System.Random random)
    {
        T item = (T)list.OrderBy(t => random.Next()).ToList().First();
        return item;
    }

    public static List<T> Pull<T>(this List<T> list, int numberToPull)
    {
        System.Random random = new System.Random();
        if (list.Count < numberToPull)
            numberToPull = list.Count;
        List<T> newList = list.OrderBy(t => random.Next()).Take(numberToPull).ToList();
        return newList;
    }

    public static T GetNext<T>(this List<T> list, T currentItem)
    {
        var index = list.IndexOf(currentItem);
        if (index == list.Count - 1)
            return list[0];
        else
            return list[index + 1];
    }

    public static T GetPrev<T>(this List<T> list, T currentItem)
    {
        var index = list.IndexOf(currentItem);
        if (index == 0)
            return list[list.Count - 1];
        else
            return list[index - 1];
    }

    public static void AddRange<T>(this HashSet<T> hashSet, List<T> range)
    {
        foreach (var item in range)
        {
            hashSet.Add(item);
        }
    }

    public static Vector2Int ToInt(this Vector2 vector)
    {
        return new Vector2Int((int)vector.x, (int)vector.y);
    }
}