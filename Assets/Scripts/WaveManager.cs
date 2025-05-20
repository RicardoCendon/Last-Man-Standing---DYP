using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // ¡Asegúrate de importar esto arriba!
using UnityEngine.SceneManagement; 
using UnityEngine.UI; // Para el mini sistema de UI oleada

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;

    public Transform[] spawnPointsEnemy1;
    public Transform[] spawnPointsEnemy2;
    public Transform[] spawnPointsEnemy3;

    public int currentLevel = 1;
    public float timeBetweenWaves = 5f;
    private int maxWaves = 6;


    private List<GameObject> currentEnemies = new List<GameObject>();
    private bool isSpawning = false;

    // Para mostrar el texto de la oleada
    public TextMeshProUGUI waveText;
    public float waveTextDisplayTime = 2f;

    void Update()
    {
        currentEnemies.RemoveAll(e => e == null);

        if (currentEnemies.Count == 0 && !isSpawning && currentLevel <= maxWaves)
        {
            StartCoroutine(SpawnNextWave());
        }

        else if (currentEnemies.Count == 0 && currentLevel > maxWaves && !isSpawning)
        {
            if (waveText != null)
            {
                waveText.text = "¡HAS GANADO!";
                waveText.gameObject.SetActive(true);
            }

            // Previene que se vuelva a mostrar
            isSpawning = true;
            StartCoroutine(ReturnToMenuAfterDelay(7f));
        }
    }


    IEnumerator SpawnNextWave()
    {
        isSpawning = true;

        if (waveText != null)
        {
            waveText.text = "OLEADA " + currentLevel;
            waveText.gameObject.SetActive(true);
            yield return new WaitForSeconds(waveTextDisplayTime);
            waveText.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(timeBetweenWaves);

        int enemiesToSpawn = 3 + currentLevel * 2;

        GameObject selectedPrefab = GetEnemyPrefabForLevel(currentLevel);
        Transform[] selectedSpawnPoints = GetSpawnPointsForLevel(currentLevel);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Transform spawnPoint = selectedSpawnPoints[Random.Range(0, selectedSpawnPoints.Length)];
            GameObject enemy = Instantiate(selectedPrefab, spawnPoint.position, Quaternion.identity);
            currentEnemies.Add(enemy);
            yield return new WaitForSeconds(0.2f);
        }

        currentLevel++;
        isSpawning = false;
    }

    GameObject GetEnemyPrefabForLevel(int level)
    {
        if (level <= 2)
            return enemyPrefab1;
        else if (level <= 4)
            return enemyPrefab2;
        else
            return enemyPrefab3;
    }

    Transform[] GetSpawnPointsForLevel(int level)
    {
        if (level <= 2)
            return spawnPointsEnemy1;
        else if (level <= 4)
            return spawnPointsEnemy2;
        else
            return spawnPointsEnemy3;
    }

    IEnumerator ReturnToMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Menú"); 
    }
}
