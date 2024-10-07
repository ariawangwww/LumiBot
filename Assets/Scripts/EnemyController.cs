using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed; // Normal move speed
    public float sprintSpeed; // Speed during a sprint
    public float detectionRange; // Detection range for the player
    public float chaseDistance; // Distance at which the enemy will chase the player
    public float sprintCooldownMin; // Minimum cooldown before the next sprint
    public float sprintCooldownMax; // Maximum cooldown before the next sprint
    public float sprintDuration; // How long the sprint lasts

    private Rigidbody2D rb;
    private Transform player; // Player object

    private Vector2 currentDirection; // Current movement direction
    private float moveDuration; // Movement duration
    private float moveCooldown; // Time between movements
    private float moveTimer; // Movement timer

    private bool isSprinting = false; // Is the enemy currently sprinting?
    private bool setDirection = false;
    private float sprintTimer; // Timer to control the sprint duration
    private float sprintCooldownTimer; // Timer to control when the next sprint can occur
    Vector2 sprintDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform; // Assumes the player has a "Player" tag
        ResetSprintCooldown(); // Initialize sprint cooldown
    }

    void FixedUpdate()
    {
        MainBehavior();
    }

    void MainBehavior()
    {
        // Distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            Debug.Log("enemy-chase");
            if (distanceToPlayer <= chaseDistance)
            {
                ChasePlayer();
            }
        }
        else
        {
            Debug.Log("enemy-calm");
            MoveRandomlyWithPause();
        }
    }

    void ChasePlayer()
    {
        // Check if the enemy is sprinting
        if (isSprinting)
        {
            if(!setDirection)
            {
                sprintDirection = (player.position - transform.position).normalized;
                setDirection = true;
            }
            Sprint(sprintDirection); // Perform the sprint
        }
        else
        {
            // Normal chase behavior
            Vector2 chaseDirection = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + chaseDirection * moveSpeed * Time.deltaTime * Random.Range(2f, 3f));

            // Countdown to the next sprint
            sprintCooldownTimer -= Time.deltaTime;
            if (sprintCooldownTimer <= 0)
            {
                // Start sprint
                isSprinting = true;
                sprintTimer = sprintDuration;
                ResetSprintCooldown();
            }
        }
    }

    void Sprint(Vector2 direction)
    {
        Debug.Log("enemy-sprint");
        sprintTimer -= Time.deltaTime;

        if (sprintTimer > 0)
        {
            // Perform a high-speed dash towards the player
            rb.velocity = direction * sprintSpeed* Random.Range(4f, 6f);
        }
        else
        {
            // Stop sprinting once the duration is over
            isSprinting = false;
            setDirection = false;
        }
    }

    void MoveRandomlyWithPause()
    {
        moveTimer -= Time.deltaTime;
        rb.velocity = rb.position + currentDirection * moveSpeed * Random.Range(0.5f, 1.5f);

        if (moveTimer <= 0)
        {
            SetRandomMoveTimers();
        }
    }

    void SetRandomMoveTimers()
    {
        currentDirection = Random.insideUnitCircle.normalized;
        moveDuration = Random.Range(1f, 3f); // NPC moves for 1 to 3 seconds
        moveCooldown = Random.Range(2f, 5f); // NPC pauses for 2 to 5 seconds
        moveTimer = moveDuration + moveCooldown;
    }

    void ResetSprintCooldown()
    {
        // Reset the sprint cooldown to a random value between the min and max
        sprintCooldownTimer = Random.Range(sprintCooldownMin, sprintCooldownMax);
    }

    void SuccessTest()
    {
        Debug.Log("success");
    }
}
