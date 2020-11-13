using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShield : MonoBehaviour
{
  [Header("Set in Inspector")]
    public float rotationsPerSecond = 0.6f;

    [Header("Set Dynamically")]
    public int levelShown = 0;

    // this non public variable will not appear in the inspector
    Material mat;

    void Start(){
        mat = GetComponent<Renderer>().material;
    }// end Start()

    void Update(){
        // read current shield level from MilitaryStyle Singleton
        int currLevel = Mathf.FloorToInt(Alien.s.shieldLevel);
        
        // if this is different from level shown
        if(levelShown != currLevel){
            levelShown = currLevel;
            // adjust the texture offset to show different shield level
            mat.mainTextureOffset = new Vector2(0.2f*levelShown, 0);
        }

        // rotate the shield a bit every frame in a time-based way
        float rZ = -(rotationsPerSecond*Time.time*360) % 360f;
        transform.rotation = Quaternion.Euler(0,0,rZ);
    }// end Update()
}// end class AlienShield()
