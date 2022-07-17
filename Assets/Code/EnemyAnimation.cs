using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!animator.GetBool("isDying") && TryGetComponent(out Enemy e))
            animator.SetBool("isMoving", e.isActiveAndEnabled);
        if (TryGetComponent(out NavMeshAgent agent))
            animator.speed = agent.speed / 10f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            animator.speed = 2f;
            StartCoroutine(Attack());
        }
    }
    public void StartDie()
    {
        StartCoroutine(Die());
    }
    IEnumerator Die()
    {
        animator.SetBool("isDying", true);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    IEnumerator Attack()
    {
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("isAttacking", false);
    }
}
