using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public GameObject player;
  public GameObject playerProjectile;
  public GameObject playerProjectileClone;
  public float playerSpeed;
  public float maxX;

  private float _movementHorizontal;

  // Start is called before the first frame update
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    // Move the player left or right within the screen
    _movementHorizontal = Input.GetAxis("Horizontal");
    if ((_movementHorizontal > 0 && transform.position.x < maxX) ||
        (_movementHorizontal < 0 && transform.position.x > -maxX))
    {
      transform.position += Vector3.right *
                            (_movementHorizontal * playerSpeed *
                             Time.deltaTime);
    }

    // Fire a projectile when the space bar is pressed
    if (Input.GetKeyDown(KeyCode.Space) && playerProjectileClone == null)
    {
      var position = player.transform.position;
      playerProjectileClone = Instantiate(playerProjectile, new Vector3
          (position.x, position.y + 0.6f), 
        player.transform.rotation);
    }
  }
}