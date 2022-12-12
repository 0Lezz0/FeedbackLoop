using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to determinatediferent states of the playable character
/// It probably should be a singleton
/// </summary>
public class MechaStatus : MonoBehaviour
{
    [SerializeField]
    private bool _isFlying, _isDashing, _isFalling;

    [SerializeField]
    private bool _isFlyingAllowed, _isGroundMovementAllowed;

    [SerializeField]
    private float _groundSpeed, _airSpeed, _verticalSpeed;
    [SerializeField]
    private float _airDashImpulse;
    [SerializeField]
    private float _airDashCooldown, _airDashDuration;
    public bool IsFlying { get => _isFlying; set => _isFlying = value; }
    public bool IsDashing { get => _isDashing; set => _isDashing = value; }
    public bool IsFalling { get
        {
            if (IsFlying)
                return false;

            return _isFalling;
        }
        set => _isFalling = value; }
    public float GroundSpeed { get => _groundSpeed; set => _groundSpeed = value; }
    public float AirSpeed { get => _airSpeed; set => _airSpeed = value; }
    public float VerticalSpeed { get => _verticalSpeed; set => _verticalSpeed = value; }
    public float AirDashImpulse { get => _airDashImpulse; set => _airDashImpulse = value; }
    public float AirDashCooldown { get => _airDashCooldown; set => _airDashCooldown = value; }
    public float AirDashDuration { get => _airDashDuration; set => _airDashDuration = value; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleFlight()
    {
        IsFlying = !IsFlying;
    }
    public void ToggleDashState()
    {
        IsDashing = !IsDashing;
    }
    public void ToggleFallingState()
    {
        IsFalling = !IsFalling;
    }

}
