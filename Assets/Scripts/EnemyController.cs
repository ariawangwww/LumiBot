using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f; // �ƶ��ٶ�
    public float detectionRange = 10f; // �����ҵķ�Χ
    public float fleeDistance = 15f; // ����ľ���
    public float threshold = 5f; // ��ֵ
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
        // �ƶ���ʱ������
        moveTimer -= Time.deltaTime;
        rb.MovePosition(rb.position + currentDirection * moveSpeed * Time.deltaTime);

        if (moveTimer <= 0)
        {
            // ��������������ƶ�����ͷ���
            SetRandomMoveTimers();

            // �����ǰ���ƶ��׶Σ�������������ƶ�

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
