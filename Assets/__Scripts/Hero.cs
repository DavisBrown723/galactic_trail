using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    int selectedWeaponSlot = -1;

    public Hero() {

    }

    void Start()
    {
        int i = 0;
        foreach(var weapon in PersistentData.playerWeapons) {
            addWeapon(weapon);
            assignWeaponToSlot(weapon, i++);
        }

        selectWeaponSlot(0);

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
           selectWeaponSlot(0);
        } else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) {
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
        if (slot < 0 || slot > 3 || getWeapon(weaponSlots[slot]) == null)
            return;

        if (selectedWeaponSlot >= 0) {
            string oldButtonName = "ButtonWeapon" + selectedWeaponSlot.ToString();
            var oldSelButton = GameObject.Find(oldButtonName).GetComponent<Button>();
            var oldColors = oldSelButton.colors;
            oldColors.normalColor = Color.white;
            oldSelButton.colors = oldColors;
        }

        selectedWeaponSlot = slot;
        selectWeapon(weaponSlots[slot]);

        string newButtonName = "ButtonWeapon" + slot.ToString();
        var newSelButton = GameObject.Find(newButtonName).GetComponent<Button>();
        var newColors = newSelButton.colors;
        newColors.normalColor = Color.blue;
        newSelButton.colors = newColors;
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
        
        if (go.tag == "Enemy" || go.tag == "MedicalShip" || go.tag == "CargoShip")
        {
            if(go.tag == "Enemy"){
                EnemiesMissed.numEnemies = EnemiesMissed.numEnemies + 1;
            }
            shieldLevel--;
            Destroy(go); 
        }
        else if (go.tag == "PowerUp")
        {
            Destroy(go);
        }else if(go.tag == "HealthPack"){
          
            shieldLevel++;
            Destroy(go);
        }else if(go.tag == "Cargo"){
            PersistentData.numCargo += 1;
            // CargoPickUp.numCargo = CargoPickUp.numCargo + 1;
            Destroy(go);
            // print("Triggered Cargo");
        }else
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
