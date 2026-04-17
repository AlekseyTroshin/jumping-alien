using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{

    [SerializeField] private Player _player;
    [SerializeField] private Button[] _levels;
    [SerializeField] private TMP_Text _cointText;

    private void Start()
    {
        CheckLevels();
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("Coints"))
            _cointText.text = PlayerPrefs.GetInt("Coints").ToString();
        else
            _cointText.text = "0";
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

}
