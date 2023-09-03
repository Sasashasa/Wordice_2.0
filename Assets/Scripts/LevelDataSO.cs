using UnityEngine;

[CreateAssetMenu(fileName = "Level_0", menuName = "ScriptableObjects/LevelDataSO", order = 1)]
public class LevelDataSO : ScriptableObject
{
    [Header("Characteristics")]
    public int Id;
    public string Word;
    public int Turns;
    public CharacterType Character;

    [Header("Dialog")]
    public Dialog Dialog;

    [Header("Pictures")]
    public Material[] Pictures;
}