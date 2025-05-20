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

    // Configuración para shooters
    public float attackDistance = 10f; // distancia para disparar (no acercarse más)

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
                // (Acá después podrías agregar código para disparar)
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

        Destroy(gameObject, 2f); // ajustá este tiempo si tu animación de morir dura más
    }
}


