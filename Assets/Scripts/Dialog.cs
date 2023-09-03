using UnityEngine;

[System.Serializable]
public struct Dialog
{
    [SerializeField]
    [TextArea(5, 5)]
    private string _characterText;
    
    [SerializeField]
    [TextArea(5, 5)]
    private string _predictorText;

    public string CharacterText => _characterText;
    public string PredictorText => _predictorText;
}