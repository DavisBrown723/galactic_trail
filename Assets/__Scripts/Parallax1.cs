﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax1 : MonoBehaviour
{
    // Parallax1 created 10/06/2020 by kat, a duplicate of Parallax
    // might want to change some things and did not want to affect
    // other areas that are using the original Parallax script
    [Header("Set in Inspector")]
    public GameObject poi;              //the player ship
    public GameObject[] panels;         //the scrolling foregrounds
    public float scrollSpeed = -30f;
    //motionMult controls how much the panels react to player movement
    public float motionMult = 0.25f;

    private float panelHt;      //Height of each panel
    private float depth;        //Depth of panels (that is pos.z)


    // Start is called before the first frame update
    void Start()
    {
        panelHt = panels[0].transform.localScale.y;
        depth = panels[0].transform.position.z;
        //Set inital positions for panels
        panels[0].transform.position = new Vector3(0, 0, depth);
        panels[1].transform.position = new Vector3(0, panelHt, depth);
    }

    // Update is called once per frame
    void Update()
    {
        float tY, tX = 0;
        tY = Time.time * scrollSpeed % panelHt + (panelHt * 0.5f);

        if(poi != null)
        {
            tX = -poi.transform.position.x * motionMult;
        }

        //position panels[0]
        panels[0].transform.position = new Vector3(tX, tY, depth);
        //Then position panels[1] where needed to make a continuous starfield
        if(tY >= 0)
        {
            panels[1].transform.position = new Vector3(tX, tY - panelHt, depth);
        }
        else
        {
            panels[1].transform.position = new Vector3(tX, tY + panelHt, depth);
        }
    }
}
