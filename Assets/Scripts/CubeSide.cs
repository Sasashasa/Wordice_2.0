using UnityEngine;
using UnityEngine.EventSystems;

public class CubeSide : MonoBehaviour
{
    private bool _isLongTapState = false;
    private Vector3 _defaultPosition;
    private Quaternion _defaultRotation;
    private bool _canLongTap = true;

    private void Awake()
    {
        _defaultPosition = transform.localPosition;
        _defaultRotation = transform.localRotation;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (_isLongTapState)
            transform.GetComponentInParent<Cube>().SelectLetterWithLongTap(transform.GetSiblingIndex());
    }
    
    private void OnMouseUpAsButton()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        bool isCubeCanLongTap = transform.GetComponentInParent<Cube>().CanLongTap;

        if (isCubeCanLongTap)
        {
            if (!_isLongTapState && _canLongTap)
            {
                GetComponentInParent<Cube>().OpenAllLetters();
            }
            
            _canLongTap = !_canLongTap;
        }
    }

    public void EnableLongTapState()
    {
        _isLongTapState = true;
    }

    public void DisableLongTapState()
    {
        _isLongTapState = false;
        SetDefaltPositionAndRotation();
    }

    private void SetDefaltPositionAndRotation()
    {
        transform.localPosition = _defaultPosition;
        transform.localRotation = _defaultRotation;
    }
}   