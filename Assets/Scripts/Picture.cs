using System.Collections.Generic;
using UnityEngine;

public class Picture : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private LevelGenerator _levelGenerator;
    
    private Renderer _rend;
    private List<Material> _pictures;
    private int _index;

    private void Awake()
    {
        _rend = GetComponent<Renderer>();
    }

    private void Start()
    { 
        _levelGenerator.LevelLoaded += SetPictures;
    }

    private void OnDisable()
    {
        _levelGenerator.LevelLoaded -= SetPictures;
    }

    public void NextPicture()
    {
        if (_index == _pictures.Count - 1)
        {
            _rend.material = _pictures[0];
            _index = 0;
        }
        else
        {
            _rend.material = _pictures[_index + 1];
            _index++;
        }
        
        Player.Instance.TakeTurn();
    }

    private void SetPictures()
    {
        LevelDataSO curLevel = _levelGenerator.CurLevel;
        _pictures = new List<Material>(curLevel.Pictures);
        _rend.material = _pictures[0];
    }
}