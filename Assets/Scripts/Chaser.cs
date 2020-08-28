using System;
using UnityEngine;
using UnityEngine.AI;

public class Chaser : MonoBehaviour
{
    [SerializeField]
    Transform target;

    NavMeshAgent agent;
    Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (ShouldChase())
        {
            Vector3 destination = target.position;
            agent.SetDestination(destination);
            if (agent.remainingDistance >= 0.5f)
            {
                animator.SetBool(Constants.IS_WALKING, true);
            }
            else
            {
                animator.SetBool(Constants.IS_WALKING, false);
            }
        }
        if (agent.velocity.magnitude <= 0.1f)
        {
            animator.SetBool(Constants.IS_WALKING, false);
        }
    }

    private bool ShouldChase()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return distance < 10.0f;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
