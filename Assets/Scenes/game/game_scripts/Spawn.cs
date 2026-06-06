using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public UI_Manager manager;
    public Portal portal;
    public GameObject Prefab_cokie;
    public GameObject Prefab_pizza;
    public GameObject Prefab_burger;
    
    public void Spawn_cookie()
    {
        
        StartCoroutine(Spawn_Enumer(Prefab_cokie));
    }
    public void Spawn_pizza()
    {
        
        StartCoroutine(Spawn_Enumer(Prefab_pizza));
    }
    public void Spawn_burger()
    {
        
        StartCoroutine(Spawn_Enumer(Prefab_burger));
    }

    public IEnumerator Spawn_Enumer(GameObject prefab)
    {
        
        manager.OnResumePress(1);
        yield return new WaitForSeconds(0.2f);
        
        portal.ToggleYScale();
        yield return new WaitForSeconds(1f);
        
        Instantiate(prefab,new Vector2(3,3),transform.rotation);
        yield return new WaitForSeconds(1.5f);
        
        portal.ToggleYScale();
        yield return new WaitForSeconds(0.8f);
        
        manager.OnFoodPress();
       
        
    }
}
