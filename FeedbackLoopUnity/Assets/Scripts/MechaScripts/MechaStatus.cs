using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to determinatediferent states of the playable character
/// </summary>
public class MechaStatus : MonoBehaviour
{
    [SerializeField]
    private bool _isFlying;
    [SerializeField]
    private bool _isDashing;
    public bool IsFlying { get => _isFlying; set => _isFlying = value; }
    public bool IsDashing { get => _isDashing; set => _isDashing = value; }

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
}
