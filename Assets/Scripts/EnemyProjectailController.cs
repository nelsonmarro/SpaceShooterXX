using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyProjectailController : MonoBehaviour
{
  public GameObject enemyProjectile;
  public float projectileSpeed = -3;

  // Start is called before the first frame update
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    // Move the projectile up the screen
    transform.Translate(new Vector3(0, projectileSpeed * Time.deltaTime, 0));
  }

  private void OnCollisionEnter2D(Collision2D collission)
  {
    if (collission.gameObject.CompareTag("Player"))
    {
      collission.gameObject.transform.position = GameManager.instance.respawn;
      Destroy(enemyProjectile);
      GameManager.instance.playGame = false;
      GameManager.instance.lifeLost = true;
    }

    if (collission.gameObject.CompareTag("BottomOfScreen"))
    {
      Destroy(enemyProjectile);
    }
  }
}