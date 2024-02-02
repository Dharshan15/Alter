using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private float spawnRange = 9.0f;
    public int enemyCount;
    public int waveNumber=1;

    public TextMeshProUGUI score;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button mainMenuButton;

    public PlayerController playerController;
    public AudioSource fall;
    private bool hasPlayed;

    // Start is called before the first frame update
    void Start()
    {
        Enemy.score = 0;
        hasPlayed = false;
        playerController = FindObjectOfType<PlayerController>();
        //Time.timeScale = 1;
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount==0 && !playerController.isGameOver)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
        score.text = "Score : "+Enemy.score;
        if(playerController.isGameOver)
        {
            if(!hasPlayed)
            {
                fall.Play();
                hasPlayed = true;
            }
            mainMenuButton.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(true);
        }
    }
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for(int i=0 ; i<enemiesToSpawn ; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }

    public void GameOver()
    {        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
