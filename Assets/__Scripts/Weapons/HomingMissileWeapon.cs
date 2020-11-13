using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissileWeapon : Weapon
{
    public HomingMissileWeapon(Ship parent) : base("Homing Missile", parent) {
        ammoRemaining = -1;
        muzzleVelocity = 25f;
        projectilePrefab = Resources.Load<GameObject>("Prefabs/ProjectileMissile");
    }

    public override void fire() {
        if (ammoRemaining != 0) {
            GameObject proj = GameObject.Instantiate(projectilePrefab);
            proj.transform.position = parentShip.pos;
            proj.GetComponent<Rigidbody>().velocity = Vector3.up * muzzleVelocity;

            ammoRemaining--;
        }
    }
}
