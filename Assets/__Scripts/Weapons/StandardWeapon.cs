using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardWeapon : Weapon
{

    public StandardWeapon(Ship parent): base("Single Shot", parent) {
        ammoRemaining = -1;
        muzzleVelocity = 40f;
        projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectile");
    }

    public override void fire() {
        if (ammoRemaining-- != 0) {
            var projectile = GameObject.Instantiate(projectilePrefab);
            Rigidbody rigidBody = projectile.GetComponent<Rigidbody>();

            projectile.transform.position = parentShip.pos;
            projectile.GetComponent<Rigidbody>().velocity = Vector3.up * muzzleVelocity;
        }
    }
}
