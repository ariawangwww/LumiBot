using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float moveSpeed; // 移动速度
    public float detectionRange; // 检测玩家的范围
    public float fleeDistance; // 逃离的距离
    public float threshold; // 阈值
    public int intensity; // NPC的强度

    private Rigidbody2D rb;
    private Transform player; // 玩家对象

    private Vector2 currentDirection; // 当前的移动方向
    private float moveDuration; // 移动的持续时间
    private float moveCooldown; // 移动间隔时间
    private float moveTimer; // 用来计时

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform; // 假设玩家有"Player"标签
    }

    void FixedUpdate()
    {
        MainBehavior();
        RotateSpriteTowardsMovement();
    }

    void RotateSpriteTowardsMovement()
    {
        bool isflip = false;
        Debug.Log(rb.velocity.x);
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

    void MainBehavior()
    {
        // 与玩家的距离判断
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            // 判定
            if (intensity <= threshold)
            {
                SuccessTest(); // 调用成功测试函数
            }
            else
            {
                if (distanceToPlayer <= fleeDistance)
                {
                    Vector2 fleeDirection = (transform.position - player.position).normalized;
                    rb.velocity = fleeDirection * moveSpeed * Random.Range(2f, 3f);
                }
                else
                {
                    rb.velocity = Vector2.zero; // Stop moving when not fleeing
                }
            }
        }
        else
        {
            MoveRandomlyWithPause();
        }
    }

    void MoveRandomlyWithPause()
    {
        // 移动计时器减少
        moveTimer -= Time.deltaTime;

        // NPC在冷却期间保持静止
        if (moveTimer > 0)
        {
            rb.velocity = currentDirection * moveSpeed * Random.Range(0.5f, 1.5f);
        }
        else
        {
            // 停止移动
            SetRandomMoveTimers();
        }
    }

    void SetRandomMoveTimers()
    {
        // 随机设置移动的方向
        currentDirection = Random.insideUnitCircle.normalized;

        // 随机设置移动持续时间和间隔时间
        moveDuration = Random.Range(1f, 3f); // NPC每次移动持续1到3秒
        moveCooldown = Random.Range(2f, 5f); // NPC移动后暂停2到5秒

        // 重置计时器，开始新的移动或等待阶段
        moveTimer = moveDuration + moveCooldown;
    }

    void SuccessTest()
    {
    }
}
