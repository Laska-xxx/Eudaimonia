using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;

    public void Init(int maxHp)
    {
        healthSlider.maxValue = maxHp;
        healthSlider.value = maxHp;
    }

    public void UpdateHealth(int damage)
    {
        healthSlider.value -= damage;
    }
}
