using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float moveSpeed; // 移动速度
    public float detectionRange; // 检测玩家的范围
    public float fleeDistance; // 逃离的距离
    public float threshold; // 阈值
    public int intensity; // NPC的强度
    public float boundaryRadius;
    public GameObject NPCEffect;
    public bool success;

    private Rigidbody2D rb;
    private Transform player; // 玩家对象
    private bool notifyunsuccess = false;

    private Vector2 currentDirection; // 当前的移动方向
    private float moveDuration; // 移动的持续时间
    private float moveCooldown; // 移动间隔时间
    private float moveTimer; // 用来计时
    PlayerController playercontroller;
    SpriteController spritecontroller;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform; // 假设玩家有"Player"标签
        GameObject playerobj = GameObject.FindWithTag("Player");
        playercontroller = playerobj.GetComponent<PlayerController>();
        GameObject spriteobj = GameObject.FindWithTag("Sprite");
        spritecontroller = spriteobj.GetComponent<SpriteController>();
    }

    void FixedUpdate()
    {
        intensity = playercontroller.LightIntensity;
        MainBehavior();
    }

    private void Update()
    {
        RotateSpriteTowardsMovement();
        GameObject _playerEffects = Instantiate(NPCEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(_playerEffects, 1f);
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
                transform.localScale = new Vector3(-6, 6, 6); // Flip horizontally
                isflip = true;
            }
            else
            {
                // If moving right (positive x direction), reset to normal
                transform.localScale = new Vector3(6, 6, 6); // Reset flip
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
                success = false;
                if (distanceToPlayer <= fleeDistance)
                {
                    Vector2 fleeDirection = (transform.position - player.position).normalized;
                    Vector2 newVelocity = fleeDirection * moveSpeed * Random.Range(2f, 3f);
                    if (Vector2.Distance(Vector2.zero, transform.position) > boundaryRadius)
                    {
                        Vector2 spawnPosition;

                        // 确保生成位置不在半径为40的圆内
                        do
                        {
                            spawnPosition = Random.insideUnitCircle * 550;
                        } while (spawnPosition.magnitude < 40 || Vector2.Distance(spawnPosition, player.position) < 150);
                        transform.position = spawnPosition;
                        newVelocity = Vector2.zero;
                    }
                    else
                    {
                        rb.velocity = newVelocity;
                    }
                }
            }
        }
        else
        {
            success = false;
            MoveRandomlyWithPause();
        }
        if(!success && notifyunsuccess)
        {
            spritecontroller.SetSuccess(false);
            notifyunsuccess = false;
        }
    }

    void MoveRandomlyWithPause()
    {
        // 移动计时器减少
        moveTimer -= Time.deltaTime;

        // NPC在冷却期间保持静止
        if (moveTimer > 0)
        {
            Vector2 newVelocity = currentDirection * moveSpeed * Random.Range(0.5f, 1.5f);
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
        if (!spritecontroller.success) {
            notifyunsuccess = true;
        }
        spritecontroller.SetSuccess(true);
        success = true;
        Vector2 chaseDirection = (player.position - transform.position).normalized;
        Vector2 newVelocity = chaseDirection * moveSpeed * Random.Range(0.2f, 0.4f);
        if (Vector2.Distance(Vector2.zero, transform.position) > boundaryRadius)
        {
            Vector2 spawnPosition;

            // 确保生成位置不在半径为40的圆内
            do
            {
                spawnPosition = Random.insideUnitCircle * 550;
            } while (spawnPosition.magnitude < 40 || Vector2.Distance(spawnPosition, player.position) < 150);
            transform.position = spawnPosition;
            newVelocity = Vector2.zero;
        }
        else
        {
            rb.velocity = newVelocity;
        }
    }
}
