using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamage
{
    [SerializeField]
    float health = 100f;
    [SerializeField]
    float damage = 10f;
    [SerializeField]
    float lookRadius = 10f;
    [SerializeField]
    float attackRange = 5;
    [SerializeField]
    Animator anim;
    bool isDead;
   // bool canWalk, canAttack;
    public LayerMask isOnGround;
    float playerDistance;
    Transform target;
    NavMeshAgent agent;

    //Patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    void Start()
    {
        target = FindPlayer.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("isDead", false);
        isDead = false;
        //canAttack = true;
       // canWalk = true;
    }

    void Update()
    {
        playerDistance = Vector3.Distance(target.position, transform.position);
        if (!isDead)
        {
            if (playerDistance <= lookRadius)
            {
                AudioManager.PlaySound(AudioManager.Sound.EnemyBreath, GetPosition());
                Move();
            }
            else Patrol();
            if (playerDistance <= attackRange)
            {
                anim.SetBool("isAttacking", true);
                agent.SetDestination(transform.position);
            }
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    void Patrol()
    {
        anim.SetBool("isWalking", true);
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 3f)
        {
            walkPointSet = false;
        }     
    }

    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
    
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //verify if path is valid
        NavMeshPath navMeshPath = new NavMeshPath();
        if (agent.CalculatePath(walkPoint, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            agent.SetPath(navMeshPath);
            walkPointSet = true;
        } 
    }

    void Move()
    {
    //    AudioManager audio = FindObjectOfType<AudioManager>();
        if (isDead)
            agent.SetDestination(transform.position);

        anim.SetBool("isWalking", true);
        agent.SetDestination(target.position);

        if (playerDistance <= agent.stoppingDistance)
        {
        //    audio.Play("EnemyBreath");
            FaceTarget();
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void TakeDamage(float amount)
    {
        AudioManager.PlaySound(AudioManager.Sound.EnemyPain, GetPosition());
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Attack()
    {
        LayerMask player = LayerMask.GetMask("Player");
        FaceTarget();
        if (!alreadyAttacked)
        {
            Vector3 attackDirection = (target.position - transform.position).normalized;
            if (Physics.Raycast(transform.position, attackDirection, out RaycastHit hit, attackRange, player)){
                IDamage dest = hit.transform.GetComponent<IDamage>();
                if (dest != null)
                {
                    dest.TakeDamage(damage);
                }
            }
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
        anim.SetBool("isAttacking", false);
    }

    void Die()
    {
        AudioManager.PlaySound(AudioManager.Sound.EnemyDie, GetPosition());
        GetComponent<Collider>().enabled = false;
        isDead = true;
        anim.SetBool("isDead", true);
        Invoke(nameof(DelayDeath), 4f);
    }

    void DelayDeath()
    {
        Destroy(gameObject);
    }

    //animations
    //refactor to module in future
}
