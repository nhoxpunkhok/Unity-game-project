using UnityEngine;
public class Stats : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    public HealthBar healthBar;
    public GameObject deathEffect;

    private float currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;

        healthBar.SetSliderMax(maxHealth);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
		{
			TakeDamage(20);
		}
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.SetSlider(currentHealth);
    }
    public void HealPlayer(float amount)
    {
        currentHealth += amount;
        healthBar.SetSlider(currentHealth);
    }

    private void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Debug.Log("You died!");

        //Play death animation

        //Activate death screen

        //...
    }
}