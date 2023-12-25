using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float maxHp = 100f; // Hp Max
    public float hp = 100f; // HP hien tai
    public int xp = 0; // XP

    public Slider hpSlider; // Reference den UI Slider cho HP
    public Slider xpSlider; // Reference den UI Slider cho XP

    void Start()
    {
        // Khoi tao gtri ban dau
        hp = maxHp;
        UpdateUI();
    }

    void Update()
    {
        // Gôi ham xu ly input, di chuyen, va cac logic khac o day
    }

    // Ham giam HP
    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp < 0f)
            hp = 0f; // Dam bao khon bao giô duoi Hp = 0

        UpdateUI();

        // ktra va xu li khi hp = 0 (player die)
        if (hp == 0f)
        {
            // goi ham xu li player die
            Die();
        }
    }

    // Ham tang XP
    public void GainXp(int amount)
    {
        xp += amount;
        UpdateUI();
    }

    // Ham cap nhap UI
    void UpdateUI()
    {
        hpSlider.value = hp / maxHp; // Cap nhap gia tri cho Slider HP
        xpSlider.value = (float)xp / 100f; // Cap nhap gia tri cho Slider XP
    }

    // Ham xu li khi player die
    void Die()
    {
        // Xu li khi playe, vd respawn, game over, etc.
    }
}
