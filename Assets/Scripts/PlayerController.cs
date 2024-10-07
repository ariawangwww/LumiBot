using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed;
    public float minSpeed;
    public Rigidbody2D rb;
    public float boundaryRadius;
    public Object player;
    public GameObject PlayerEffect;
    public int LightIntensity;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LightIntensity = 1;
    }
    void FixedUpdate()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 49f;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = (mousePosition - transform.position).normalized;
        float distance = Vector2.Distance(mousePosition, transform.position);

        float speed = Mathf.Lerp(minSpeed, maxSpeed, distance);
        Vector2 newVelocity = direction * speed;

        if (Vector2.Distance(Vector2.zero, transform.position) > boundaryRadius)
        {
            Vector2 spawnPosition;

            // 确保生成位置不在半径为40的圆内
            do
            {
                spawnPosition = Random.insideUnitCircle * 550;
            } while (spawnPosition.magnitude < 40);
            transform.position = spawnPosition;
            newVelocity = Vector2.zero;
        }
        else
        {
            rb.velocity = newVelocity;
        }
        RotateSpriteTowardsMovement();
    }

    void RotateSpriteTowardsMovement()
    {
        bool isflip = false;
        // Check if the NPC is moving (non-zero velocity)
        if (rb.velocity.x != 0)
        {
            // If moving left (negative x direction), flip the sprite
            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Flip horizontally
                isflip = true;
            }
            else
            {
                // If moving right (positive x direction), reset to normal
                transform.localScale = new Vector3(1, 1, 1); // Reset flip
                isflip = false;
            }
        }
        // Check if the NPC is moving (non-zero velocity)
        if (rb.velocity != Vector2.zero)
        {
            // Calculate the angle in degrees from the velocity
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            if (isflip)
            {
                angle -= 180;
            }
            // Rotate the NPC sprite to face the movement direction
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private void Update()
    {
        GameObject _playerEffects = Instantiate(PlayerEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z + 50), Quaternion.identity);
        Destroy(_playerEffects, 1f);
        GetInput();
    }

    public void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (LightIntensity > 1)
            {
                LightIntensity = LightIntensity - 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (LightIntensity < 3)
            {
                LightIntensity = LightIntensity + 1;
            }
        }
    }
}
    
