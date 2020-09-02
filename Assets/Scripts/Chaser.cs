using System;
using UnityEngine;
using UnityEngine.AI;

public class Chaser : MonoBehaviour
{
    [SerializeField]
    Transform target;

    NavMeshAgent agent;
    Animator animator;

    Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
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
                enemy.PlaySound();
            }
            else
            {
                animator.SetBool(Constants.IS_WALKING, false);
                enemy.StopSound();
            }
        }
        else {
            agent.SetDestination(transform.position);
            enemy.StopSound();
        }
        if (agent.velocity.magnitude <= 0.1f)
        {
            animator.SetBool(Constants.IS_WALKING, false);
            enemy.StopSound();
        }

    }

    private bool ShouldChase()
    {
        if (GameManager.Instance.IsGameRunning())
        {
            float distance = Vector3.Distance(transform.position, target.position);
            return distance < 13.0f;
        }
        else
        {
            return false;
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
