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
            Vector2 boundaryPosition = (transform.position - new Vector3(0, 0, 50)).normalized * boundaryRadius * 1.01f;
            rb.MovePosition(new Vector3(boundaryPosition.x, boundaryPosition.y, transform.position.z));
            newVelocity = Vector2.zero;
        }
        else
        {
            rb.velocity = newVelocity;
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
