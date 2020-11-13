using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundOver : MonoBehaviour
{
    public void Awake() {
        PersistentData.numPoints += PersistentData.numCargo * PersistentData.valuePerCargo;
        PersistentData.numCargo = 0;
    }

    public void Continue() {
        string nextScene = PersistentData.ChosenSide == "Alien" ? "_Scene_AlienShip" : "_Scene_EarthShip";
        GetComponentInParent<LoadAScene>().LoadNextOrPrevScene(nextScene);
    }
}
