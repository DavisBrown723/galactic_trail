using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShip : MonoBehaviour
{
    static public AlienShip S; // singleton

    public enum FireMode { Basic, Triangle };
    [Header("Set in Inspector")]

    // these fields control the movement of the ship
    public float speed = 40;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f; //Added on pg575
    public GameObject projectilePrefab; //Added on pg578
    public float projectileSpeed = 40;

    [Header("Set Dynamically")]
    [SerializeField]                    //Added/Edited pg574
    private float _shieldLevel = 1;

    private GameObject lastTriggerGo = null;    //added on pg573
    private FireMode currFireMode = FireMode.Basic;
    private int shotsRemaining = -1;

     void Awake(){
        if(S == null){
            S = this; // set the singleton
        }else{
         
            // Drop in here if there are two game objects in the scene that have the AlienShip script
            // this has happened... 
            Debug.LogError("AlienShip.Awake() - Attempted to assign second AlienShip.S!");
        }
    }// end Awake()

     // Update is called once per frame
    void Update()
    {
        // pull in information from the Input class
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        // change the transform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        // rotate the ship to make it feel more dynamic
        transform.rotation = Quaternion.Euler(yAxis*pitchMult,xAxis*rollMult,0);
        // this line is used to give the ship a bit of rotation based on the speed 
        // at which it is moving, which can make the ship feel more reactive and 
        // juicy. 

        // allow the ship to fire!
        if(Input.GetKeyDown(KeyCode.Space)){
              TempFire();
        }
    
    }// end Update()




     void TempFire() 
    {
        switch (currFireMode)
        {
            case FireMode.Basic:
                {
                    GameObject projGO = Instantiate<GameObject>(projectilePrefab);
                    projGO.transform.position = transform.position;
                    Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
                    rigidB.velocity = Vector3.up * projectileSpeed;
                    break;
                }
            case FireMode.Triangle: {
                    GameObject projLeft = Instantiate<GameObject>(projectilePrefab);
                    projLeft.transform.position = transform.position;
                    projLeft.GetComponent<Rigidbody>().velocity = Vector3.up * projectileSpeed + Vector3.left * projectileSpeed;

                    GameObject projMiddle = Instantiate<GameObject>(projectilePrefab);
                    projMiddle.transform.position = transform.position;
                    projMiddle.GetComponent<Rigidbody>().velocity = Vector3.up * projectileSpeed;

                    GameObject projRight = Instantiate<GameObject>(projectilePrefab);
                    projRight.transform.position = transform.position;
                    projRight.GetComponent<Rigidbody>().velocity = Vector3.up * projectileSpeed + Vector3.right * projectileSpeed;
                    break;
                }
        }

        if (--shotsRemaining == 0)
        {
            currFireMode = FireMode.Basic;
        }
    }// end TempFire()


     void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        //print("Triggered: " + go.name);

        //Make sure it's not the same triggering go as last time
        if(go == lastTriggerGo)
        {
            return;
        }
        lastTriggerGo = go;
        
        if (go.tag == "Enemy")
        {
            shieldLevel--;
            Destroy(go);
        }
        else if (go.tag == "PowerUp")
        {
            currFireMode = FireMode.Triangle;
            shotsRemaining = 5;

            Destroy(go);
        }
        else
        {
            print("Triggered by non-Enemy: " + go.name);
        }
    }// end OnTriggerEnter(Collider)

    //Added pg574
    public float shieldLevel
    {
        get
        {
            return (_shieldLevel);
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4);
            //if the shield is going to be set to less than zero
            if (value < 0)
            {
                Destroy(this.gameObject);

                Main.s.DelayedRestart(gameRestartDelay);
            }
        }
    }// end shieldLevel


}// end class AlienShip
