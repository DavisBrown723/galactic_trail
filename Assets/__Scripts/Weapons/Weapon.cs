using UnityEngine;

public abstract class Weapon
{
    public string name = "";
    public Ship parentShip = null;
    public int ammoRemaining = -1;
    public float muzzleVelocity = 1f;

    public Weapon(string weaponName, Ship parent, float muzzleVel, int ammo) {
        name = weaponName;
        parentShip = parent;
        ammoRemaining = ammo;
        muzzleVelocity = muzzleVel;
    }

    abstract public void fire();

}
