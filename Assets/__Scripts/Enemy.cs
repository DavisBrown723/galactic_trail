using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // added 11/07
public class Enemy : MonoBehaviour
{
    [Header("Set in Inspector: Enemy")]
    public float speed = 10f;       //the speed in m/s
    public float fireRate = 0.3f;   //seconds/shot (unused)
    public float health = 10;
    public int score = 100;         //points earned for destroying this

    private BoundsCheck bndCheck;

     [Header("Set Dynamically")]

    // Attempting to add scoring code on 11/07/2020, following apple picker prototype for now
    // probably not exactly what we want, but if it works, we can modify it from here. 
    public Text scoreGT;

    void Start(){
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        scoreGT = scoreGO.GetComponent<Text>();
        scoreGT.text = "Enemies hit: 0";

    }// end Start()

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
            Destroy(otherGO);
            Destroy(gameObject);
            int score = int.Parse(scoreGT.text);
            score+=1;
            scoreGT.text = "Enemies hit: " + score.ToString();
        }
        else
        {
            print("Enemy hit by non-ProjectileHero: " + otherGO.name);
        }
    }

}
