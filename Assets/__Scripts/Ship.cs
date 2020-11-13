using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private Dictionary<string, Weapon> weapons = new Dictionary<string, Weapon>();
    private string selectedWeapon = "";

    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    public bool hasWeapon(string name) {
        return weapons.ContainsKey(name);
    }

    public bool addWeapon(Weapon weapon) {
        if (hasWeapon(weapon.name))
            return false;

        weapons.Add(weapon.name, weapon);
        weapon.attachToShip(this);
        return true;
    }

    public bool removeWeapon(string name) {
        return weapons.Remove(name);
    }

    public Weapon getWeapon(string name) {
        Weapon foundWeapon;
        if (weapons.TryGetValue(name, out foundWeapon))
            return foundWeapon;
        else
            return null;
    }

    public bool selectWeapon(string name) {
        if (hasWeapon(name)) {
            selectedWeapon = name;
            return true;
        }

        return false;
    }

    public void FireWeapon() {
        Weapon weapon = getWeapon(selectedWeapon);
        if (weapon != null) {
            weapon.fire();
        }
    }
}
