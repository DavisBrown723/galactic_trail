using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryShield : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float rotationsPerSecond = 0.1f;

    [Header("Set Dynamically")]
    public int levelShown = 0;


    //this nonpublic variable will not appear in the inspector
    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //Read the current shield level from the Hero singleton
        int currLevel = Mathf.FloorToInt(Military.s.shieldLevel);
        //if this is different than level shown,   ensures the shield jumps to the new Xoffset rather than show an offset between two shield icons
        if(levelShown != currLevel)
        {
            levelShown = currLevel;
            //Adjust the texture offset to show different shield level
            mat.mainTextureOffset = new Vector2(0.2f * levelShown, 0);
        }
        //Rotate the shield a bit every frame in a time-based way
        float rZ = -(rotationsPerSecond * Time.time * 360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, rZ);        //Used to rotate slowly about the Z-axis
    }
}
