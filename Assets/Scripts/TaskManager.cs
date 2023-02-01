using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Food;
    [Space] 
    public TypeOfFood currentTask;
    public int count;


    
    
    
    public string GenerateText()
    {
        int n = Random.Range(0, Food.Length);

        currentTask = Food[n].GetComponent<Item>().typeOfFood;
        
        return Food[n].name;
    }

    public int GenerateCount()
    {
        count = Random.Range(1, 6);;
        
        return count;
    }
}