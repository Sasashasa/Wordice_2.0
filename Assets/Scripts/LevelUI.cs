using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image _russianPauseHeader;
    [SerializeField] private Image _englishPauseHeader;
    [SerializeField] private Button _russianContinueButton;
    [SerializeField] private Button _englishContinueButton;
    [SerializeField] private Button _russianPausePlayAgainButton;
    [SerializeField] private Button _englishPausePlayAgainButton;
    [SerializeField] private Button _russianPauseMainMenuButton;
    [SerializeField] private Button _englishPauseMainMenuButton;
    
    [SerializeField] private Image _russianWinHeader1;
    [SerializeField] private Image _englishWinHeader1;
    [SerializeField] private Image _russianWinHeader2;
    [SerializeField] private Image _englishWinHeader2;
    [SerializeField] private Button _russianNextLevelButton;
    [SerializeField] private Button _englishNextLevelButton;
    
    [SerializeField] private Image _russianLoseHeader;
    [SerializeField] private Image _englishLoseHeader;
    [SerializeField] private Button _russianLosePlayAgainButton;
    [SerializeField] private Button _englishLosePlayAgainButton;
    [SerializeField] private Button _russianLoseMainMenuButton;
    [SerializeField] private Button _englishLoseMainMenuButton;
    
    [SerializeField] private Button _russianNextButton;
    [SerializeField] private Button _englishNextButton;
    
    [Header("UI")]
    [SerializeField] private Text _wordText;
    [SerializeField] private Text _turnsCountText;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _nextPictureButton;
    [SerializeField] private Image _pausePanel;
    [SerializeField] private Image _winPanel;
    [SerializeField] private Image _losePanel;

    [Header("Components")]
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private Swipe _swipe;

    private const int _predictorSceneId = 1;

    private void Awake()
    {
        if (PlayerPrefs.GetString(SaveDataType.Language) == SaveDataType.Russian)
        {
            _russianContinueButton.gameObject.SetActive(true);
            _russianLoseHeader.gameObject.SetActive(true);
            _russianNextButton.gameObject.SetActive(true);
            _russianPauseHeader.gameObject.SetActive(true);
            _russianWinHeader1.gameObject.SetActive(true);
            _russianWinHeader2.gameObject.SetActive(true);
            _russianNextLevelButton.gameObject.SetActive(true);
            _russianLoseMainMenuButton.gameObject.SetActive(true);
            _russianLosePlayAgainButton.gameObject.SetActive(true);
            _russianPauseMainMenuButton.gameObject.SetActive(true);
            _russianPausePlayAgainButton.gameObject.SetActive(true);
        }
        else
        {
            _englishContinueButton.gameObject.SetActive(true);
            _englishLoseHeader.gameObject.SetActive(true);
            _englishNextButton.gameObject.SetActive(true);
            _englishPauseHeader.gameObject.SetActive(true);
            _englishWinHeader1.gameObject.SetActive(true);
            _englishWinHeader2.gameObject.SetActive(true);
            _englishNextLevelButton.gameObject.SetActive(true);
            _englishLoseMainMenuButton.gameObject.SetActive(true);
            _englishLosePlayAgainButton.gameObject.SetActive(true);
            _englishPauseMainMenuButton.gameObject.SetActive(true);
            _englishPausePlayAgainButton.gameObject.SetActive(true);
        }
    }
    
    private void Start()
    {
        _levelGenerator.LevelLoaded += SetWordText;
    }

    private void OnDisable()
    {
        _levelGenerator.LevelLoaded -= SetWordText;
    }

    public void UpdateTurnsText(int turns)
    {
        _turnsCountText.text = turns.ToString();
    }

    public void ChangeUIForWin() 
    {
        _winPanel.gameObject.SetActive(true);
        _pauseButton.interactable = false;
        _nextPictureButton.interactable = false;
    }

    public void ChangeUIForLose() 
    {
        _losePanel.gameObject.SetActive(true);
        _pauseButton.interactable = false;
        _nextPictureButton.interactable = false;
    }

    public void Pause()
    {
        if (_pausePanel.gameObject.activeInHierarchy == false)
        {
            _pausePanel.gameObject.SetActive(true);
            _nextPictureButton.interactable = false;
            _swipe.StopSwipe();
        }
        else
        {
            _pausePanel.gameObject.SetActive(false);
            _nextPictureButton.interactable = true;
            _swipe.AllowSwipe();
        }
    }

    public void LoadPredictorScene()
    {
        _levelGenerator.RusData.SetDialogDestination(isLoadFromMenu: false);
        _levelGenerator.EngData.SetDialogDestination(isLoadFromMenu: false);
        
        SceneManager.LoadScene(_predictorSceneId);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SetWordText()
    {
        _wordText.text = _levelGenerator.CurLevel.Word;
    }
}