using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemiesMissed : MonoBehaviour
{
    static public int numEnemies = 0; 
    void Start(){
        Text gt = this.GetComponent<Text>();
        gt.text = "Enemies Missed: "+numEnemies;
    }// end Update()
}// end class EnemiesMissed
