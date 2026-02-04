using System;
using UnityEngine;
using UnityEngine.AI;

/*
Escena main: marmonanerf2 (enemigo comun) y marmotacetro3 (enemigo jefe)
Los objetos con este script necesitan un nav mesh agent para moverse y un nav mesh surface sobre el que moverse
Se establece un objeto que al entrar este en rango provoca a los enemigos y hace que empiece a seguir al transform asignado como target
Si se esta en rango de ataque, se ataca a este cada cierto tiempo (attack cooldown)
*/

public class EnemyAIMovement : MonoBehaviour
{
    //A que transform seguir (player)
    [SerializeField] Transform Target;

    //Toma el navmeshagent
    NavMeshAgent navMeshAgent;
    //Toma la vida del jugador
    BarraDeVida targetHealth;
    //Toma la vida de la marmota (enemigo)
    VidaEnemigo enemyHealth;

    //Rango de deteccion de la marmota
    [SerializeField] float Range = 10f;
    float distanceToTarget = Mathf.Infinity;

    //Estadisticas de daño y velocidad
    [SerializeField] public float Damage = 10f;
    [SerializeField] float turningSpeed = 5f;
    [SerializeField] public float attackCooldown = 1.5f;
    float lastAttackTime = -Mathf.Infinity;

    //bool para saber si ataca al ser provocado el enemigo
    public bool isProvoked = false;

    //bool para verificar si el enemigo esta vivo
    bool dead;

    //Animator por si tiene animacion
    Animator animator;

    //Toma los valores del enemigo y jugador
    void Start()
    {

        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        targetHealth = Target.GetComponent<BarraDeVida>();
        enemyHealth = GetComponent<VidaEnemigo>();
    }

    void Update()
    {
        //Verifica si el enemigo esta vivo o no
        dead = enemyHealth.IsDead();
        if (dead)
        {
            Die();
            return;
        }

        //Calcula la distancia si el enemigo no necesita ser provocado
        if (!isProvoked)
        {
            distanceToTarget = Vector3.Distance(Target.position, transform.position);
        }

        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= Range)
        {
            //Si el player entra en el rango se empieza a mover hacia el jugador
            isProvoked = true;
        }
    }

    //Empieza a moverse en direccion al jugador al ser provocado el enemigo
    private void EngageTarget()
    {
        //Mira a la direccion del jugador
        LookTarget();

        //Compara la distancia con el jugador y donde se tiene que detener
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            //Si el enemigo no alcanzo la distancia para detenerse, seguir moviendose hacia el jugador
            if (navMeshAgent.isOnNavMesh && navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                navMeshAgent.SetDestination(Target.position);
                //animator.SetBool("isMoving", true);
            }
        }
        else
        {
            //Si el enemigo esta a rango de ataque del jugador, atacarlo
            AttackTarget();
        }
    }

    //Mira en direccion al jugador
    private void LookTarget()
    {
        //Calcula la posicion entre el jugador y el enemigo
        Vector3 direction = (Target.position - transform.position).normalized;

        //Calcula la nueva rotacion que debe tener para ver al jugador
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        //Asigna la rotacion al enemigo
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turningSpeed);
    }

    //Ataca al jugador mientras siga en el rango de ataque
    private void AttackTarget()
    {
        animator.SetBool("isMoving", false);
        if (Time.time > lastAttackTime + attackCooldown)
        {
            //Animacion de ataque si hubiera
            animator.SetTrigger("attack");
            //Reduce la vida del player por el daño asignado a la marmota
            targetHealth.ReducirVida(Damage);
            lastAttackTime = Time.time;
        }
    }

    //Llama a una animacion de morir si la hubiera y detiene todas las acciones del gameobject
    private void Die()
    {
        navMeshAgent.enabled = false;
        animator.SetTrigger("die");
        this.enabled = false;
    }

    //Visualizacion del rango en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
