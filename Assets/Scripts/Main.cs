using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{

    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _cointText;
    [SerializeField] private Image[] _hearts;
    [SerializeField] private Sprite _isLife, _nonLife;
    [SerializeField] private GameObject _gamePlay;
    [SerializeField] private GameObject _gamePause;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TimeWork _timeWork;
    [SerializeField] private float _counDown;
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private SoundEffecter _soundEffecter;
    [SerializeField] private AudioSource _musicSource, _soundSource;

    private float _timer = 0f;


    private void Start()
    {   
        Time.timeScale = 1f;

        if ((int)_timeWork == 2)
            _timer = _counDown;

        _musicSource.volume = PlayerPrefs.GetInt("MusicVolume") / (float)10;
        _soundSource.volume = PlayerPrefs.GetInt("SoundVolume") / (float)10;
        
    }

    private void Update()
    {
        _cointText.text = _player.Coints.ToString();

        for (int i = 0; i < _hearts.Length; i++)
        {
            if (_player.Hearts > i)
                _hearts[i].sprite = _isLife;
            else
                _hearts[i].sprite = _nonLife;
        }

        if ((int)_timeWork == 1)
        {
            _timer += Time.deltaTime;
            _timerText.text = _timer.ToString("F2").Replace(",", ":");
        }
        else if ((int)_timeWork == 2)
        {
            _timer -= Time.deltaTime;
            if (_timer < 0) _timer = 0;
            // _timerText.text = _timer.ToString("F2").Replace(",", ":");
            int seconds = (int)_timer - ((int)_timer / 60) * 60;
            _timerText.text = ((int)_timer / 60).ToString() + ":" + seconds.ToString("D2");
                ;
            if (_timer <= 0)
            {
                LoseGame();
            }
        }
        else
        {
            _timerText.transform.parent.gameObject.SetActive(false);
        }
        
    }

    public void ReloadLvl()
    {
        Time.timeScale = 1f;
        _player.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameActive()
    {
        if (_gamePause.activeSelf)
        {
            Time.timeScale = 0f;
            _player.enabled = false;

            _gamePlay.SetActive(true);
            _gamePause.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            _player.enabled = true;
            
            _gamePlay.SetActive(false);
            _gamePause.SetActive(true);
        }
    }

    public void WinGame()
    {
        Time.timeScale = 0f;
        _player.enabled = false;
        _winScreen.SetActive(true);
        _inventoryPanel.SetActive(false);
        GetComponent<Inventory>().RecountItems();
        _soundEffecter.PlayWinSound();

        if (!PlayerPrefs.HasKey("Level") || 
            PlayerPrefs.GetInt("Level") < SceneManager.GetActiveScene().buildIndex)
        {
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);
        }

        if (PlayerPrefs.HasKey("Coints"))
            PlayerPrefs.SetInt("Coints", PlayerPrefs.GetInt("Coints") + _player.Coints);
        else
            PlayerPrefs.SetInt("Coints", _player.Coints);

           
    }

    public void LoseGame()
    {
        Time.timeScale = 0f;
        _player.enabled = false;
        _loseScreen.SetActive(true);
        _inventoryPanel.SetActive(false);
        _soundEffecter.PlayLoseSound();

        GetComponent<Inventory>().RecountItems();
    }

    public void MenuLvl()
    {
        SceneManager.LoadScene("Menu");
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == PlayerPrefs.GetInt("Level"))
        {
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        }
        
        SceneManager.LoadScene("Menu");
    }

}

public enum TimeWork
{
    None,
    StopWatch,
    Timer
}