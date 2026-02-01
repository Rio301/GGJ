using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Visuals (Order matters)")]
    public List<SpriteRenderer> healthSprites; // left → right or bottom → top
    public List<Light2D> healthLights; // left → right or bottom → top

    [Header("Colors")]
    public Sprite fullColor;
    public Sprite emptyColor;

    private int maxHealth;

    void Start()
    {
        maxHealth = healthSprites.Count;
        Data.health = maxHealth;
    }

    void Update()
    {
        int currentHealth = Mathf.Clamp(Data.health, 0, maxHealth);

        for (int i = 0; i < healthSprites.Count; i++)
        {
            // Example: i = 0 → first heart
            // If currentHealth = 3 → indices 0,1,2 are full
            healthSprites[i].sprite = i < currentHealth ? fullColor : emptyColor;
            healthLights[i].enabled = i < currentHealth;
        }

        if (Data.health <= 0)
        {
            SceneManager.LoadScene("EndGame");
            Debug.LogWarning("EndConditionnya Belum Di tambah");
        }
    }
}
