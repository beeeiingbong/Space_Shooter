using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{      
    [SerializeField]private GameObject enemyShipPrefab;
    [SerializeField]private GameObject[] powerups; 
    //Reference to the GameManager
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(EnenmySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    // Update is called once per frame
    //create a coroutine to spawn every 5sec

    public void StartSpawnRoutines()
    {
        StartCoroutine(EnenmySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    IEnumerator EnenmySpawnRoutine()
    {
        while(_gameManager.gameOver == false)
        {
            Instantiate(enemyShipPrefab, new Vector3(Random.Range(-7f,7f),7,0),Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

   IEnumerator PowerupSpawnRoutine()
   {
       while(_gameManager.gameOver == false)
       {
        int randomPowerup = Random.Range(0, 3);
        Instantiate(powerups[randomPowerup], new Vector3(Random.Range(-7, 7), 7, 0),Quaternion.identity);
        yield return new WaitForSeconds(5.0f);

       }

   }      
  
}
