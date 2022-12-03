using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaMovement : MonoBehaviour
{
    private MechaControls controlSystem;
    private MechaControls.MechaActionsActions mechaActions;

    private Rigidbody mechaRigidBody;


    [SerializeField]
    private Vector2 movement;
    [SerializeField]
    private float pitch;
    private bool shouldJumpOrDash;
    private float verticalCameraRotation;
    private MechaStatus mechaStatus;

    [SerializeField]
    private float _groundSpeed, _airSpeed, _verticalSpeed;
    [SerializeField]
    private float _verticalCameraLimitGround, _verticalCameraLimitAir;
    [SerializeField]
    private Camera _mainMechaCamera;
    [SerializeField]
    private Vector2 _mouseSensitivity;
    public float GroundSpeed { get => _groundSpeed; set => _groundSpeed = value; }
    public float AirSpeed { get => _airSpeed; set => _airSpeed = value; }
    public Camera MainMechaCamera { get => _mainMechaCamera; set => _mainMechaCamera = value; }
    public Vector2 MouseSensitivity { get => _mouseSensitivity; set => _mouseSensitivity = value; }
    public float VerticalSpeed { get => _verticalSpeed; set => _verticalSpeed = value; }
    public float VerticalCameraLimitGround { get => _verticalCameraLimitGround; set => _verticalCameraLimitGround = value; }
    public float VerticalCameraLimitAir { get => _verticalCameraLimitAir; set => _verticalCameraLimitAir = value; }

    // Start is called before the first frame update
    void Start()
    {
        controlSystem = new MechaControls();
        mechaActions = controlSystem.MechaActions;
        mechaActions.Enable();

        mechaRigidBody = gameObject.GetComponent<Rigidbody>();
        mechaStatus = gameObject.GetComponent<MechaStatus>();

        mechaStatus.IsFlying = false;
        verticalCameraRotation = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        movement = mechaActions.Movement.ReadValue<Vector2>();
        pitch = mechaActions.Pitchcontrol.ReadValue<float>();

        if (mechaActions.TakeOff.triggered)
        {
            mechaStatus.ToggleFlight();
        }

        AimCamera(mechaActions.Aim.ReadValue<Vector2>());
    }
    private void FixedUpdate()
    {
        if (mechaStatus.IsFlying)
        {
            mechaRigidBody.useGravity = false;
            MoveOnAir();
        }
        else
        {
            mechaRigidBody.useGravity = true;
            MoveOnGround();
        }

        if (shouldJumpOrDash)
        {
            shouldJumpOrDash = false;
            mechaStatus.ToggleFlight();
        }
    }


    private void MoveOnGround()
    {
        Vector3 cameraForward = new(gameObject.transform.forward.x, 0, gameObject.transform.forward.z);
        Vector3 cameraRight = new(gameObject.transform.right.x, 0, gameObject.transform.right.z);
        Vector3 moveDirection = cameraForward.normalized * movement.y + cameraRight.normalized * movement.x;

        Vector3 velocity = moveDirection * GroundSpeed;

        mechaRigidBody.AddForce(velocity, ForceMode.Impulse);
    }

    private void MoveOnAir()
    {
        Vector3 cameraForward = new(gameObject.transform.forward.x, 0, gameObject.transform.forward.z);
        Vector3 cameraRight = new(gameObject.transform.right.x, 0, gameObject.transform.right.z);
        Vector3 moveDirection = cameraForward.normalized * movement.y + cameraRight.normalized * movement.x;

        Vector3 velocity = new Vector3(moveDirection.x * AirSpeed, pitch * VerticalSpeed, moveDirection.z * AirSpeed);

        mechaRigidBody.AddForce(velocity, ForceMode.Impulse);
    }

    private void IsTakeOff()
    {

    }

    private void CanDash()
    {

    }
    
    private void AimCamera(Vector2 aimingPoint)
    {

        verticalCameraRotation -= (aimingPoint.y * Time.deltaTime) * MouseSensitivity.y;
        if (mechaStatus.IsFlying)
        {
            verticalCameraRotation = Mathf.Clamp(verticalCameraRotation, -1f * VerticalCameraLimitAir, VerticalCameraLimitAir);
        }
        else {
            verticalCameraRotation = Mathf.Clamp(verticalCameraRotation, -1f * VerticalCameraLimitGround, VerticalCameraLimitGround);
        }

        MainMechaCamera.transform.localRotation = Quaternion.Euler(verticalCameraRotation, 0, 0);
        transform.Rotate(Vector3.up * (aimingPoint.x * Time.deltaTime) * MouseSensitivity.x);
    }
}
