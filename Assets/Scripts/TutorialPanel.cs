using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialPanel : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private Text _text;
    
    private readonly string[] _videoNames = { "Swipe.mp4", "LongTap.mp4", "ChangeImage.mp4", "CorrectWord.mp4" };
    private int _videoIndex = -1;

    public void Setup(string text)
    {
        _text.text = text;
        _videoIndex++;
        _videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath,_videoNames[_videoIndex]);
        gameObject.SetActive(true);
        _videoPlayer.Play();
    }
}