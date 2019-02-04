using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public float lookRadius = 10f;
    public EnemyStats enemyStats;
    public bool hasAttacked = false;

    Transform target;
    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

        //Create head UI for name and other stats
        //CreateHoverUI.instance.CreateHeadUI(gameObject.GetComponent<EnemyStats>().name, CameraManager.instance.GetActiveCamera().WorldToViewportPoint(transform.position));
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius || hasAttacked) //if player is in look radius or enemy has been attacked than trigger atack to player
        {
            Invoke("UnTrigger", 10f); //call untriger after 10 seconds of chasing
            PlayerManager.instance.player.GetComponent<PlayerStats>().GetAttacked(); //calling get attacked function
            agent.SetDestination(target.position);
            
            if(distance <= agent.stoppingDistance)
            {
                //Attack to target
                if (!PlayerManager.instance.player.GetComponent<PlayerStats>().deathState && !IsInvoking("AttackTarget")) //attack if attack counter is at yield and player has not died
                    AttackTarget();

                //Face to target
                FaceTarget();
            }
        }
	}

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void AttackTarget()
    {
        if(Vector3.Distance(target.position, transform.position) <= agent.stoppingDistance)
        {
            int actionDiceVelue = Random.Range(1, 20); //action dice to giving a random success to attack
            int attackDiceValue = Random.Range(1, enemyStats.damage.GetValue()); //random damage. System is like frp
            int attackValue;
            if (actionDiceVelue > 15)
                attackValue = attackDiceValue;
            else
                attackValue = (attackDiceValue * actionDiceVelue * 5) / 100; //calculating final attack value accorging to dice values
            if(target.name == "Player")
                PlayerManager.instance.player.GetComponent<PlayerStats>().TakeDamage(attackValue);

            if(!PlayerManager.instance.player.GetComponent<PlayerStats>().deathState)
                Invoke("AttackTarget", enemyStats.attackCooldown.GetValue());
        }
    }

    private void OnDrawGizmosSelected() //drawing look sphere in editor
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void UnTrigger() // ending attack state of both player and enemy
    {
        hasAttacked = false;
        PlayerManager.instance.player.GetComponent<PlayerStats>().AttackFinished();
    }
}
