using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{

    [SerializeField]
    private int maxHealth;
    private int health;

    [SerializeField]
    private Slider healthSlider;

    private void Start()
    {
        healthSlider.maxValue = maxHealth;
        health = maxHealth;
        healthSlider.value = health;
    }
    public void takeDamage()
    {
        health--;
        healthSlider.value = health;
    }


    private void Update()
    {
        if (health <= 0) 
        {
            SceneManager.LoadScene("DeathScreen");
            print("player is dead");
        }
    }
}
