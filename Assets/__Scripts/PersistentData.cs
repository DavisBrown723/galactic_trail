using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    public static string ChosenSide = "";
    public static int numCargo = 0;
    public static int valuePerCargo = 10;
    public static int numPoints = 0;

    public static List<Weapon> playerWeapons;

    void Awake()
    {
        if (playerWeapons == null) {
            playerWeapons = new List<Weapon>() {
                new StandardWeapon(null)
            };
        }
    }
}
