using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public GameObject enemyProjectile;
  public GameObject enemyProjectileClone;
  public GameObject enemy;

  private float _timer;
  private int _numOfMovements;
  private float _movementAmount = 0.19f;
  private float _fireTimer;
  private float _timeToFire = 0.1f;
  private float _maxY = -3.0f;

  // Start is called before the first frame update
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    if (GameManager.instance.playGame)
    {
      _timer += Time.deltaTime;

      // Move enemies horizontally
      if (_timer > 0.5f && _numOfMovements != 17)
      {
        transform.Translate(new Vector3(_movementAmount, 0, 0));
        _timer = 0;
        _numOfMovements++;
      }

      // Move enemies vertically and reverse horizontal movement direction
      if (_numOfMovements == 17)
      {
        transform.Translate(0, -1, 0);
        _numOfMovements = 0;
        _timer = 0;
        _movementAmount = -_movementAmount;

        if (transform.position.y < _maxY)
        {
          GameManager.instance.enemyBreach = true;
          GameManager.instance.playGame = false;
        }
      }

      _fireTimer += Time.deltaTime;
      if (_fireTimer > _timeToFire)
      {
        FireEnemyProjectile();
        _fireTimer = 0;
      }
    }
  }

  private void FireEnemyProjectile()
  {
    if (Random.Range(0f, 125f) < 1)
    {
      enemyProjectileClone = Instantiate(enemyProjectile, new Vector3
          (enemy.transform.position.x, enemy.transform.position.y - 0.6f),
        enemy.transform.rotation);
    }
  }
}