using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;

  public GameObject enemy;
  public GameObject enemies;
  public GameObject level2On;
  public GameObject level2Off;
  public GameObject level3On;
  public GameObject level3Off;
  public GameObject modalPanel;
  public GameObject startGamePanel;
  // public GameObject dialogBorder;
  public GameObject endGamePanel;
  public GameObject exitGamePanel;
  public GameObject[] livesImages;
  public Vector3 respawn = new Vector3(-1.6f, -4, 0);
  public Vector2Int size;
  public Vector2 offset;
  public TMP_Text scoreText;
  public TMP_Text levelText;
  public TMP_Text level1HitPointsDisplay;
  // public TMP_Text level2HitPointsDisplay;
  // public TMP_Text level3HitPointsDisplay;
  public TMP_Text endGameText;
  public int level1HitPoints = 25;
  public int level2HitPoints = 50;
  public int level3HitPoints = 200;
  public int score;
  public int enemyCount;
  public bool lifeLost;
  public bool playGame;
  public bool enemyBreach;
  public int lives = 5;
  public float enemySpeed;
  public float level1EnemySpeed = 0.5f;
  public float level2EnemySpeed = 0.4f;
  public float level3EnemySpeed = 0.3f;
  public float enemyFireRate;
  public float level1EnemyFireRate = 150f;
  public float level2EnemyFireRate = 125f;
  public float level3EnemyFireRate = 100f;
  public int timerResetCount;
  public bool resetTimer;
  
  private GameObject _newEnemy;
  private int _level = 1;
  private const int FreezeGame = 0;
  private const int UnfreezeGame = 1;
  private int _hitPoints;
  private bool _gameStart = true;
  private bool _resetEnemies;
  
  // Start is called before the first frame update
  void Start()
  {
    instance = this;
    LevelDisplay(_level);
    DrawEnemies();
    level1HitPointsDisplay.text = level1HitPoints.ToString("000");
    // level2HitPointsDisplay.text = level2HitPoints.ToString("000");
    // level3HitPointsDisplay.text = level3HitPoints.ToString("000");
    modalPanel.SetActive(true);
    startGamePanel.SetActive(true);
    // dialogBorder.SetActive(true);
    Time.timeScale = FreezeGame;
  }

  // Update is called once per frame
  void Update()
  {
    scoreText.text = score.ToString("00000");
    
    if ((lifeLost && lives <= 0) || enemyBreach)
    {
      playGame = false;
      lifeLost = false; 
      enemyBreach = false;
      endGameText.text = "GAME OVER!";
      modalPanel.SetActive(true);
      endGamePanel.SetActive(true);
      // dialogBorder.SetActive(true);
      Time.timeScale = FreezeGame;
    }
    else if (lifeLost)
    {
      lives--;
      livesImages[lives].SetActive(false);
      lifeLost = false;
    }

    if (playGame && enemyCount == 0)
    {
      playGame = false;
      endGameText.text = "YOU WIN!";
      modalPanel.SetActive(true);
      endGamePanel.SetActive(true);
      // dialogBorder.SetActive(true);
      Time.timeScale = FreezeGame;
    }

    if (Input.GetKeyDown(KeyCode.Escape) && !endGamePanel.activeInHierarchy && !startGamePanel.activeInHierarchy)
    {
      ExitPrompt();
    }
  }

  private void DrawEnemies()
  {
    for (int i = 0; i < size.x; i++)
    {
      for (int j = 0; j < size.y; j++)
      {
        _newEnemy = Instantiate(enemy, enemies.transform);
        _newEnemy.transform.position = new Vector3(i * offset.x - 5.5f,
          j * offset.y + 0.8f, 0);
        enemyCount++;
      }
    }
  }

  public void Restart()
  {
    if (modalPanel.activeInHierarchy)
    {
      modalPanel.SetActive(false);
    }

    if (startGamePanel.activeInHierarchy)
    {
      startGamePanel.SetActive(false);
      // dialogBorder.SetActive(false);
      Time.timeScale = UnfreezeGame;
    }

    if (endGamePanel.activeInHierarchy)
    {
      endGamePanel.SetActive(false);
      // dialogBorder.SetActive(false);
      Time.timeScale = UnfreezeGame;
    }

    _level = 1;
    LevelDisplay(_level);
    score = 0;
    scoreText.text = score.ToString("00000");
    _hitPoints = level1HitPoints;
    enemySpeed = level1EnemySpeed;
    enemyFireRate = level1EnemyFireRate;
  }

  private void LevelDisplay(int newLevel)
  {
    switch (newLevel)
    {
      case 1:
        level2On.SetActive(false);
        level3On.SetActive(false);
        level2Off.SetActive(true);
        level3Off.SetActive(true);
        break;
      case 2:
        level2On.SetActive(true);
        level3On.SetActive(false);
        level2Off.SetActive(false);
        level3Off.SetActive(true);
        break;
      case 3:
        level2On.SetActive(true);
        level3On.SetActive(true);
        level2Off.SetActive(false);
        level3Off.SetActive(false);
        break;
    }

    levelText.text = newLevel.ToString("0");
  }
  
  public void ExitPrompt()
  {
    modalPanel.SetActive(true);
    exitGamePanel.SetActive(true);
    // dialogBorder.SetActive(true);
    Time.timeScale = FreezeGame;
  }
  
  public void NoExit()
  {
    modalPanel.SetActive(false);
    exitGamePanel.SetActive(false);
    // dialogBorder.SetActive(false);
    Time.timeScale = UnfreezeGame;
  }

  public void ExitGame()
  {
    Debug.Log("Quitting game...");
    Application.Quit();
  }
}