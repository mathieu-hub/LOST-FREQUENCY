using UnityEngine;

public class AlienAI : MonoBehaviour
{
    public GameObject playerEntity;    
    public Animator alienAnim;
    public float distance;

    //Patroling
    public float moveSpeed;
    [SerializeField] private Transform target;
    [SerializeField] private int wayPointIndex; 

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States 
    [Range(0,10)] public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Start()
    {
        target = WayPath.points[0];
    }

    private void Update()
    {
        distance = Vector3.Distance(playerEntity.transform.position, transform.position);

        if (distance < sightRange)
        {
            playerInSightRange = true;
        }
        else
        {
            playerInSightRange = false;
        }

        if (distance < attackRange)
        {
            playerInAttackRange = true;
        }
        else
        {
            playerInAttackRange = false;
        }

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer(); // À Remplacer Par ChasePlayer()
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    void Patroling()
    {
        moveSpeed = 4;

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);
        transform.LookAt(target);

        if (Vector3.Distance(transform.position, target.position) <= 0.4)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if (wayPointIndex >= WayPath.points.Length - 1)
        {
            wayPointIndex = 0;
        }
        else 
        {
            wayPointIndex++;
        }
        
        target = Waypoints.points[wayPointIndex];
    }

    void ChasePlayer()
    {
        moveSpeed = 9;

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);
        transform.LookAt(target);

        if (Vector3.Distance(transform.position, target.position) <= 0.4)
        {
            GetNextWaypoint();
        }
    }

    void AttackPlayer()
    {
        transform.LookAt(playerEntity.transform);

        if (!alreadyAttacked)
        {
            ///Attack Code Here
            Debug.Log("Alien Attack");
            //Animation;
            playerEntity.GetComponent<HealthManager>().Health -= 40f; 
            ///

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
