using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject player;
    private float spawnRange = 50;
    public int enemyCount;
    public int waveNumber = 1;
    public bool gameActive;
    public AudioClip explosionSound;
    private AudioSource gameAudio;
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public TextMeshProUGUI pauseText;
    public Button startButton;
    public TextMeshProUGUI menuText;
    public int time;
    public TextMeshProUGUI timeText;


    // Start is called before the first frame update
    void Start()
    {
        gameAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        if (gameActive)
        {
            SpawnWaves();
        }

    }

    private Vector3 GenerateSpawnPosition()
    {
        Vector3 randomPos = new Vector3(0, 4, 0);
        do
        {
            float spawnPosX = Random.Range(-spawnRange, spawnRange);
            float spawnPosY = Random.Range(-spawnRange, spawnRange);
            randomPos = new Vector3(spawnPosX, 4, spawnPosX);
        }
        while (Vector3.Distance(randomPos, player.transform.position) < 30);
        return randomPos;
    }
    
    private float GenerateSpawnRotation()
    {
        float rotation = Random.Range(0,180);
        return rotation;
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            enemy.transform.Rotate(0, GenerateSpawnRotation(), 0, Space.World);

        }
    }

    private void SpawnWaves()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            SpawnEnemyWave(waveNumber);
            waveNumber++;
        }
    }

    public void StartGame()
    {
        gameActive = true;
        score = 0;
        updateScore(0);
        player.SetActive(true);
        startButton.gameObject.SetActive(false);
        menuText.gameObject.SetActive(false);
        StartCoroutine(Countdown());
    }

    public void GameOver(bool time)
    {
        if (!time)
        {
            gameAudio.PlayOneShot(explosionSound, 1.0f);
        }
        gameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void TogglePause()
    {
        gameActive = !gameActive;
        if (gameActive)
        {
            player.SetActive(true);
            pauseText.gameObject.SetActive(false);

        }
        else
        {
            player.SetActive(false);
            pauseText.gameObject.SetActive(true);
        }
    }

    public void updateScore(int scoreAdded)
    {
        score += scoreAdded;
        scoreText.text = "Score: " + score;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator Countdown()
    {
        while (gameActive)
        {
            yield return new WaitForSeconds(1);
            time -= 1;
            timeText.text = "Fuel Level: " + time; 
            if (time == 0)
            {
                GameOver(true);
                Destroy(player);
            }
        }
    }
}
