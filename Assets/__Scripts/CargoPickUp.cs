using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CargoPickUp : MonoBehaviour
{
    // static public int numCargo = 0; 
    void Update(){
        Text gt = this.GetComponent<Text>();
        gt.text = "Cargo Retrieved: " + PersistentData.numCargo;
    }// end Update()
}// end class CargoPickUp
