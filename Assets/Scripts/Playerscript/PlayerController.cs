using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed; // 最大速度
    public float minSpeed;  // 最小速度
    public Rigidbody2D rb;       // 主角的 Rigidbody2D
    public float boundaryRadius;
    public Object player;
    public GameObject PlayerEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            // 限制位置在圆形区域内，保留 z 坐标
            Vector2 boundaryPosition = (transform.position - new Vector3(0, 0, 50)).normalized * boundaryRadius * 1.01f;
            rb.MovePosition(new Vector3(boundaryPosition.x, boundaryPosition.y, transform.position.z)); // 保持 z 坐标
            newVelocity = Vector2.zero; // 停止移动
        }
        else
        {
            rb.velocity = newVelocity; // 正常更新速度
        }
    }

    private void Update()
    {
        GameObject _playerEffects = Instantiate(PlayerEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z + 50), Quaternion.identity);
        Destroy(_playerEffects, 10f);
    }
}
