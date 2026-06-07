using UnityEngine;
[System.Serializable]
public class player_data
{
    public static player_data current;
    public string slime_name;
    public float hunger;
    public float sleepiness;
    public float happiness;
    public float health;
    public float age;
    public string lastSaveTime; 
    public bool is_sleeping;
}
