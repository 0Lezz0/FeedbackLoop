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

    [SerializeField]
    private bool _isFlying;
    [SerializeField]
    private float _groundSpeed, _airSpeed;
    [SerializeField]
    private Camera _mainMechaCamera;
    public bool IsFlying { get => _isFlying; set => _isFlying = value; }
    public float GroundSpeed { get => _groundSpeed; set => _groundSpeed = value; }
    public float AirSpeed { get => _airSpeed; set => _airSpeed = value; }
    public Camera MainMechaCamera { get => _mainMechaCamera; set => _mainMechaCamera = value; }

    // Start is called before the first frame update
    void Start()
    {
        controlSystem = new MechaControls();
        mechaActions = controlSystem.MechaActions;
        mechaActions.Enable();

        mechaRigidBody = gameObject.GetComponent<Rigidbody>();

        IsFlying = false;
    }

    // Update is called once per frame
    void Update()
    {
        movement = mechaActions.Movement.ReadValue<Vector2>();
        pitch = mechaActions.Pitchcontrol.ReadValue<float>();

        if (mechaActions.Dashjump.IsPressed())
        {
            shouldJumpOrDash = true;
        }
    }
    private void FixedUpdate()
    {
        if (IsFlying)
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
            ToggleFlight();
        }
    }


    private void MoveOnGround()
    {
        Vector3 velocity = new Vector3(movement.x, 0, movement.y) * GroundSpeed;

        mechaRigidBody.AddForce(velocity, ForceMode.Impulse);
    }

    private void MoveOnAir()
    {
        Vector3 velocity = new Vector3(movement.x, pitch, movement.y) * AirSpeed;

        mechaRigidBody.AddForce(velocity, ForceMode.Impulse);
    }

    private void IsTakeOff()
    {

    }

    private void CanDash()
    {

    }
    
    private void ToggleFlight()
    {
        IsFlying = !IsFlying;
    }
}
