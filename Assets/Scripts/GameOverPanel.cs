using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Text textOfWinning;
    [SerializeField] private Image winningImage;
    
    
    
    
    public void Setup(string text, Sprite image)
    {
        textOfWinning.text = text;
        winningImage.sprite = image;
    }
}
