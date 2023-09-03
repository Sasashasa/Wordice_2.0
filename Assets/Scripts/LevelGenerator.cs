using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private DataSO _russianData;
    [SerializeField] private DataSO _englishData;
    [SerializeField] private GameObject _cubeTemplate;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform[] _evenFinalPositions;
    [SerializeField] private Transform[] _oddFinalPositions;
    [SerializeField] private Material[] _russianLettersMaterials;
    [SerializeField] private Material[] _englishLettersMaterials;

    [Header("Components")] 
    [SerializeField] private Audio _audio;
    [SerializeField] private FirstLevelTutorial _firstLevelTutorial;

    private List<Cube> _cubes;
    private Dictionary<char, Material> _letters;
    private readonly Dictionary<char, Material> _selectedLetters = new Dictionary<char, Material>();
    private DataSO _curData;

    public event Action LevelLoaded;
    public LevelDataSO CurLevel { get; private set; }
    public DataSO RusData => _russianData;
    public DataSO EngData => _englishData;

    private void Start()
    {
        _curData = PlayerPrefs.GetString(SaveDataType.Language) == SaveDataType.Russian ? _russianData : _englishData;
        FillDictionary();
        LoadLevel();
        SelectLetters();
        GenerateCubes();
        MoveCubes();
    }

    private void FillDictionary()
    {
        if (PlayerPrefs.GetString(SaveDataType.Language) == SaveDataType.Russian)
        {
            _letters = new Dictionary<char, Material>
            {
                { 'А', _russianLettersMaterials[0] },
                { 'Б', _russianLettersMaterials[1] },
                { 'В', _russianLettersMaterials[2] },
                { 'Г', _russianLettersMaterials[3] },
                { 'Д', _russianLettersMaterials[4] },
                { 'Е', _russianLettersMaterials[5] },
                { 'Ё', _russianLettersMaterials[6] },
                { 'Ж', _russianLettersMaterials[7] },
                { 'З', _russianLettersMaterials[8] },
                { 'И', _russianLettersMaterials[9] },
                { 'Й', _russianLettersMaterials[10] },
                { 'К', _russianLettersMaterials[11] },
                { 'Л', _russianLettersMaterials[12] },
                { 'М', _russianLettersMaterials[13] },
                { 'Н', _russianLettersMaterials[14] },
                { 'О', _russianLettersMaterials[15] },
                { 'П', _russianLettersMaterials[16] },
                { 'Р', _russianLettersMaterials[17] },
                { 'С', _russianLettersMaterials[18] },
                { 'Т', _russianLettersMaterials[19] },
                { 'У', _russianLettersMaterials[20] },
                { 'Ф', _russianLettersMaterials[21] },
                { 'Х', _russianLettersMaterials[22] },
                { 'Ц', _russianLettersMaterials[23] },
                { 'Ч', _russianLettersMaterials[24] },
                { 'Ш', _russianLettersMaterials[25] },
                { 'Щ', _russianLettersMaterials[26] },
                { 'Ъ', _russianLettersMaterials[27] },
                { 'Ы', _russianLettersMaterials[28] },
                { 'Ь', _russianLettersMaterials[29] },
                { 'Э', _russianLettersMaterials[30] },
                { 'Ю', _russianLettersMaterials[31] },
                { 'Я', _russianLettersMaterials[32] }
            };
        }
        else
        {
            _letters = new Dictionary<char, Material>
            {
                { 'A', _englishLettersMaterials[0] },
                { 'B', _englishLettersMaterials[1] },
                { 'C', _englishLettersMaterials[2] },
                { 'D', _englishLettersMaterials[3] },
                { 'E', _englishLettersMaterials[4] },
                { 'F', _englishLettersMaterials[5] },
                { 'G', _englishLettersMaterials[6] },
                { 'H', _englishLettersMaterials[7] },
                { 'I', _englishLettersMaterials[8] },
                { 'J', _englishLettersMaterials[9] },
                { 'K', _englishLettersMaterials[10] },
                { 'L', _englishLettersMaterials[11] },
                { 'M', _englishLettersMaterials[12] },
                { 'N', _englishLettersMaterials[13] },
                { 'O', _englishLettersMaterials[14] },
                { 'P', _englishLettersMaterials[15] },
                { 'Q', _englishLettersMaterials[16] },
                { 'R', _englishLettersMaterials[17] },
                { 'S', _englishLettersMaterials[18] },
                { 'T', _englishLettersMaterials[19] },
                { 'U', _englishLettersMaterials[20] },
                { 'V', _englishLettersMaterials[21] },
                { 'W', _englishLettersMaterials[22] },
                { 'X', _englishLettersMaterials[23] },
                { 'Y', _englishLettersMaterials[24] },
                { 'Z', _englishLettersMaterials[25] },
            };
        }
    }

    private void LoadLevel() 
    {
        CurLevel = _curData.CurLevel;

        if (CurLevel == null)
            throw new ArgumentNullException(nameof(CurLevel));

        string word = CurLevel.Word;

        if (string.IsNullOrEmpty(word) || string.IsNullOrWhiteSpace(word))
            throw new ArgumentNullException(nameof(word));

        LevelLoaded?.Invoke();
    }

    private void SelectLetters()
    {
        for (int i = 0; i < CurLevel.Word.Length; i++) 
        {
            char letter = CurLevel.Word[i];

            if (!_selectedLetters.ContainsKey(letter)) 
            {
                _selectedLetters.Add(letter, _letters[letter]);
                _letters.Remove(letter);
            }
        }
    }

    private void GenerateCubes()
    {
        _cubes = new List<Cube>();

        for (int i = 0; i < CurLevel.Word.Length; i++)
        {
            Vector3 position = _spawnPoints[i].position;
            CubeRotationState cubeRotationState = CubeState.GenerateRandomRotationState();
            Quaternion rotation = CubeState.Rotation[cubeRotationState];
            
            Cube cube = Instantiate(_cubeTemplate, position, rotation).GetComponent<Cube>();
            cube.SetRotationState(cubeRotationState);
            _cubes.Add(cube);

            for (int j = 0; j < 6; j++)
            {
                var materials = new List<Material>(_letters.Values);
                Renderer side = cube.transform.GetChild(j).GetComponent<Renderer>();
                side.material = materials[Random.Range(0, materials.Count)];
            }
            
            cube.transform.GetChild(0).GetComponent<Renderer>().material = _selectedLetters[CurLevel.Word[i]];
        }
        
        _audio.PlayThrowSound();
    }

    private void MoveCubes()
    {
        switch (_cubes.Count)
        {
            case 3:
                for (int i = 0; i < _cubes.Count; i++)
                {
                    _cubes[i].StartMove(_oddFinalPositions[i + 1]);
                }
                break;
            case 4:
                for (int i = 0; i < _cubes.Count; i++)
                {
                    _cubes[i].StartMove(_evenFinalPositions[i + 1]);
                }
                break;
            case 5:
                for (int i = 0; i < _cubes.Count; i++)
                {
                    _cubes[i].StartMove(_oddFinalPositions[i]);
                }
                break;
            case 6:
                for (int i = 0; i < _cubes.Count; i++)
                {
                    _cubes[i].StartMove(_evenFinalPositions[i]);
                }
                break;
        }

        if (CurLevel.Id == 0)
            StartCoroutine(StartFirstLevelTutorial());
    }
    
    private IEnumerator StartFirstLevelTutorial()
    {
        yield return new WaitForSeconds(1.5f);
        _firstLevelTutorial.StartTutorial();
    }
}