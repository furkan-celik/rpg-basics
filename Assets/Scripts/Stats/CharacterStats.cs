using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour {

    public int maxHealth = 100;
    public int CurrentHealth { get; private set; }
    public int money = 0;
    public GameObject headOverDisplayPrefab;

    public Stat damage;
    public Stat armor;

    public Modifier attackCooldown;

    [HideInInspector]
    public bool deathState = false;
    [HideInInspector]
    public bool onAttack = false;

    private GameObject headOverDisplay;

	// Use this for initialization
	void Start () {
        //headOverDisplay = Instantiate<GameObject>(headOverDisplayPrefab, transform);
        //headOverDisplay.transform.position = Camera.main.WorldToViewportPoint(gameObject.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
        
        //headOverDisplay.transform.position = Camera.main.WorldToViewportPoint(gameObject.transform.position);
    }

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        amount -= armor.GetValue();
        amount = Mathf.Clamp(amount, 0, int.MaxValue);
        CurrentHealth -= amount;
        Debug.Log(transform.name + " take " + amount + " of damage. Current health is " + CurrentHealth);

        if(CurrentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        //This method will be overwritten
        Debug.Log(transform.name + " died.");
        deathState = true;
    }

    public virtual void TakeMoney(int amount)
    {
        money += amount;
    }

    public void GetAttacked()
    {
        onAttack = true;
    }

    public void AttackFinished()
    {
        onAttack = false;
    }
}
