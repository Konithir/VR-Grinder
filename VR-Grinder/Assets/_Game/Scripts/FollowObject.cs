using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField]
    private Transform _followTarget;

    [SerializeField]
    private Rigidbody _rigidbody;

    private Quaternion _tempRotation;
    private Vector3 _tempRotationVector;

    public Transform FollowTarget
    {
        get { return _followTarget; }
        set { _followTarget = value; }
    }

    private void FixedUpdate()
    {
        if(_followTarget == null)
        {
            return;
        }

        _rigidbody.velocity = (_followTarget.position - transform.position) / Time.fixedDeltaTime;

        _tempRotation = _followTarget.rotation * Quaternion.Inverse(transform.rotation);
        _tempRotation.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);

        _tempRotationVector = angleInDegree * rotationAxis;

        _rigidbody.angularVelocity = (_tempRotationVector * Mathf.Deg2Rad) / Time.fixedDeltaTime;
    }
}
