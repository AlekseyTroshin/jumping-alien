using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    
    [SerializeField] private int heart, blueGem, greenGem;
    [SerializeField] private bool isKey;
    [SerializeField] private Sprite[] numbers;
    [SerializeField] private Sprite 
        noHeart,
        yesHeart,
        noBlueGem,
        yesBlueGem,
        noGreenGem,
        yesGreenGem,
        noKey,
        yesKey;
    [SerializeField] private Image 
        heartImage,
        blueGemImage,
        greenGemImage,
        keyImage;
    [SerializeField] private Player player;

    private void Start()
    {
        
        heart = PlayerPrefs.GetInt("Heart");
        blueGem = PlayerPrefs.GetInt("GemBlue");
        greenGem = PlayerPrefs.GetInt("GemGreen");

        if (heart > 0)
        {
            heartImage.sprite = yesHeart;
            heartImage.transform.GetChild(0)
                .GetComponent<Image>().sprite = numbers[heart];
        }

        if (blueGem > 0)
        {
            blueGemImage.sprite = yesBlueGem;
            blueGemImage.transform.GetChild(0)
                .GetComponent<Image>().sprite = numbers[blueGem];
        }

        if (greenGem > 0)
        {
            greenGemImage.sprite = yesGreenGem;
            greenGemImage.transform.GetChild(0)
                .GetComponent<Image>().sprite = numbers[greenGem];
        }
    }

    public void AddHeart()
    {
        heart++;
        heartImage.sprite = yesHeart;
        heartImage.transform.GetChild(0)
            .GetComponent<Image>().sprite = numbers[heart];
    }

    public void AddBlueGem()
    {
        blueGem++;
        blueGemImage.sprite = yesBlueGem;
        blueGemImage.transform.GetChild(0)
            .GetComponent<Image>().sprite = numbers[blueGem];
    }

    public void AddGreenGem()
    {
        greenGem++;
        greenGemImage.sprite = yesGreenGem;
        greenGemImage.transform.GetChild(0)
            .GetComponent<Image>().sprite = numbers[greenGem];
    }

    public void AddKey()
    {
        keyImage.sprite = yesKey;
        isKey = true;
    }

    public void UseHeart()
    {
        if (heart > 0 && player.Hearts < 3)
        {
            heart--;
            heartImage.transform.GetChild(0)
                .GetComponent<Image>().sprite = numbers[heart];
            player.RecountHP(1);

            if (heart <= 0)
                heartImage.sprite = noHeart;
        }
    }

    public void UseBlueGem()
    {
        if (blueGem > 0 && player.canHit)
        {
            blueGem--;
            blueGemImage.transform.GetChild(0)
                .GetComponent<Image>().sprite = numbers[blueGem];
            player.ActiveBlueGem();

            if (blueGem <= 0)
                blueGemImage.sprite = noBlueGem;
        }
    }

    public void UseGreenGem()
    {
        if (greenGem > 0)
        {
            greenGem--;
            greenGemImage.transform.GetChild(0)
                .GetComponent<Image>().sprite = numbers[greenGem];
            player.ActiveGreenGem();

            if (greenGem <= 0)
                greenGemImage.sprite = noGreenGem;
        }
    }

    public void RecountItems()
    {
        PlayerPrefs.SetInt("Heart", heart);
        PlayerPrefs.SetInt("GemBlue", blueGem);
        PlayerPrefs.SetInt("GemGreen", greenGem);
    }

}
