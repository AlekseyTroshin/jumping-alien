using Unity.VisualScripting;
using UnityEngine;

public class Finish : MonoBehaviour
{

    [SerializeField] private Main main;
    [SerializeField] private Sprite finishSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().sprite = finishSprite;
            main.WinGame();
        }
    }

}
