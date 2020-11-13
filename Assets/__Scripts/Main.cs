using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // added 11/07

public class Main : MonoBehaviour
{
    static public Main s;

    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies;
    public GameObject prefabPowerup;
    public float enemySpawnPerSecond = 0.5f;
    public float enemyDefaultPadding = 1.5f;
    public float powerUpSpawnPerSecond = 0.05f;

    private BoundsCheck bndCheck;
 
    [Header("Set Dynamically")] // 11/07 kat

    // Attempting to add scoring code on 11/07/2020, following apple picker prototype for now
    // probably not exactly what we want, but if it works, we can modify it from here. 
    public static Text scoreGT; // 11/07 kat, this is assigned in Main.cs because having
                                // a start() method here does not work well (every time 
                                // an enemy is spawned, start() is called and it's reset to 0. yikes!)
 

      void Start(){
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        scoreGT = scoreGO.GetComponent<Text>();
        scoreGT.text = "0";  
        EnemiesMissed.numEnemies = 0;
        // CargoPickUp.numCargo = 0;
    }// end Start()


    void Awake()
    {
        s = this;
        //Set bndCheck to reference the BoundsCheck component on this GameObject
        bndCheck = GetComponent<BoundsCheck>();
        //Invoke SpawnEnemy() once (in 2 seconds, based on default values)
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
        Invoke("SpawnPowerUp", 1f / powerUpSpawnPerSecond);
    }

    public void SpawnEnemy()
    {
        //Pick a rnadom Enemy prefab to instantiate
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

        //Position the enemy above the screen with a random x position
        float enemyPadding = enemyDefaultPadding;
        if(go.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }
        //Set the initial position for the spawned enemy
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;
        go.transform.position = pos;

        //invoke spawnEnemy again
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }

    public void SpawnPowerUp()
    {
        GameObject powerUp = Instantiate<GameObject>(prefabPowerup);

        float xPadding = 1.5f;
        if (powerUp.GetComponent<BoundsCheck>() != null)
        {
            xPadding = Mathf.Abs(powerUp.GetComponent<BoundsCheck>().radius);
        }

        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + xPadding;
        float xMax = bndCheck.camWidth - xPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + xPadding;
        powerUp.transform.position = pos;

        Invoke("SpawnPowerUp", 1f / powerUpSpawnPerSecond);
    }

    public void DelayedRestart(float delay) //Added pg576
    {
        //Invoke the restart method in delay seconds
        Invoke("Restart", delay);
    }
    
    public void Restart() //Added pg576
    {
        //Reload _Scene_0 to restart the game
        SceneManager.LoadScene("__Scene_GameOver");
    }

}
