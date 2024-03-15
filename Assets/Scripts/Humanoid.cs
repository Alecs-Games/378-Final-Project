using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
ANIMATION GUIDE:
Index 1-4: walking up,down,left,right
Index 5-8: standing up,down,left,right
*/
public enum Direction
{
    up,
    down,
    left,
    right
};

public class Humanoid : AnimatedObject
{
    public Sprite portrait;
    Rigidbody2D rb;
    public float moveSpeed;
    public float walkAnimSpeed;
    Vector2 lastDirection = new Vector2(0, 0);
    Direction currDirection = Direction.up;
    public delegate void MoveDelegate(Vector2 movement, float speed);
    public MoveDelegate Move;
    Vector2 vectorDirection;
    public bool isMoving;
    public bool canMove;
    public GameObject meleeAttack;
    AudioSource hitAudio;

    [SerializeField]
    float meleeAttackLength;

    [SerializeField]
    float meleeAttackDistance;

    [SerializeField]
    bool animateToDirection;
    GameObject currentAttack;
    public const float IFRAMESLENGTH = 0.3f;
    bool isOnIFrames = false;

    //stats
    public int health;
    public int maxHealth;

    protected StateMachine stateMachine;
    Vector2 velocity;
    bool dead;
    public bool injured;

    #region initialization

    new protected void Start()
    {
        base.Start();
        canMove = true;
        animIndex = 1;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hitAudio = GetComponent<AudioSource>();
    }
    #endregion
    #region movement
    void OnEnable()
    {
        Move += SetMovement;
    }

    void OnDisable()
    {
        Move -= SetMovement;
    }

    public void SetMovement(Vector2 movement, float speed)
    {
        //set animation direction
        if (canMove)
        {
            if (movement.x > 0)
            {
                currDirection = Direction.right;
            }
            else if (movement.x < 0)
            {
                currDirection = Direction.left;
            }
            if (movement.y > 0)
            {
                currDirection = Direction.up;
            }
            else if (movement.y < 0)
            {
                currDirection = Direction.down;
            }
            if (movement.x > lastDirection.x && movement.x > 0)
            {
                currDirection = Direction.right;
            }
            else if (movement.x < lastDirection.x && movement.x < 0)
            {
                currDirection = Direction.left;
            }
            if (movement.y == 0)
            {
                lastDirection = movement;
            }
            if (movement.x == 0)
            {
                lastDirection = movement;
            }
            movement.Normalize();
            if (speed == 0)
            {
                //velocity = movement * moveSpeed;
                vectorDirection = movement * moveSpeed;
                //vectorDirection = DirectionToVector2(currDirection) * moveSpeed;
            }
            else
            {
                //velocity = movement * speed;
                vectorDirection = movement * speed;
                //vectorDirection = DirectionToVector2(currDirection) * speed;
            }
            if (movement == Vector2.zero)
            {
                if (animateToDirection)
                    SetAnimation(currDirection, false);
            }
            else
            {
                if (animateToDirection)
                    SetAnimation(currDirection, true);
                isMoving = true;
            }
        }
    }

    public virtual void UpdateHealth(int value)
    {
        if (health != -1)
        {
            health += value;
            int maxHealthToCheck;
            if (injured)
                maxHealthToCheck = maxHealth / 2;
            else
                maxHealthToCheck = maxHealth;
            if (health > maxHealthToCheck)
                health = maxHealthToCheck;
            if (health <= 0 && !dead)
            {
                StartCoroutine(Die());
            }
            else if (health > maxHealth)
            {
                health = maxHealth;
            }
        }
    }

    public IEnumerator Die()
    {
        Instantiate(GameManager.instance.poofab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        yield return new WaitForSeconds(0.1f);
    }

    public void SetMovement(Vector2 movement)
    {
        SetMovement(movement, moveSpeed);
    }

    public void MoveToward(Vector2 target, float speed)
    {
        Vector2 d = -((Vector2)transform.position - target);
        d.Normalize();
        d.x = Mathf.RoundToInt(d.x);
        d.y = Mathf.RoundToInt(d.y);
        Move(d, speed);
    }

    public void Stop()
    {
        velocity = Vector2.zero;
        isMoving = false;
        if (animateToDirection)
        {
            SetAnimation(currDirection, false);
        }
    }
    #endregion

    protected void SetAnimation(Direction dir, bool moving)
    {
        if (moving)
        {
            Animate((int)dir, true, walkAnimSpeed, true);
        }
        else
        {
            Animate((int)dir + 4, true, 0f, false);
        }
    }

    public void AttackAnimation(float animSpeed)
    {
        if (currentAttack == null)
        {
            Vector2 d = DirectionToVector2(currDirection);
            currentAttack = Instantiate(meleeAttack, transform);
            currentAttack.GetComponent<Attack>().Initialize(1, 9, currDirection);
            currentAttack.transform.localPosition = new Vector2(
                d.x * meleeAttackDistance,
                d.y * meleeAttackDistance
            );
            Invoke("EndMeleeAttack", meleeAttackLength);
            canMove = false;
            Stop();
        }
    }

    void EndMeleeAttack()
    {
        Destroy(currentAttack);
        currentAttack = null;
        canMove = true;
    }

    protected new void Update()
    {
        base.Update();
        if (!GameManager.instance.paused)
        {
            if (stateMachine != null)
            {
                stateMachine.Update();
            }
            if (!isMoving)
                velocity = Vector2.MoveTowards(velocity, Vector2.zero, 0.5f);
            else
            {
                velocity = Vector2.MoveTowards(velocity, vectorDirection, 0.5f);
            }
            transform.position += ((Vector3)velocity * Time.deltaTime);
        }
        else
        {
            isMoving = false;
        }
    }

    protected void FixedUpdate()
    {
        if (stateMachine != null)
        {
            stateMachine.FixedUpdate();
        }
    }

    public virtual void Hit(int damage, int knockback, GameObject origin)
    {
        if (!isOnIFrames)
        {
            hitAudio.Play();
            Vector2 knockbackVector = (
                (Vector2)transform.position - (Vector2)origin.transform.position
            );
            knockbackVector.Normalize();
            velocity = Vector2.zero;
            ApplyForce(knockbackVector * knockback);
            StartCoroutine(IFrames());
            UpdateHealth(-damage);
        }
    }

    public void ApplyForce(Vector2 force)
    {
        velocity += force;
    }

    Vector2 DirectionToVector2(Direction d)
    {
        switch (d)
        {
            case Direction.down:
                return new Vector2(0, -1);
            case Direction.left:
                return new Vector2(-1, 0);
            case Direction.right:
                return new Vector2(1, 0);
            case Direction.up:
                return new Vector2(0, 1);
        }
        return new Vector2(0, 0);
    }

    IEnumerator IFrames()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        isOnIFrames = true;
        yield return new WaitForSeconds(IFRAMESLENGTH);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        isOnIFrames = false;
    }
}
