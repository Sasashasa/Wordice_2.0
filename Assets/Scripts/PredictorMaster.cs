using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PredictorMaster : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button _russianStartButton;
    [SerializeField] private Button _englishStartButton;
    [SerializeField] private Button _russianContinueButton;
    [SerializeField] private Button _englishContinueButton;

    [Header("GameObjects")]
    [SerializeField] private DataSO _russianData;
    [SerializeField] private DataSO _englishData;
    [SerializeField] private GameObject _characterPanel;
    [SerializeField] private GameObject _predictorPanel;
    [SerializeField] private Text _characterText;
    [SerializeField] private Text _predictorText;
    [SerializeField] private Image _characterImage;
    
    private DataSO _data;
    private LevelDataSO _curLevel;
    private const int _mainMenuSceneId = 0;
    private const int _levelSceneId = 2;

    private void Awake()
    {
        if (PlayerPrefs.GetString(SaveDataType.Language) == SaveDataType.Russian)
        {
            _data = _russianData;
            _russianStartButton.gameObject.SetActive(true);
            _russianContinueButton.gameObject.SetActive(true);
        }
        else
        {
            _data = _englishData;
            _englishStartButton.gameObject.SetActive(true);
            _englishContinueButton.gameObject.SetActive(true);
        }
        
        _curLevel = _data.CurLevel;
        SetText();
        SetCharacter();
        ChoosePanel();
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(_levelSceneId);
    }

    public void LoadLevelsMenu()
    {
        SceneManager.LoadScene(_mainMenuSceneId);
    }

    private void ChoosePanel()
    {
        if (_data.IsLoadFromMenu)
        {
            OpenCharacterPanel();
        }
        else
        {
            OpenPredictorPanel();
        }
    }
    
    private void OpenCharacterPanel()
    {
        _characterPanel.SetActive(true);
        _predictorPanel.SetActive(false);
    }

    private void OpenPredictorPanel()
    {
        _predictorPanel.SetActive(true);
        _characterPanel.SetActive(false);
    }

    private void SetText()
    {
        Dialog curDialog = _curLevel.Dialog;
        _characterText.text = curDialog.CharacterText;
        _predictorText.text = curDialog.PredictorText;
    }

    private void SetCharacter()
    {
        Sprite curCharacter = _data.GetCurCharacter();
        _characterImage.GetComponent<RectTransform>().sizeDelta = new Vector2(curCharacter.rect.width * 2, curCharacter.rect.height * 2);
        _characterImage.sprite = curCharacter;
    }
}