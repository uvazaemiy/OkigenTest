using UnityEngine;
using UnityEngine.UI;

public class CurrentCount : MonoBehaviour
{
    public Text countText;
    [SerializeField] private Animator animator;

    public void StartAnimation(bool plus)
    {
        if (plus)
            animator.SetTrigger("Plus");
        else
            animator.SetTrigger("Minus");
    }
}
