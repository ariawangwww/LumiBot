using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float moveSpeed; // �ƶ��ٶ�
    public float detectionRange; // �����ҵķ�Χ
    public float fleeDistance; // ����ľ���
    public float threshold; // ��ֵ
    public int intensity; // NPC��ǿ��

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
                Debug.Log("escp");
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
            Debug.Log("calm");
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
            rb.velocity = currentDirection * moveSpeed * Random.Range(0.5f, 1.5f);
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
        Debug.Log("sucs");
    }
}
