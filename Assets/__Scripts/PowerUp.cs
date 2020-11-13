using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Set in Inspector: Enemy")]
    public float speed = 10f;       //the speed in m/s

    private BoundsCheck bndCheck;


    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

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

    void Update()
    {
        Move();
        if (bndCheck != null && bndCheck.offDown)
        {
            Destroy(gameObject);
        }
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    void OnCollisionEnter(Collision coll)
    {
        
        GameObject collidedWith = coll.gameObject;
        if (collidedWith.tag == "Hero")
        {
            
            /// print("Inside OnCollision Enter");
            Destroy(gameObject);
            
        }
      
    }
}
