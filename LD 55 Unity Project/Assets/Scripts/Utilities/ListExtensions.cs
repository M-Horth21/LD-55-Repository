using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ListExtensions
{
  public static T ChooseRandom<T>(this List<T> list)
  {
    //double check that the list has at least 1 item in it
    if (list.Count < 1)
    {
      Debug.Log($"Cannot choose random element from an empty list, returning a default value for type {typeof(T).Name}");
      return default;
    }

    //get a random integer from 1 to the number of items in the list
    int _chosenIndex = UnityEngine.Random.Range(0, list.Count);

    return list[_chosenIndex];
  }

  public static T ChooseRandom<T>(this List<T> list, System.Random randomGenerator = null)
  {
    // Double check that the list has at least 1 item in it.
    if (list.Count < 1)
    {
      Debug.Log($"Cannot choose random element from an empty list, returning a default value for type {typeof(T).Name}");
      return default;
    }

    // Get a random valid index using the provided random generator.
    int _chosenIndex = randomGenerator.Next(list.Count);

    return list[_chosenIndex];
  }

  public static T ChooseRandom<T>(this T[] _array)
  {
    //double check that the list has at least 1 item in it
    if (_array.Length < 1)
    {
      Debug.Log($"Cannot choose random element from an empty array, returning a default value for type {typeof(T).Name}");
      return default;
    }

    //get a random integer from 1 to the number of items in the list
    int _chosenIndex = UnityEngine.Random.Range(0, _array.Length);

    return _array[_chosenIndex];
  }

  public static T ChooseRandomWeighted<T>(this IEnumerable<T> list, System.Func<T, float> weightGetter, System.Random randomGenerator = null)
  {
    // Double check that the list has at least 1 item in it.
    if (list.Count() < 1)
    {
      Debug.Log($"Cannot choose random element from an empty list, returning a default value for type {typeof(T).Name}");
      return default;
    }

    // If the list only has one element in it, that's the answer. Surprise!
    if (list.Count() == 1)
    {
      //Debug.Log("This list only has 1 item in it, you fucking idiot...");
      return list.FirstOrDefault();
    }

    // Add up all the weights.
    float totalWeight = list.Sum(weightGetter);

    // Randomly generate a weight between 0 and the total.
    if (randomGenerator == null) randomGenerator = new();
    float chosenWeight = (float)randomGenerator.NextDouble() * totalWeight;

    // Iterate up the options, adding up their weights, until one option lands on our chosen weight.
    float weightMarker = 0;
    foreach (var choice in list)
    {
      weightMarker += weightGetter(choice);
      if (weightMarker > chosenWeight) return choice;
    }

    return default(T);
  }

  public static T ChooseRandomWithoutRepeating<T>(this List<T> list, T nonRepeat)
  {
    if (nonRepeat == null) return list.ChooseRandom();

    // Make a temporary list with the non repeat item removed, then run the choose random method.
    var tempList = new List<T>(list);
    if (tempList.Contains(nonRepeat))
    {
      tempList.Remove(nonRepeat);
    }

    return tempList.ChooseRandom();
  }
}