using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class plusText : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private float time = 1;

    private void Start()
    {
        transform.Rotate(0, 77.27f, 0);
        transform.DOMoveY(0.1f, time).SetRelative(true);
        text.DOFade(0, time);
        Destroy(gameObject, time + 0.1f);
    }

    public void Init(string text, Color color)
    {
        this.text.text = text;
        this.text.color = color;
    }
}