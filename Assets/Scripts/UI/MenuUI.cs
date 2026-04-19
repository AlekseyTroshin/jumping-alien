using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{

    [SerializeField] private Player _player;
    [SerializeField] private Button[] _levels;
    [SerializeField] private TMP_Text _cointText;
    [SerializeField] private Slider _musicSlider, _soundSlider;
    [SerializeField] private TMP_Text _musicText, _soundText;

    private int cointsGlobal = 0;

    private void Start()
    {
        CheckLevels();

        if (!PlayerPrefs.HasKey("Heart"))
            PlayerPrefs.SetInt("Heart", 0);
        if (!PlayerPrefs.HasKey("GemBlue"))
            PlayerPrefs.SetInt("GemBlue", 0);
        if (!PlayerPrefs.HasKey("GemGreen"))
            PlayerPrefs.SetInt("GemGreen", 0);

        if (PlayerPrefs.HasKey("Coints"))
            cointsGlobal = PlayerPrefs.GetInt("Coints");

        if (!PlayerPrefs.HasKey("MusicVolume"))
            PlayerPrefs.SetInt("MusicVolume", 3);

        if (!PlayerPrefs.HasKey("SoundVolume"))
            PlayerPrefs.SetInt("SoundVolume", 3);

        _musicSlider.value = PlayerPrefs.GetInt("MusicVolume");
        _soundSlider.value = PlayerPrefs.GetInt("SoundVolume");
        _musicText.text = PlayerPrefs.GetInt("MusicVolume").ToString();
        _soundText.text = PlayerPrefs.GetInt("SoundVolume").ToString();
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("Coints"))
            _cointText.text = PlayerPrefs.GetInt("Coints").ToString();
        else
            _cointText.text = "0";

        PlayerPrefs.SetInt("MusicVolume", (int)_musicSlider.value);
        PlayerPrefs.SetInt("SoundVolume", (int)_soundSlider.value);
        _musicText.text = _musicSlider.value.ToString();
        _soundText.text = _soundSlider.value.ToString();
    }

    public void CheckLevels()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            for (int i = 0; i < _levels.Length; i++)
                if (i < PlayerPrefs.GetInt("Level"))
                    _levels[i].interactable = true;
                else
                    _levels[i].interactable = false;
        }
    }

    public void OpenScene(int index)
    {
        SceneManager.LoadScene(index);
        Time.timeScale = 1f;
    }

    public void DeleteKeys()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Menu");
    }

    public void BuyHeart(int cost)
    {
        if (cointsGlobal >= cost)
        {
            cointsGlobal -= cost;
            PlayerPrefs.SetInt("Heart", PlayerPrefs.GetInt("Heart") + 1);
            PlayerPrefs.SetInt("Coints", cointsGlobal);
        }    
    }

    public void BuyGemBlue(int cost)
    {
        if (cointsGlobal >= cost)
        {
            cointsGlobal -= cost;
            PlayerPrefs.SetInt("GemBlue", PlayerPrefs.GetInt("GemBlue") + 1);
            PlayerPrefs.SetInt("Coints", cointsGlobal);
        }    
    }

    public void BuyGemGreen(int cost)
    {
        if (cointsGlobal >= cost)
        {
            cointsGlobal -= cost;
            PlayerPrefs.SetInt("GemGreen", PlayerPrefs.GetInt("GemGreen") + 1);
            PlayerPrefs.SetInt("Coints", cointsGlobal);
            
        }    
    }

}
