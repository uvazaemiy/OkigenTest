using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [Header("Objects")] [Space]
    [SerializeField] private GameObject[] Food;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform belt3;
    [Space] [Header("Game logic")] [Space]
    [SerializeField] private IKManager iKManager;
    [SerializeField] private Transform Target;
    [SerializeField] private Transform ItemSlot;
    [SerializeField] private Transform Stickman;
    [SerializeField] private float delay = 1;
    [SerializeField] private float force = 1;
    [SerializeField] private float timer = 1.4f;
    [SerializeField] private List<Transform> points;
    public bool isGameOver;
    [Space] [Header("UI")] [Space] 
    [SerializeField] private UIManager uIManager;
    [SerializeField] private TaskManager taskManager;
    [Space]
    [SerializeField] private Color PlusColor;
    [SerializeField] private Color MinusColor;
    [Space]
    [SerializeField] private Sprite winIcon;
    [SerializeField] private Sprite loseIcon;


    private int pointsID = 0;
    private List<Item> allItems = new List<Item>();

    private int countOfFood = 0;

    
    
    

    private void Start()
    {
        uIManager.ShowCurrentTask(taskManager.GenerateText(), taskManager.GenerateCount());
        StartCoroutine(Spawnobjects());
    }

    private IEnumerator Spawnobjects()
    {
        if (!isGameOver)
        {
            GameObject itemPrefab = GetRandomItem();
            Item item = Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity, belt3).GetComponent<Item>();
            item.Init(this, allItems.Count, endPoint.position);

            StartCoroutine(item.MoveItem(force));
            
            yield return new WaitForSeconds(delay);
            StartCoroutine(Spawnobjects());
        }

        yield return null;
    }

    private GameObject GetRandomItem()
    {
        int n = Random.Range(0, Food.Length);
        GameObject randomItem = Food[n];
        allItems.Add(randomItem.GetComponent<Item>());
        return randomItem;
    }

    public void DeleteItem(Item item)
    {
        allItems.Remove(item);
        Destroy(item.gameObject);
    }





    public void StartPicking(Item item)
    {
        iKManager.ActiveIK();
        points[2].position = item.transform.position;
        
        StartCoroutine(Picking(item));
        StartCoroutine(Rotation());
    }

    private IEnumerator Picking(Item item = null)
    {
        if (pointsID != points.Count && Vector3.Distance(Target.position, points[pointsID].position) >= 0.1f)
        {
            Target.position = Vector3.Lerp(Target.position, points[pointsID].position, 0.12f);
            yield return null;
            StartCoroutine(Picking(item));
        }
        else if (pointsID != points.Count)
        {
            pointsID++;

            if (pointsID == 2)  //item
            {
                item.isMoving = false;
                item.transform.SetParent(ItemSlot);
                item.transform.position = ItemSlot.position;
            }
            else if (pointsID == 5) //busket
            {
                item.transform.SetParent(points[4]);
                item.transform.position = points[4].position;
                item.EnablePhysics();
            } 
            
            StartCoroutine(Picking(item));
        }
        else
        {
            iKManager.DisableIK();
            pointsID = 0;
            
            StartCoroutine(item.DisablePhysics(timer * 3));
            CheckTheRightFood(item);
        }
    }

    private IEnumerator Rotation()
    {
        Vector3 oldPos = Stickman.position;
        yield return Stickman.DORotate(new Vector3(0, -50, 0), timer / 2, RotateMode.LocalAxisAdd).WaitForCompletion();
        Stickman.DORotate(new Vector3(0, 50, 0), timer / 2, RotateMode.LocalAxisAdd);
    }





    private void CheckTheRightFood(Item item)
    {
        if (item.typeOfFood == taskManager.currentTask)
        {
            countOfFood++;
            uIManager.UpdateCurrentCount(taskManager.count - countOfFood, PlusColor, true);
            uIManager.SpawnNumber(1, PlusColor);

            if (countOfFood == taskManager.count)
            {
                isGameOver = true;
                StartCoroutine(uIManager.GameOver("YOU WIN!", winIcon));
            }
        }
        else
        {
            countOfFood--;
            uIManager.SpawnNumber(-1, MinusColor);

            if (countOfFood < 0)
            {
                countOfFood = 0;
                StartCoroutine(uIManager.GameOver("YOU LOSE!", loseIcon));
            }
            else
                uIManager.UpdateCurrentCount(taskManager.count - countOfFood, MinusColor, false);
        }
    }

    public void ReloadScene()
    {
        StartCoroutine(ReloadSceneRoutine());
    }

    private IEnumerator ReloadSceneRoutine()
    {
        yield return StartCoroutine(uIManager.MoveMessageDown());

        DOTween.KillAll();
        SceneManager.LoadScene("MainGame");
    }
}