using UnityEngine;
using System;
using System.Collections;

public class CubesTrigger : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] private GameObject _winFX;

    [Header("Components")]
    [SerializeField] private Audio _audio;
    [SerializeField] private LevelGenerator _levelGenerator;
    
    private int _guessedLetters = 0;
    private int _wordLength;

    public event Action Win;
    public static CubesTrigger Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _levelGenerator.LevelLoaded += SetWordLength;
    }

    private void OnDisable()
    {
        _levelGenerator.LevelLoaded -= SetWordLength;
    }

    public void AddCorrectLetter(Transform cube)
    {
        foreach (Renderer side in cube.GetComponentsInChildren<Renderer>())
        {
            side.material.color = new Color(1f, 1f, 1f, 1f);
        }

        _guessedLetters++;

        if (_guessedLetters == _wordLength)
        {
            StartCoroutine(WinLevel());
        }
    }

    private void SetWordLength() 
    {
        _wordLength = _levelGenerator.CurLevel.Word.Length;
    }

    private IEnumerator WinLevel() 
    {
        yield return new WaitForSeconds(0.7f);

        Win?.Invoke();
        _audio.PlayWinSound();
        Destroy(Instantiate(_winFX), 1f);
    }
}