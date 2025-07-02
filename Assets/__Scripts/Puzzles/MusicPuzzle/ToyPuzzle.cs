using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using NUnit.Framework;
using UnityEngine;

public class ToyPuzzle : MonoBehaviour
{
    public static ToyPuzzle Instance;

    [SerializeField] private List<int> combination;
    private List<int> enteredCombination;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            enteredCombination = new List<int>();
        }
        else
        {
            Destroy(Instance);
            return;
        }
    }

    public void Entered(int number)
    {
        enteredCombination.Add(number);
        print(number);
        if(enteredCombination.Count == combination.Count)
        {
            CheckCombination();
        }
    }

    public void CheckCombination()
    {
        if (enteredCombination.SequenceEqual(combination))
        {
            print("Combination Complete");
        }
        else
        {
            print("Combination uncomplete");
        }
    }


}
