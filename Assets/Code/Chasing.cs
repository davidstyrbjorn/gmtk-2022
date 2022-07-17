using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chasing : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;

    private const float BASE_SPEED = 2.5f;
    private Scale scale;

    private SpriteRenderer spre;

    void Start()
    {
        spre = GetComponent<SpriteRenderer>();
        scale = FindObjectOfType<Scale>();
        agent = GetComponent<NavMeshAgent>();
        // The system rotates the GameObject, this is not what we want to disable rotational attributes on the agent
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = 2.5f;

        target = FindObjectOfType<PlayerMovement>().transform; // Assume we chase player on spawn
    }

    void FixedUpdate()
    {
        // Sprite flippage
        spre.flipX = (target.position.x < transform.position.x);

        agent.speed = 5f * scale.GetEnemyMovementMultiplier();
        // Set target for agent so it knows where to move
        agent.SetDestination(target.transform.position);

        // Are we close to target?
        const float EPS = 0.35f;
        if (Vector2.Distance(target.position, transform.position) < EPS)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
    }

}
