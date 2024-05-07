using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class aiMovement : MonoBehaviour
{
    public Transform player;
    public float chaseDistance = 10f; // Adjust this value as needed
    public float wanderRadius = 5f; // Adjust this value as needed
    public float wanderTimer = 5f; // Adjust this value as needed

    private NavMeshAgent agent;
    private float timer;
    private Vector3 randomDestination;
    public Transform safezone;

    //reference to the enemy's animator component
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        GetNewRandomDestination();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance between enemy and player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        //check if the player is inside the safezone
        //calculate the distance between player and safezone
        float distanceToSafezone = Vector3.Distance(player.position, safezone.position);

        //check if the player is within the safezone
        bool playerInsideSafezone = distanceToSafezone <= safezone.localScale.x / 2;
        if (playerInsideSafezone)
        {
            Debug.Log("Player is insdie the safezone.");
        }
        else
        {
            Debug.Log("Player isn't inside the safezone.");
        }

        // If the player is outside the chase distance, move randomly
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            GetNewRandomDestination();
            animator.SetBool("isWalking", true);
            timer = wanderTimer;

        }

        agent.destination = randomDestination;

        //player in safezone && distanceToPlayer <= chaseDistance, enemy moves randomly
        if (playerInsideSafezone && distanceToPlayer <= chaseDistance)
        {
            animator.SetBool("isWalking", true);
            agent.destination = randomDestination;
        }

        // If the player is within the chase distance, chase the player
        else if (distanceToPlayer <= chaseDistance)
        {
            agent.destination = player.position;
            animator.SetBool("isWalking", true);
            return;
        }
    }


    void GetNewRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);
        randomDestination = hit.position;
    }
}