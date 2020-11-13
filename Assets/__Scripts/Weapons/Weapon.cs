using UnityEngine;

public abstract class Weapon
{
    public string name = "";
    public int ammoRemaining = -1;
    public float muzzleVelocity = 1f;
    public GameObject projectilePrefab;
    
    public Ship parentShip = null;

    public Weapon(string weaponName, Ship parent) {
        name = weaponName;
        parentShip = parent;
    }

    public void attachToShip(Ship parent) {
        parentShip = parent;
    }

    abstract public void fire();

}
