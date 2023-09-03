using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Swipe : MonoBehaviour
{
    [Header("Characteristics")]
    [SerializeField] private float _minSwipeDistance;

    [Header("Components")]
    [SerializeField] private Audio _audio;
    [SerializeField] private Player _player;
    
    private Cube _selectedCube;
    private Vector2 _startPosition;
    private bool _wasCollision;
    private bool _canSwipe = true;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                CheckMobileSwipe();
                break;
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.WindowsPlayer:
                CheckComputerSwipe();
                break;
            default:
                CheckComputerSwipe();
                break;
        }
    }

    public void StopSwipe()
    {
        _canSwipe = false;
    }

    public void AllowSwipe()
    {
        _canSwipe = true;
    }

    private void CheckMobileSwipe() 
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _startPosition = touch.position;

                Ray ray = _camera.ScreenPointToRay(_startPosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.TryGetComponent(out CubeSide cubeSide)) 
                    {
                        _selectedCube = cubeSide.GetComponentInParent<Cube>();
                        _wasCollision = true;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (_wasCollision) 
                {
                    float verticalSwipe = touch.position.y - _startPosition.y;
                    float verticalSwipeDistance = Mathf.Abs(verticalSwipe);

                    if (verticalSwipeDistance >= _minSwipeDistance)
                    {
                        float verticalSwipeDirection = Mathf.Sign(verticalSwipe);

                        if (verticalSwipeDirection > 0)
                        {
                            RotateCube(false);
                        }
                        else
                        {
                            RotateCube(true);
                        }
                    }

                    _wasCollision = false;
                    _startPosition = Vector2.zero;
                }
            }
        }
    }
    private void CheckComputerSwipe() 
    {
        if (Input.GetMouseButtonDown(0)) 
        {   
            _startPosition = Input.mousePosition;

            Ray ray = _camera.ScreenPointToRay(_startPosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out CubeSide cubeSide)) 
                {
                    _selectedCube = cubeSide.GetComponentInParent<Cube>();
                    _wasCollision = true;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            if (_wasCollision) 
            {
                float verticalSwipe = Input.mousePosition.y - _startPosition.y;
                float verticalSwipeDistance = Mathf.Abs(verticalSwipe);

                if (verticalSwipeDistance >= _minSwipeDistance)
                {
                    float verticalSwipeDirection = Mathf.Sign(verticalSwipe);

                    if (verticalSwipeDirection > 0)
                    {
                        RotateCube(false);
                    }
                    else
                    {
                        RotateCube(true);
                    }
                }

                _wasCollision = false;
                _startPosition = Vector2.zero;
            }
        }
    }

    private void RotateCube(bool isDown)
    {
        if (!_canSwipe)
            return;

        if (_selectedCube == null)
            throw new ArgumentNullException(nameof(_selectedCube));

        if (!_selectedCube.CanRotate)
        {
            _selectedCube = null;
            return;
        }

        _selectedCube.StartRotate(isDown);
        _player.TakeTurn();
        _audio.PlaySpinSound();
        _selectedCube = null;
    }
}