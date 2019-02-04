using UnityEngine;
using System.Collections;

public class Enemy : Interactable {

    public EnemyStats enemyStats;

    public override void Interaction()
    {
        base.Interaction();

        if (!IsInvoking("TakeDamage")) //if auto attack has not been started
        {
            //Attack to enemy
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        if(!enemyStats.deathState)
        {
            int actionDiceVelue = Random.Range(1, 20); //action dice to giving a random success to attack
            int attackDiceValue = Random.Range( PlayerManager.instance.player.GetComponent<PlayerStats>().damage.GetValue() / 2,
                                                PlayerManager.instance.player.GetComponent<PlayerStats>().damage.GetValue()); //random damage. System is like frp
            int attackValue;
            if (actionDiceVelue > 15)
                attackValue = attackDiceValue;
            else
                attackValue = (attackDiceValue * actionDiceVelue * 5) / 100; //calculating final attack value accorging to dice values
            enemyStats.TakeDamage(attackValue);
            gameObject.GetComponent<EnemyController>().hasAttacked = true;
            CreateHoverUI.instance.CreateCooldownUI(PlayerManager.instance.player.GetComponent<PlayerStats>().attackCooldown.GetValue());
            Invoke("TakeDamage", PlayerManager.instance.player.GetComponent<PlayerStats>().attackCooldown.GetValue());
        }
    }
}
