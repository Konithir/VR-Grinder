using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrinderController : MonoBehaviour
{
    [SerializeField]
    private HandsManager _handsManager;

    [SerializeField]
    private Animator _grinderAnimator;

    [SerializeField]
    private Animator _bladeAnimator;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private GrabbingPoint _mainGrabbingPoint;

    [SerializeField]
    private GrabbingPoint _secondaryGrabbingPoint;

    [SerializeField]
    private Rigidbody _rigidBody;

    private bool _isWorking;

    public UnityEvent OnGrinderWorking;
    public UnityEvent OnGrinderStartWorking;
    public UnityEvent OnGrinderStopWorking;

    public GrabbingPoint MainGrabbingPoint
    {
        get { return _mainGrabbingPoint; }
    }

    public GrabbingPoint SecondaryGrabbingPoint
    {
        get { return _secondaryGrabbingPoint; }
    }

    public Rigidbody Rigidbody
    {
        get { return _rigidBody; }
    }

    public bool IsWorking
    {
        get { return _isWorking; }
    }

    private void Update()
    {
        if(_mainGrabbingPoint.IsGrabbed)
        {
            GrinderWorking();
        }
        else
        {
            StopGrindingWork();
        }
    }

    private void GrinderWorking()
    {
        if (_handsManager.GetLeftHandPrimaryPressed() || _handsManager.GetRightHandPrimaryPressed())
        {
            OnGrinderWorking?.Invoke();
            _grinderAnimator.SetBool("Grinding", true);
            _bladeAnimator.SetBool("Grinding", true);

            if(!_isWorking)
            {
                OnGrinderStartWorking?.Invoke();
            }

            _isWorking = true;
        }
        else
        {
            StopGrindingWork();
        }
    }

    private void StopGrindingWork()
    {
        if(_isWorking)
        {
            OnGrinderStopWorking?.Invoke();
        }

        _isWorking = false;
        _grinderAnimator.SetBool("Grinding", false);
        _bladeAnimator.SetBool("Grinding", false);
    }

    public void UnparentGrinder()
    {
        if(!_mainGrabbingPoint.IsGrabbed && !SecondaryGrabbingPoint.IsGrabbed)
        {
            transform.parent = null;
            Rigidbody.isKinematic = false;
            Rigidbody.useGravity = true;
        }
    }

    public void ParentGrinder(Transform newParent)
    {
        if (transform.parent == null)
        {
            transform.localEulerAngles = newParent.transform.eulerAngles;
            transform.SetParent(newParent);
            Rigidbody.isKinematic = true;
            Rigidbody.useGravity = false;
        }
    }
}
