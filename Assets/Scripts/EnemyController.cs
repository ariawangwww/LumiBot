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
    public float boundaryRadius;
    public GameObject EnemyEffect;

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

    private void Update()
    {
        RotateSpriteTowardsMovement();
        GameObject _playerEffects = Instantiate(EnemyEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
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
        // Distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            if (distanceToPlayer <= chaseDistance)
            {
                ChasePlayer();
            }
        }
        else
        {
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
            Vector2 newVelocity = chaseDirection * moveSpeed * Random.Range(2f, 3f);
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
        sprintTimer -= Time.deltaTime;

        if (sprintTimer > 0)
        {
            // Perform a high-speed dash towards the player
            Vector2 newVelocity = direction * sprintSpeed* Random.Range(4f, 6f);
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
}
