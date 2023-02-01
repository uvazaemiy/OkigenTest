using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;





public enum TypeOfFood
{
    Watermellon,
    Cheese,
    Cherry
}
public class Item : MonoBehaviour
{
    public TypeOfFood typeOfFood;
    public int index;
    private Rigidbody rb;
    private Vector3 endPoint;
    private GameManager gameManager;
    public bool isMoving = true;

    
    
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Init(GameManager gameManager, int index, Vector3 endPoint)
    {
        this.gameManager = gameManager;
        this.index = index;
        this.endPoint = endPoint;
    }

    public IEnumerator MoveItem(float force)
    {
        if (isMoving)
        {
            yield return new WaitForFixedUpdate();
            float speed = force / 100;
            transform.position = Vector3.MoveTowards(transform.position, endPoint, speed);
            StartCoroutine(MoveItem(force));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EndPoint")
            gameManager.DeleteItem(this);
    }

    private void OnMouseDown()
    {
        if (!gameManager.isGameOver)
            gameManager.StartPicking(this);
    }

    public void EnablePhysics()
    {
        rb.isKinematic = false;
        transform.DOScale(4, 0.5f).SetEase(Ease.InBounce);
    }

    public IEnumerator DisablePhysics(float time)
    {
        yield return time;
        rb.isKinematic = true;
    }
}
