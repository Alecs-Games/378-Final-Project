using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Humanoid
{
    bool moving;
    public bool takingInput = true;
    public bool canAttack;
    public float forestEncounterRate;
    public Vector2 forestEncounterReturnPos;
    ParticleSystem grassParticles;
    ParticleSystem sprintParticles;
    public string[] randomEncounterScenes;

    public void EnterMapMode()
    {
        //moveSpeed = 3f;
        //canAttack = false;
    }

    public void EnterRegularMode()
    {
        //moveSpeed = 3f;
        //canAttack = true;
    }

    public void Heal(int value) { }

    // Use this for initialization
    new void Start()
    {
        base.Start();
        stateMachine = new StateMachine();
        stateMachine.Initialize(new State(), (Humanoid)this);
        //grassParticles = GetComponent<ParticleSystem>();


        Transform sprintParticleTransform = transform.Find("Sprint Trail");
        sprintParticles = sprintParticleTransform.GetComponent<ParticleSystem>();

         Transform grassParticleTransform = transform.Find("Tree Trail");
        grassParticles = grassParticleTransform.GetComponent<ParticleSystem>();
        
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        float x;
        float y;
        if (!GameManager.instance.paused)
        {
            if (takingInput)
            {
                x = (float)Input.GetAxisRaw("Horizontal");
                y = (float)Input.GetAxisRaw("Vertical");
            }
            else
            {
                x = 0f;
                y = 0f;
            }
            if (x != 0 || y != 0)
            {
                Move(new Vector2(x, y), 0);
                moving = true;
            }
            else if (moving)
            {
                Stop();
                moving = false;
            }
            if (canAttack && takingInput && Input.GetKeyDown(KeyCode.Z))
            {
                StartSwing();
            }


            // Sprinting Code
            if (Input.GetKey(KeyCode.LeftShift) && !sprintParticles.isPlaying)
            {
                sprintParticles.Play();

                moveSpeed = 4;
                walkAnimSpeed = 1;
            }
            else if (!Input.GetKey(KeyCode.LeftShift) && sprintParticles.isPlaying)
            {
                sprintParticles.Stop(false, ParticleSystemStopBehavior.StopEmitting);

                moveSpeed = 3;
                walkAnimSpeed = 0.5f;
            }


            
            
        }
    }

    public void SetState(State newState)
    {
        stateMachine.SetState(newState);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ReturnToMap"))
        {
            GameManager.instance.LoadScene("map", true, forestEncounterReturnPos);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Forest") && grassParticles.isPlaying)
        {
            grassParticles.Stop(false, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    
    private void OnTriggerStay2D(Collider2D other)
    {

        
        if (other.gameObject.CompareTag("Forest"))
        {
            if (grassParticles.isPlaying)
            {
                if (!isMoving)
                {
                    grassParticles.Stop(false, ParticleSystemStopBehavior.StopEmitting);
                }
            }
            else
            {
                if (isMoving)
                {
                    grassParticles.Play();
                }
            }
            if (isMoving)
            {
                if (Random.Range(0, 1000) < forestEncounterRate)
                {
                    forestEncounterReturnPos = transform.position;
                    GameManager.instance.LoadScene(
                        randomEncounterScenes[Random.Range(0, randomEncounterScenes.Length - 1)],
                        false,
                        new Vector2(0f, 0f)
                    );
                }
            }
        }
    }

    void OnDestroy()
    {
        GameManager.instance.OnPlayerDeath();
    }

    public override void UpdateHealth(int value)
    {
        base.UpdateHealth(value);
        GameManager.instance.hearts.UpdateHearts();
    }

    void StartSwing()
    {
        // Attacks only permitted if not sprinting
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            AttackAnimation(0.05f);
        }
    }

    GameObject GetClosest(List<GameObject> targets)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in targets)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
}
