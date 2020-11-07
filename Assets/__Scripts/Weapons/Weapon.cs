using UnityEngine;

public abstract class Weapon
{
    public Ship parentShip;
    public int ammoRemaining = -1;
    public float muzzleVelocity = 1f;

    public Weapon(Ship parent, float muzzleVel, int ammo) {
        parentShip = parent;
        ammoRemaining = ammo;
        muzzleVelocity = muzzleVel;
    }

    abstract public void fire();

}
