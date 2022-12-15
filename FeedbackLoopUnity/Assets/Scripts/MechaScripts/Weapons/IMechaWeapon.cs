using UnityEngine;

public interface IMechaWeapon
{
    public void FireWeapon();
    public void AimWeapon();
    public ParticleSystem GetFiringEffect();
}
