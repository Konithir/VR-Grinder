using UnityEngine;

public class GrabbingPoint : MonoBehaviour
{
    [SerializeField]
    private HandsManager _handsManager;

    [SerializeField]
    private GrinderController _grinderController;

    private bool _isGrabbed = false;

    private bool _grabbedWithRight;
    private bool _grabbedWithLeft;

    public bool IsGrabbed
    {
        get { return _isGrabbed; }
    }

    private void Update()
    {
        if (!_isGrabbed)
            return;

        CheckForUngrab();
    }

    private void OnTriggerStay(Collider other)
    {
        if (_isGrabbed)
            return;

        CheckForGrab(other);
    }

    private void CheckForGrab(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if ((_handsManager.GetLeftHandGrabbingPressed() && !_handsManager.LeftHandOccupied) || (_handsManager.GetRightHandGrabbingPressed() && !_handsManager.RightHandOccupied))
            {
                if(_handsManager.GetLeftHandGrabbingPressed())
                {
                    _grabbedWithLeft = true;
                    _handsManager.LeftHandOccupied = true;
                }

                if(_handsManager.GetRightHandGrabbingPressed())
                {
                    _grabbedWithRight = true;
                    _handsManager.RightHandOccupied = true;
                }          

                Grab(other);
            }
        }
    }

    private void CheckForUngrab()
    {
        if(_grabbedWithLeft)
        {
            if(!_handsManager.GetLeftHandGrabbingPressed())
            {
                _handsManager.LeftHandOccupied = false;
                Ungrab();
            }
        }

        if (_grabbedWithRight)
        {
            if (!_handsManager.GetRightHandGrabbingPressed())
            {
                _handsManager.RightHandOccupied = false;
                Ungrab();
            }
        }
    }

    private void Grab(Collider other)
    {
        _isGrabbed = true;

        _grinderController.ParentGrinder(other.transform);
    }

    private void Ungrab()
    {
        _isGrabbed = false;
        _grabbedWithRight = false;
        _grabbedWithLeft = false;

        _grinderController.UnparentGrinder();
    }
}
