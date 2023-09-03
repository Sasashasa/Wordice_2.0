using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public struct TutorialStep
{
    [TextArea(4, 4)]
    [SerializeField] private string _russianText;
    
    [TextArea(4, 4)]
    [SerializeField] private string _englishText;
    
    public string RussianText => _russianText;
    public string EnglishText => _englishText;
}