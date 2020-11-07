using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hero : MonoBehaviour
{
    static public Hero s;       //Singleton
    public enum FireMode { Basic, Triangle };

    [Header("Set in Inspector")]
    //These fields control movement of the ship
    public float speed = 30;
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

    


    void Awake()
    {
        if (s == null)
        {
            s = this;
        }
        else
        {//Would happen if two GameObjects are in teh scene that have the heroScript attached
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S!");
        }
    }


   

    // Update is called once per frame
    void Update()
    {
        //Pull in infromation from teh Input class
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        //Change transform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        //Rotate the ship to make it feel more dynamic based on the speed which the ship is moving
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        // Allow the ship to fire //Added pg579
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TempFire();
        }
    }

    void TempFire() //Added pg579
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
    }

    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        // print("Triggered: " + go.name);

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
    }

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
    }

}
