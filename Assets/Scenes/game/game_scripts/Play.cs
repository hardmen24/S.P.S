using UnityEngine;

public class Play : MonoBehaviour
{
    public Stats stat_script;
    private float timeSinceLastChange = 0f;
    private float changeInterval = 1f; 

    void Update_happiness()
    {
        timeSinceLastChange += Time.deltaTime;
        if (timeSinceLastChange >= changeInterval)
        {
            timeSinceLastChange = 0f;
            stat_script.happiness.Change(0.33433f);
        }
    }
   private void  OnCollisionStay2D(Collision2D collision)
    {
        // Check if the object we hit has the tag "Tag"
        if (collision.gameObject.CompareTag("cursor") )
        {
            Update_happiness();
        }
    }
}
