using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class Hunger_bar : MonoBehaviour
{
   public Stats stats_script;
   public Slider slider;
    void Start()
    {
        slider.maxValue = 100f;
    }
    void Update()
    {
        slider.value = stats_script.hunger.GetValue();
    }
}
