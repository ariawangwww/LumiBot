using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float boundaryRadius;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(Vector2.zero, transform.position) > boundaryRadius)
        {
            Vector2 boundaryPosition = (transform.position - new Vector3(0, 0, 50)).normalized * boundaryRadius * 1.01f;
            rb.MovePosition(new Vector3(boundaryPosition.x, boundaryPosition.y, transform.position.z));
        }
    }
}
