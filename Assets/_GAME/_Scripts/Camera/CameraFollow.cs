using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float _smoothSpeed = 0.15f;
    [SerializeField] Transform _targetToFollow;
    [SerializeField] Vector3 _cameraOffset;

    void LateUpdate()
    {
        Vector3 desiredPosition = _targetToFollow.position + _cameraOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
        transform.position = smoothedPosition;
    }

    // public Transform target;
    // public Vector3 offset;
    // public float damping;
    // private Vector3 velocity = Vector3.zero;

    // private void FixedUpdate()
    // {
    //     Vector3 movePosition = target.position + offset;
    //     transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
    // }
}
