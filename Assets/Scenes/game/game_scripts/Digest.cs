using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Digest : MonoBehaviour
{
    [SerializeField] private float hungerRefill;
    public Stats stats;
    private bool _eaten = false;

    private void Start()
    {
        // This searches the scene directly for the Stats script component!
        stats = Object.FindAnyObjectByType<Stats>();

        if (stats != null)
        {
            Debug.Log("Successfully found the Stats script directly!");
        }
        else
        {
            Debug.LogError("Could not find any Stats script active in this scene!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object we hit has the tag "Tag"
        if (collision.gameObject.CompareTag("Core") && !_eaten)
        {
             Debug.Log("slime recognized");
            stats.hunger.Change(hungerRefill);
            _eaten = true;
            Debug.Log("food refiled");
            Destroy(gameObject);
            
        }
    }
}
