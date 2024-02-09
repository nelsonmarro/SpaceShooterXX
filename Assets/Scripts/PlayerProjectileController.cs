using UnityEngine;

public class PlayerProjectileController : MonoBehaviour
{
  public GameObject playerProjectile;
  public float projectileSpeed = -5;

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
    if (collission.gameObject.CompareTag("Enemy"))
    {
      collission.gameObject.SetActive(false);
      Destroy(playerProjectile);
      GameManager.instance.playGame = true;
      GameManager.instance.enemyCount--;
      GameManager.instance.score += 25;
    }

    if (collission.gameObject.CompareTag("TopOfScreen"))
    {
      Destroy(playerProjectile);
    }
  }
}