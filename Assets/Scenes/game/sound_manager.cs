using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class sound_manager : MonoBehaviour
{
    
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI textVolume;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            load();
        }
        else
        {
            volumeSlider.value = 1f;
            textVolume.text = "100%";
        }
    }

    public void SetVolume()
    {
        AudioListener.volume = volumeSlider.value;
        textVolume.text =  (int)(volumeSlider.value * 100) + "%";
        save();
    }
    private void save()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }
    private void load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 1f);
        textVolume.text = (int)(volumeSlider.value * 100) + "%";
    }
}
