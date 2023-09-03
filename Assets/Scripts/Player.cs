using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private LevelUI _levelUI;
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private CubesTrigger _cubesTrigger;
    [SerializeField] private Swipe _swipe;
    
    private int _maxTurns;
    private int _curTurns;

    public bool IsLongTap { get; private set; }
    public static Player Instance { get; private set; }
    
    
    [DllImport("__Internal")]
    private static extern void ShowAds();

    [DllImport("__Internal")]
    private static extern void RateUs();


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _levelGenerator.LevelLoaded += SetTurns;
        _cubesTrigger.Win += Win;
    }

    private void OnDisable()
    {
        _cubesTrigger.Win -= Win;
    }

    public void TakeTurn()
    { 
        DecreaseTurns(1);

        if (_curTurns == 0)
            Lose();
    }

    public void IncreaseTurns(int turns) 
    {
        if (turns <= 0)
            throw new ArgumentOutOfRangeException(nameof(turns));

        if (_curTurns + turns < _maxTurns) 
        {
            _curTurns += turns;
        }
        else 
        {
            _curTurns = _maxTurns;
        }

        _levelUI.UpdateTurnsText(_curTurns);
    }

    public void SetLongTapState(bool flag)
    {
        IsLongTap = flag;
    }
    
    private void Lose()
    {
        _swipe.StopSwipe();
        _levelUI.ChangeUIForLose();
    }

    private void DecreaseTurns(int turns) 
    {
        if (turns <= 0)
            throw new ArgumentOutOfRangeException(nameof(turns));

        if (_curTurns - turns > 0)
        {
            _curTurns -= turns;
        }
        else
        {
            _curTurns = 0;
        }
        
        _levelUI.UpdateTurnsText(_curTurns);
    }

    private void SetTurns()
    {
        _maxTurns = _levelGenerator.CurLevel.Turns;
        _curTurns = _maxTurns;
        _levelUI.UpdateTurnsText(_curTurns);
    }

    private void Win()
    {
        _swipe.StopSwipe();
        _levelUI.ChangeUIForWin();
        SaveData();
        
        if (PlayerPrefs.GetInt(SaveDataType.PassedLevels) == 1)
            RateUs();
        
        if (PlayerPrefs.GetInt(SaveDataType.PassedLevels) % 2 == 0)
            ShowAds();
    }

    private void SaveData()
    {
        int curLevel = _levelGenerator.CurLevel.Id + 1;
        int passedLevels = PlayerPrefs.GetInt(SaveDataType.PassedLevels);

        if (curLevel > passedLevels)
            PlayerPrefs.SetInt(SaveDataType.PassedLevels, curLevel);
    }
}