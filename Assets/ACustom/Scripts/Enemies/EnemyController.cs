using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamage, IEnemy
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
    bool canWalk, canAttack;
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
        canAttack = true;
        canWalk = true;
    }

    void Update()
    {
        playerDistance = Vector3.Distance(target.position, transform.position);

        if (playerDistance <= lookRadius && canWalk) Move();
        else Patrol();
        if (playerDistance <= attackRange && canAttack) Attack();
    }

    void Patrol()
    {
        anim.SetBool("isWalking", true);
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet && canWalk)
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

        //if (Physics.Raycast(walkPoint, -transform.up, 2f, isOnGround))
        if ((agent.CalculatePath(walkPoint, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete) && canWalk)
        {
            agent.SetPath(navMeshPath);
            walkPointSet = true;
        }
            
    }

    public void Move()
    {
        anim.SetBool("isWalking", true);
        if(canWalk)
            agent.SetDestination(target.position);
        else
            agent.SetDestination(transform.position);

        if (playerDistance <= agent.stoppingDistance)
        {
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
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Attack()
    {
        anim.SetBool("isAttacking", true);
        LayerMask player = LayerMask.GetMask("Player");
        agent.SetDestination(transform.position);
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

    public void Die()
    {
        canAttack = false;
        canWalk = false;
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
