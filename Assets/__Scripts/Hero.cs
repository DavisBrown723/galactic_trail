using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Ship
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

    private string[] weaponSlots = new string[] {"","","",""};
    int selectedWeaponSlot = 0;

    public Hero() {

    }

    void Awake()
    {
        addWeapon(new StandardWeapon("Basic", this, projectilePrefab, 40, -1));
        addWeapon(new ShotgunWeapon("Shotgun", this, projectilePrefab, 40, -1));

        assignWeaponToSlot(getWeapon("Basic"), 0);
        assignWeaponToSlot(getWeapon("Shotgun"), 1);

        selectWeapon("Basic");

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
            FireWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) {
            Debug.Log("Weapon 1");
           selectWeaponSlot(0);
        } else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) {
            Debug.Log("Weapon 2");
            selectWeaponSlot(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) {
            selectWeaponSlot(2);
        } else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) {
            selectWeaponSlot(3);
        }
    }

    void assignWeaponToSlot(Weapon weapon, int slot) {
        weaponSlots[slot] = weapon.name;
    }

    void selectWeaponSlot(int slot) {
        if (slot < 0 || slot > 3)
            return;

        selectedWeaponSlot = slot;
        selectWeapon(weaponSlots[slot]);
    }

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
