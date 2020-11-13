using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon : Weapon
{
    public ShotgunWeapon(Ship parent) : base("Triple Shot", parent) {
        ammoRemaining = -1;
        muzzleVelocity = 40f;
        projectilePrefab = Resources.Load<GameObject>("Prefabs/Projectile");
    }

    public override void fire() {
        if (ammoRemaining != 0) {
            GameObject projLeft = GameObject.Instantiate(projectilePrefab);
            projLeft.transform.position = parentShip.pos;
            projLeft.GetComponent<Rigidbody>().velocity = (Vector3.up * muzzleVelocity) + (Vector3.left * 0.25f * muzzleVelocity);

            GameObject projMiddle = GameObject.Instantiate(projectilePrefab);
            projMiddle.transform.position = parentShip.pos;
            projMiddle.GetComponent<Rigidbody>().velocity = Vector3.up * muzzleVelocity;

            GameObject projRight = GameObject.Instantiate(projectilePrefab);
            projRight.transform.position = parentShip.pos;
            projRight.GetComponent<Rigidbody>().velocity = (Vector3.up * muzzleVelocity) + (Vector3.right * 0.25f * muzzleVelocity);

            ammoRemaining--;
        }
    }
}
