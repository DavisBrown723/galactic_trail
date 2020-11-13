using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour
{
    /*
        From the Unity Manual
        When a mouse hovers over a planet, it changes to a yellow. This
        will hopefully guide the player and let them know they can manipulate
        some of the planets. (larger ones)

    */
    public Renderer rend;
    public Color originalPlanetColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalPlanetColor = rend.material.color;
    }

 
    void OnMouseEnter()
    {
        rend.material.color = Color.yellow;
    }

  
    void OnMouseOver()
    {
        rend.material.color -= new Color(0, 0, 0) * Time.deltaTime;
    }

    // ...and the mesh finally turns white when the mouse moves away.
    void OnMouseExit()
    {
        rend.material.color = originalPlanetColor;
    }
}// end class Glow
