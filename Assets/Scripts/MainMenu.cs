using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private DataSO _russianData;
    [SerializeField] private DataSO _englishData;

    [Header("UI")]
    [SerializeField] private Slider _levelsStatusBar;
    [SerializeField] private Text _levelsAmountText;
    [SerializeField] private Button _rusButton;
    [SerializeField] private Button _engButton;
    [SerializeField] private Sprite _enabledRusButtonSprite;
    [SerializeField] private Sprite _disabledRusButtonSprite;
    [SerializeField] private Sprite _enabledEngButtonSprite;
    [SerializeField] private Sprite _disabledEngButtonSprite;

    [Header("Panels")] 
    [SerializeField] private GameObject[] _panels;
    
    private const int _predictorSceneId = 1;
    
    private void Start()
    {
        if (PlayerPrefs.HasKey(SaveDataType.PassedLevels) == false)
            PlayerPrefs.SetInt(SaveDataType.PassedLevels, 0);
        
        if (PlayerPrefs.HasKey(SaveDataType.Language) == false)
            PlayerPrefs.SetString(SaveDataType.Language, SaveDataType.Russian);

        if (PlayerPrefs.GetInt(SaveDataType.PassedLevels) == 30)
            PlayerPrefs.SetInt(SaveDataType.PassedLevels, 0);
        
        SetLevelsStatusBar();
        CheckWorldTheme();
        SetActiveLanguageButton();
    }

    public void LoadPredictorScene()
    {
        int levelId = PlayerPrefs.GetInt(SaveDataType.PassedLevels);
        
        _russianData.SetLevelId(levelId);
        _englishData.SetLevelId(levelId);
        _russianData.SetDialogDestination(isLoadFromMenu: true);
        _englishData.SetDialogDestination(isLoadFromMenu: true);
        
        SceneManager.LoadScene(_predictorSceneId);
    }

    public void EnableRussian()
    {
        PlayerPrefs.SetString(SaveDataType.Language, SaveDataType.Russian);
        SetActiveLanguageButton();
    }

    public void EnableEnglish()
    {
        PlayerPrefs.SetString(SaveDataType.Language, SaveDataType.English);
        SetActiveLanguageButton();
    }

    private void SetLevelsStatusBar() 
    {
        int passedLevels = PlayerPrefs.GetInt(SaveDataType.PassedLevels) % 10 + 1;
        _levelsStatusBar.value = passedLevels;
        _levelsAmountText.text = $"{passedLevels}/10";
    }

    private void CheckWorldTheme()
    {
        foreach (GameObject panel in _panels)
        {
            panel.SetActive(false);
        }
        
        int id = PlayerPrefs.GetInt(SaveDataType.PassedLevels) / 10;
        _panels[id].SetActive(true);
    }

    private void SetActiveLanguageButton()
    {
        if (PlayerPrefs.GetString(SaveDataType.Language) == SaveDataType.Russian)
        {
            _rusButton.image.sprite = _enabledRusButtonSprite;
            _engButton.image.sprite = _disabledEngButtonSprite;
        }
        else
        {
            _rusButton.image.sprite = _disabledRusButtonSprite;
            _engButton.image.sprite = _enabledEngButtonSprite;
        }
    }

    public void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();

        if (PlayerPrefs.HasKey(SaveDataType.PassedLevels) == false)
            PlayerPrefs.SetInt(SaveDataType.PassedLevels, 0);
        
        if (PlayerPrefs.HasKey(SaveDataType.Language) == false)
            PlayerPrefs.SetString(SaveDataType.Language, SaveDataType.Russian);

        SetLevelsStatusBar();
        CheckWorldTheme();
        SetActiveLanguageButton();
    }
}