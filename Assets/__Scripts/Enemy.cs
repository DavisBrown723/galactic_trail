using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // added 11/07
public class Enemy : MonoBehaviour
{
    
    Vector3 medShipPos;
    [Header("Set in Inspector: Enemy")]
    public float speed = 10f;       //the speed in m/s
    public float fireRate = 0.3f;   //seconds/shot (unused)
    public float health = 10;
    public int score = 100;         //points earned for destroying this

    private BoundsCheck bndCheck;
    public GameObject HealthPack;
    
    

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }
    
    //This is a property: a method that acts like a field
    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
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
                medShipPos = gameObject.transform.position;
                Destroy(otherGO);
                Destroy(gameObject);
                score+=1;
                Main.scoreGT.text = score.ToString();
                GameObject healthPack = Instantiate<GameObject>(HealthPack);
                healthPack.transform.position = medShipPos;
                print("Inside if statement!");

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
