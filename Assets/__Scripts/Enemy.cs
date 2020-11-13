using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // added 11/07
public class Enemy : Ship
{
    
    Vector3 shipPOS;
    [Header("Set in Inspector: Enemy")]
    public float speed = 10f;       //the speed in m/s
    public float fireRate = 0.3f;   //seconds/shot (unused)
    public float health = 10;
    public int score = 100;         //points earned for destroying this

    private BoundsCheck bndCheck;
    public GameObject HealthPack;
    public GameObject cargo0;
    public GameObject cargo1;
    
    
    

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if(bndCheck != null && bndCheck.offDown)
        {//Updated on page 565-566
            Destroy(gameObject);
            EnemiesMissed.numEnemies+=1;
        }
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    void OnCollisionEnter(Collision coll)    //Added pg580
    {
        
        GameObject otherGO = coll.gameObject;
        if(otherGO.tag == "ProjectileHero")
        {
            
            int score = int.Parse(Main.scoreGT.text);

            if(gameObject.tag == "MedicalShip"){
                shipPOS = gameObject.transform.position;
                Destroy(otherGO);
                Destroy(gameObject);
                score+=1;
                Main.scoreGT.text = score.ToString();
                GameObject healthPack = Instantiate<GameObject>(HealthPack);
                healthPack.transform.position = shipPOS;
                // print("Inside if statement!");

            }else if(gameObject.tag == "CargoShip"){
                shipPOS = gameObject.transform.position;
                Destroy(otherGO);
                Destroy(gameObject);
                score+=1;
                Main.scoreGT.text = score.ToString();
                GameObject cargo_0 = Instantiate<GameObject>(cargo0);
                GameObject cargo_1 = Instantiate<GameObject>(cargo1);
                cargo_0.transform.position = shipPOS;
                // shipPOS = Quaternion.AngleAxis(-45, Vector3.down) * shipPOS;
                shipPOS.y = shipPOS.y - 10f;
                shipPOS.x = shipPOS.x + 5f;
                cargo_1.transform.position = shipPOS;

            }else{
                Destroy(otherGO);
                Destroy(gameObject);
                score+=1;
                Main.scoreGT.text = score.ToString();

            }

            if(score > HighScore.score){
                HighScore.score = score;
            }
        }
        else
        {
            print("Enemy hit by non-ProjectileHero: " + otherGO.name);
        }
    }

}
