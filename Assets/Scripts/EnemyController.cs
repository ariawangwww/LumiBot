using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f; // 移动速度
    public float detectionRange = 10f; // 检测玩家的范围
    public float fleeDistance = 15f; // 逃离的距离
    public float threshold = 5f; // 阈值
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
                Debug.Log("escp");
                if (Vector2.Distance(transform.position, player.position) <= fleeDistance)
                {
                    Vector2 fleeDirection = (transform.position - player.position).normalized;
                    rb.MovePosition(rb.position + fleeDirection * moveSpeed * Time.deltaTime * 3f);
                }
            }
        }
        else
        {
            Debug.Log("calm");
            MoveRandomlyWithPause();
        }
    }

    void MoveRandomlyWithPause()
    {
        // 移动计时器减少
        moveTimer -= Time.deltaTime;
        rb.MovePosition(rb.position + currentDirection * moveSpeed * Time.deltaTime);

        if (moveTimer <= 0)
        {
            // 重新设置随机的移动间隔和方向
            SetRandomMoveTimers();

            // 如果当前是移动阶段，则向随机方向移动

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
        Debug.Log("sucs");
    }
}
