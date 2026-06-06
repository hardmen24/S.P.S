using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    
    public stat hunger;
    public stat sleepiness;
    public stat happiness;
    public stat health;
    public string Name;
    public Sleep sleep_script;
    private stat age;
    private float timeSinceLastChange = 0f;
    private float changeInterval = 60f; // Change once per 60 seconds
    [SerializeField] private TextMeshProUGUI TextElement;
    
   public void SaveStats(){
    player_data data = new player_data
    {
        name      = Name,
        hunger    = hunger.GetValue(),
        sleepiness = sleepiness.GetValue(),
        happiness = happiness.GetValue(),
        health    = health.GetValue(),
        age       = age.GetValue(),
        lastSaveTime = System.DateTime.Now.ToString(),
        is_sleeping = sleep_script.is_in
    };
    Save_game.SaveData(Name, data);
    }
    
    public void New_game(string Name_in)
    {
        Name = Name_in;
        hunger = new stat(0.069f, this, "hladový");      
        sleepiness = new stat(0.023f, this, "unavený");
        happiness = new stat(0.06f, this, "nešťastný"); 
        health = new stat(0f, this, "zraněný");
        age = new stat(0.000694444f, this, "");
        age.Change(-100);
        transform.localScale = Vector2.one * 0.1f;
    }

void Update()
{
    timeSinceLastChange += Time.deltaTime;
    if (timeSinceLastChange >= changeInterval)
    {
        timeSinceLastChange = 0f;
        hunger.Decay();
        sleepiness.Decay();
        happiness.Decay();
        age.Grow();
    }
    UpdateStatusText(Name);
}

    void Start()
{
    if (player_data.current == null)
    {
        New_game("Slime"); // fallback
        return;
    }

    Name = player_data.current.name;
    hunger = new stat(0.069f, this, "hladový");
    sleepiness = new stat(0.023f, this, "unavený");
    happiness = new stat(0.06f, this, "nešťastný");
    health = new stat(0f, this, "zraněný");
    age = new stat(0.000694444f, this, "");

    hunger.SetValue(player_data.current.hunger);
    sleepiness.SetValue(player_data.current.sleepiness);
    happiness.SetValue(player_data.current.happiness);
    health.SetValue(player_data.current.health);
    age.SetValue(player_data.current.age);
    if (!string.IsNullOrEmpty(player_data.current.lastSaveTime))
    {
    System.DateTime lastSave = System.DateTime.Parse(player_data.current.lastSaveTime);
    System.TimeSpan offlineTime = System.DateTime.Now - lastSave;

    // how many decay ticks happened while offline
    int ticks = (int)(offlineTime.TotalSeconds / changeInterval);

    for (int i = 0; i < ticks; i++)
    {
        hunger.Decay();
        sleepiness.Decay();
        happiness.Decay();
        age.Grow();
        
    }
    if (player_data.current.is_sleeping)
    {
        for (int i = 0; i < ticks; i++)
        {
        sleep_script.sleep_calculate();
        }
    }
}
}

    private void UpdateStatusText(string Name)
    {
       
        if (TextElement == null) return;

        stat[] statsToWatch = { hunger, sleepiness, happiness, health };
        string currentStatus = "";

        foreach (stat s in statsToWatch)
        {
            if (s != null && s.GetValue() <= 20 && !string.IsNullOrEmpty(s.GetStav()))
            {
                if (currentStatus != "")
                {
                    currentStatus += ", ";
                }
                
                currentStatus += s.GetStav();
            }
        }

        if (currentStatus != "")
        {
            TextElement.text = $"{Name} je {currentStatus}";
        }
        else
        {
            TextElement.text = $"{Name} je spokojený"; // Default text when everything is fine!
        }
    }

    public void Damage(float amount)
    {
        health.Change(-amount);
        if (health.GetValue() <= 0)
        {
        }
    }
}



public class stat
{
    private float _value;
    private float _dropRate;
    private Stats _owner;
    private string _stav;

    public stat(float dropRate, Stats owner,string Stav)
    {
        _value = 80f;
        _dropRate = dropRate;
        _owner = owner;
        _stav = Stav;
    }

    public void Decay()
    {
        _value -= _dropRate;

        if (_value <= 0 && _owner != null)
        {
            _owner.Damage(1f);
        }
    }

    public void Change(float amount)
    {
        _value += amount;
        if (_value > 100f)
        {
            _value = 100f;
        }
        if (_value < 0f)
        {
            _value = 0f;
        }
    }
    public void Grow()
    {
        if(_value < 100f)
        {
            _value += _dropRate;
        }
        float scale=(0.009f * _value)+0.1f;
        _owner.transform.localScale = Vector2.one * scale;
    }

    public float GetValue()
    {
        return _value;
    }
    public string GetStav()
    {
        return _stav;
    }

    public void SetValue(float value)
{
    _value = Mathf.Clamp(value, 0f, 100f);
}


}

