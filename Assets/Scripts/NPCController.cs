using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float moveSpeed; // �ƶ��ٶ�
    public float detectionRange; // �����ҵķ�Χ
    public float fleeDistance; // ����ľ���
    public float threshold; // ��ֵ
    public int intensity; // NPC��ǿ��
    public float boundaryRadius;
    public GameObject NPCEffect;

    private Rigidbody2D rb;
    private Transform player; // ��Ҷ���

    private Vector2 currentDirection; // ��ǰ���ƶ�����
    private float moveDuration; // �ƶ��ĳ���ʱ��
    private float moveCooldown; // �ƶ����ʱ��
    private float moveTimer; // ������ʱ

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform; // ���������"Player"��ǩ
    }

    void FixedUpdate()
    {
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
        Debug.Log(rb.velocity.x);
        bool isflip = false;
        // Check if the NPC is moving (non-zero velocity)
        if (rb.velocity.x != 0)
        {
            // If moving left (negative x direction), flip the sprite
            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-7, 7, 7); // Flip horizontally
                isflip = true;
            }
            else
            {
                // If moving right (positive x direction), reset to normal
                transform.localScale = new Vector3(7, 7, 7); // Reset flip
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
        // ����ҵľ����ж�
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            // �ж�
            if (intensity <= threshold)
            {
                SuccessTest(); // ���óɹ����Ժ���
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
        // �ƶ���ʱ������
        moveTimer -= Time.deltaTime;

        // NPC����ȴ�ڼ䱣�־�ֹ
        if (moveTimer > 0)
        {
            Vector2 newVelocity = currentDirection * moveSpeed * Random.Range(0.5f, 1.5f);
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
        else
        {
            // ֹͣ�ƶ�
            SetRandomMoveTimers();
        }
    }

    void SetRandomMoveTimers()
    {
        // ��������ƶ��ķ���
        currentDirection = Random.insideUnitCircle.normalized;

        // ��������ƶ�����ʱ��ͼ��ʱ��
        moveDuration = Random.Range(1f, 3f); // NPCÿ���ƶ�����1��3��
        moveCooldown = Random.Range(2f, 5f); // NPC�ƶ�����ͣ2��5��

        // ���ü�ʱ������ʼ�µ��ƶ���ȴ��׶�
        moveTimer = moveDuration + moveCooldown;
    }

    void SuccessTest()
    {
    }
}
