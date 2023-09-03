using UnityEngine;
using System.Collections.Generic;

public class Cube : MonoBehaviour
{
    [Header("Characteristics")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;
    
    private Transform _target;
    private bool _isRotating;
    private bool _isLongTapState;
    private Quaternion _targetRotation;
    private CubeRotationState _curRotationState;

    public bool CanRotate { get; private set; } = true;
    public bool CanLongTap { get; private set; } = true;

    private void FixedUpdate()
    {
        if (_target != null)
        {
            transform.position = Vector3.Lerp(transform.position, _target.position, _movementSpeed * Time.deltaTime);

            if (_target.position == transform.position)
            {
                _target = null;
            }
        }

        if (_isRotating)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);

            if (transform.rotation == _targetRotation)
            {
                _isRotating = false;
            }

            if (Quaternion.Angle(transform.rotation, _targetRotation) <= 5)
            {
                CanLongTap = true;
            }
        }
    }

    public void StartMove(Transform targetPosition) 
    {
        _target = targetPosition;
    }

    public void StartRotate(bool isDown) 
    {
        if (!CanRotate)
            return;

        if (_isLongTapState)
            return;

        _curRotationState = isDown ? CubeState.GetNextRotationState(_curRotationState) : CubeState.GetPreviousRotationState(_curRotationState);
        _targetRotation = CubeState.Rotation[_curRotationState];
        _isRotating = true;
        CanLongTap = false;

        if (_curRotationState == CubeRotationState.Front)
        {
            CubesTrigger.Instance.AddCorrectLetter(transform);
            CanRotate = false;
        }
    }

    public void SetRotationState(CubeRotationState state)
    {
        _curRotationState = state;
    }

    public void OpenAllLetters()
    {
        if (!CanRotate)
            return;
        
        if (Player.Instance.IsLongTap)
            return;

        EnableLongTapState();

        int activeSideIndex = ((int)_curRotationState + 3) % 4;
        Quaternion activeSideRotation = transform.GetChild(activeSideIndex).rotation;
        var sideIndexes = new List<int>();
        const float yOffset = 0.85f;
        
        for (int i = activeSideIndex + 1; i < activeSideIndex + 4; i++)
        {
            sideIndexes.Add(i % 4);
        }
        
        for (int i = 0; i < 3; i++)
        {
            int sideIndex = sideIndexes[i];
            Vector3 sidePosition = new Vector3(transform.position.x, transform.position.y + yOffset * (i + 1), transform.position.z);
            transform.GetChild(sideIndex).SetPositionAndRotation(sidePosition, activeSideRotation);
        }
    }

    public void SelectLetterWithLongTap(int sideindex)
    {
        DisableLongTapState();
        StartRotate(sideindex);
    }

    private void EnableLongTapState()
    {
        CanRotate = false;
        _isLongTapState = true;
        Player.Instance.SetLongTapState(true);

        foreach (var side in transform.GetComponentsInChildren<CubeSide>())
        {
            side.EnableLongTapState();
        }

        Player.Instance.TakeTurn();
    }

    private void DisableLongTapState()
    {
        foreach (var side in transform.GetComponentsInChildren<CubeSide>())
        {
            side.DisableLongTapState();
        }

        CanRotate = true;
        _isLongTapState = false;
        Player.Instance.SetLongTapState(false);
    }

    private void StartRotate(int sideIndex)
    {
        if (!CanRotate)
            return;

        _curRotationState = (CubeRotationState)((sideIndex + 1) % 4);
        _targetRotation = CubeState.Rotation[_curRotationState];
        _isRotating = true;

        if (_curRotationState == CubeRotationState.Front)
        {
            CubesTrigger.Instance.AddCorrectLetter(transform);
            CanRotate = false;
        }
    }
}