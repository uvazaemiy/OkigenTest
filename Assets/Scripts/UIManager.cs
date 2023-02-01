using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;





public class UIManager : MonoBehaviour
{
    [SerializeField] private Image Fader;
    [SerializeField] private Text CurrentTask;
    [SerializeField] private CurrentCount currentCount;
    [SerializeField] private GameOverPanel gameOverPanel;
    [SerializeField] private GameObject plusText;
    [SerializeField] private Transform PlusTextCanvas;
    [SerializeField] private Transform spawnPoint;
    [Space] 
    [SerializeField] private float time = 1;

    [Space] [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animation[] animations;

    
    
    

    private void Start()
    {
        Fader.DOFade(0, 1);
    }

    public void ShowCurrentTask(string text, int count)
    {
        CurrentTask.text = "Collect " + text + ":";
        UpdateCurrentCount(count, Color.white);
    }

    public void UpdateCurrentCount(int count, Color color, bool plus = true)
    {
        currentCount.countText.text = count.ToString();
        currentCount.StartAnimation(plus);
    }
    
    public void SpawnNumber(int count, Color color)
    {
        plusText text = Instantiate(plusText, spawnPoint.position, Quaternion.identity).GetComponent<plusText>();
        text.transform.SetParent(PlusTextCanvas);
        
        string message = null;
        if (count > 0)
            message = "+" + count.ToString();
        else
            message = count.ToString();
        text.Init(message.ToString(), color);
    }

    public IEnumerator GameOver(string text, Sprite image)
    {
        gameOverPanel.Setup(text, image);

        foreach (Animation anim in animations)
            anim.Play();
        playerAnimator.SetTrigger("Dancing");
        
        yield return new WaitForSeconds(time * 2);
        Fader.DOFade(1, time);
        yield return new WaitForSeconds(time / 2);
        gameOverPanel.transform.DOMoveY(Screen.height / 2, time);
    }

    public IEnumerator MoveMessageDown()
    {
        yield return 
        gameOverPanel.transform.DOMoveY(- (Screen.height / 2), time).WaitForCompletion();
    }
}