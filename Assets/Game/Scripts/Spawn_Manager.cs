using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour {
    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerups;
    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(enemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
        
    }
    public void StartSpawnRoutine()
    {
        StartCoroutine(PowerupSpawnRoutine());
        StartCoroutine(enemySpawnRoutine());
    }

    IEnumerator PowerupSpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            
            int randromPowerup = Random.Range(0, 3);
            Instantiate(powerups[randromPowerup], new Vector3(Random.Range(-7, 7), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(10f);
        }
    }
    IEnumerator enemySpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            Instantiate(enemyShipPrefab, new Vector3(Random.Range(-8.0f, 8.0f), 8, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }
	// Use this for initialization
	
	//create a coroutine to spawn enemy every 5 seconds
    // while our player is alive 

	
}
