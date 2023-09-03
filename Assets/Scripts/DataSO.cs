using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DataSO", order = 2)]
public class DataSO : ScriptableObject
{
    [Header("Levels")]
    [SerializeField] private LevelDataSO[] _levels;

    [Header("Characters")]
    [SerializeField] private Character[] _characters;
    
    private int _curLevelId;

    public bool IsLoadFromMenu { get; private set; }
    public LevelDataSO CurLevel => _levels[_curLevelId];
    public int LevelsAmount => _levels.Length;

    public void SetLevelId(int levelId)
    {
        if (levelId < 0)
            throw new ArgumentOutOfRangeException(nameof(levelId));

        _curLevelId = levelId;
    }

    public void SetDialogDestination(bool isLoadFromMenu)
    {
        IsLoadFromMenu = isLoadFromMenu;
    }

    public Sprite GetCurCharacter()
    {
        Dictionary<CharacterType, Sprite> characters = _characters.ToDictionary(character => character.Type, character => character.Sprite);
        return characters[CurLevel.Character];
    }
}