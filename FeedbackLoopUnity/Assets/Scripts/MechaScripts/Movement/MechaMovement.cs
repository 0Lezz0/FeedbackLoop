using System.Collections;
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
    private bool shouldJumpOrDash, shouldTakeOff;
    private float verticalCameraRotation;
    private MechaStatus mechaStatus;

    [SerializeField]
    private float _verticalCameraLimitGround, _verticalCameraLimitAir;
    [SerializeField]
    private Camera _mainMechaCamera;
    [SerializeField]
    private Vector2 _mouseSensitivity;
    public Camera MainMechaCamera { get => _mainMechaCamera; set => _mainMechaCamera = value; }
    public Vector2 MouseSensitivity { get => _mouseSensitivity; set => _mouseSensitivity = value; }
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

        if (mechaActions.Pitchcontrol.WasPerformedThisFrame() && !mechaStatus.IsFlying)
        {
            shouldTakeOff = true;
        }

        if (mechaActions.TakeOff.triggered)
        {
            shouldTakeOff = true;
        }

        if (mechaActions.Dashjump.triggered)
        {
            shouldJumpOrDash = true;
        }

        AimCamera(mechaActions.Aim.ReadValue<Vector2>());
    }
    private void FixedUpdate()
    {
        if (!mechaStatus.IsDashing)
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
        }

        if (shouldJumpOrDash)
        {
            shouldJumpOrDash = false;
            Dash();
        }

        if (shouldTakeOff)
        {
            shouldTakeOff = false;
            TakeOff();
        }

        if (mechaStatus.IsFalling)
        {
            mechaStatus.IsFlying = false;
            mechaRigidBody.AddForce(Physics.gravity, ForceMode.VelocityChange);
        }
    }


    private void MoveOnGround()
    {
        Vector3 velocity = GetMovementDirection() * mechaStatus.GroundSpeed;

        mechaRigidBody.AddForce(velocity, ForceMode.Impulse);
    }

    private void MoveOnAir()
    {
        Vector3 moveDirection = GetMovementDirection();

        Vector3 velocity = new Vector3(moveDirection.x * mechaStatus.AirSpeed, 
            pitch * mechaStatus.VerticalSpeed, 
            moveDirection.z * mechaStatus.AirSpeed);

        mechaRigidBody.AddForce(velocity, ForceMode.Impulse);
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 cameraForward = new(gameObject.transform.forward.x, 0, gameObject.transform.forward.z);
        Vector3 cameraRight = new(gameObject.transform.right.x, 0, gameObject.transform.right.z);
        Vector3 moveDirection = cameraForward.normalized * movement.y + cameraRight.normalized * movement.x;
        return moveDirection;
    }

    private void TakeOff()
    {
        if (!mechaStatus.IsFlying)
        {
            mechaRigidBody.AddForce(gameObject.transform.up * mechaStatus.AirDashImpulse/3, ForceMode.VelocityChange);
            mechaStatus.IsFlying = true;
        }
        else {
            mechaStatus.ToggleFlight();
            mechaStatus.IsFalling = true;
        }
    }

    private bool CanDash()
    {
        return mechaStatus.IsFlying && !mechaStatus.IsDashing;
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

    private void Dash()
    {
        if (CanDash())
        {
            Vector3 movementDirection;
            if(pitch != 0)
            {
                movementDirection = gameObject.transform.up * pitch;
            } 
            else
            {
                movementDirection = GetMovementDirection();
            }

            StartCoroutine(DashCoroutine(movementDirection));
            mechaStatus.IsDashing = false;
        }
    }

    private IEnumerator DashCoroutine(Vector3 movementDirection)
    {

        mechaStatus.IsDashing = true;
        mechaRigidBody.AddForce(movementDirection * mechaStatus.AirDashImpulse, ForceMode.VelocityChange);
        yield return new WaitForSeconds(mechaStatus.AirDashDuration);
    }
}
