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
            if (_handsManager.GetLeftHandGrabbingPressed() || _handsManager.GetRightHandGrabbingPressed())
            {
                if(_handsManager.GetLeftHandGrabbingPressed())
                    _grabbedWithLeft = true;

                if(_handsManager.GetRightHandGrabbingPressed())
                    _grabbedWithRight = true;

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
                Ungrab();
            }
        }

        if (_grabbedWithRight)
        {
            if (_handsManager.GetRightHandGrabbingPressed())
            {
                Ungrab();
            }
        }
    }

    private void Grab(Collider other)
    {
        _isGrabbed = true;

        if(_grinderController.transform.parent == null)
        {
            _grinderController.transform.SetParent(other.gameObject.transform);
            _grinderController.Rigidbody.isKinematic = true;
            _grinderController.Rigidbody.useGravity = false;
        }
      
    }

    private void Ungrab()
    {
        _isGrabbed = false;
        _grabbedWithLeft = false;
        _grabbedWithRight = false;
        _grinderController.transform.parent = null;
        _grinderController.Rigidbody.isKinematic = false;
        _grinderController.Rigidbody.useGravity = true;
    }
}
