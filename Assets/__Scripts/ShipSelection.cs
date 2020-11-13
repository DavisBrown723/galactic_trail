using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSelection : MonoBehaviour
{
    public static void SelectSide(string side) {
        PersistentData.ChosenSide = side;
    }
}
