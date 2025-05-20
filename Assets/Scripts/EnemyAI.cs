using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    private Animator animator;

    public int nivelActual = 1;
    public float baseSpeed = 3f;
    public float speedPerNivel = 1f;

    private bool isDead = false;

    // Configuraci�n para shooters
    public float attackDistance = 10f; // distancia para disparar (no acercarse m�s)

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
        }

        ActualizarVelocidad();
    }

    void Update()
    {
        if (isDead) return;
        if (target == null) return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (nivelActual <= 2)
        {
            // Melee enemigos: siempre persiguen
            agent.SetDestination(target.position);
        }
        else
        {
            // Shooter enemigos
            if (distanceToTarget > attackDistance)
            {
                agent.SetDestination(target.position);
            }
            else
            {
                agent.SetDestination(transform.position); // Se detienen para disparar
                // (Ac� despu�s podr�as agregar c�digo para disparar)
            }
        }

        bool isMoving = agent.velocity.magnitude > 0.1f;
        animator.SetBool("isRunning", isMoving);
    }

    public void ActualizarVelocidad()
    {
        agent.speed = baseSpeed + speedPerNivel * (nivelActual);
    }

    public void Morir()
    {
        if (isDead) return;

        isDead = true;
        agent.isStopped = true;
        animator.SetBool("isRunning", false);
        animator.SetBool("isDying", true);

        Destroy(gameObject, 2f); // ajust� este tiempo si tu animaci�n de morir dura m�s
    }
}


