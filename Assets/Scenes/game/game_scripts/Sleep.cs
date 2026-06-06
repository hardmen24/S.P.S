using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    private float sleepRefill =1.689666667f ;
    private float helingfactor = 0.5f;
    public Stats stats_script;

    private float timeSinceLastChange = 0f;
    private float changeInterval = 60f; // Change once per 60 seconds
    public bool is_in;

    public void sleep_calculate()
    {
        
            stats_script.sleepiness.Change(sleepRefill);
            stats_script.health.Change(helingfactor);
    }

    void Update()
    {
        if (is_in)
        {
            timeSinceLastChange += Time.deltaTime;
        if (timeSinceLastChange >= changeInterval)
        {
        sleep_calculate();
        timeSinceLastChange=0;
        
        }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Slime")){
            is_in=true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Slime")){
            is_in=false;
        }
    }

}
