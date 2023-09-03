using UnityEngine;
using UnityEngine.Video;

public class FirstLevelTutorial : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private TutorialPanel _tutorialPanel;
    
    [Header("TutorialSteps")]
    [SerializeField] private TutorialStep[] _steps;
    
    private int _passedSteps;
    private bool _isGuessedFirstLetter;
    
    public void StartTutorial()
    {
        _passedSteps = 0;

        TutorialStep step = _steps[_passedSteps];
        
        string text = PlayerPrefs.GetString(SaveDataType.Language) == SaveDataType.Russian
            ? step.RussianText
            : step.EnglishText;

        _tutorialPanel.Setup(text);
        _passedSteps++;
    }
    
    public void NextStep()
    {
        if (_passedSteps == _steps.Length)
        {
            _tutorialPanel.gameObject.SetActive(false);
            return;
        }

        int curStep = _passedSteps;
        TutorialStep step = _steps[curStep];
        
        string text = PlayerPrefs.GetString(SaveDataType.Language) == SaveDataType.Russian
            ? step.RussianText
            : step.EnglishText;

        _tutorialPanel.Setup(text);
        _passedSteps++;
    }
}