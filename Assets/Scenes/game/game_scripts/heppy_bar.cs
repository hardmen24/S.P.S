using UnityEngine;
using UnityEngine.UI;

public class heppy_bar : MonoBehaviour
{

   public Stats stats_script;
   public Slider slider;
    void Start()
    {
        slider.maxValue = 100f;
    }
    void Update()
    {
        slider.value = stats_script.happiness.GetValue();
    }

}