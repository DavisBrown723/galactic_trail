using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardWeapon : Weapon
{
    private GameObject projectilePrefab;

    public StandardWeapon(string weaponName, Ship parent, GameObject projPrefab, float muzzleVelocity, int ammo): base(weaponName, parent, muzzleVelocity, ammo) {
        projectilePrefab = projPrefab;
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
