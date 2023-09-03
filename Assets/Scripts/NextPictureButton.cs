using UnityEngine;

public class NextPictureButton : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private Transform _frameCorner;
    
    private Camera _camera;
    
    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        transform.position = _camera.WorldToScreenPoint(_frameCorner.position);
    }
}