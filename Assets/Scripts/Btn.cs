using UnityEngine;

public class Btn : MonoBehaviour
{

    public GameObject[] blocks;
    public Sprite btnDown;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MarkBox")
        {
            GetComponent<SpriteRenderer>().sprite = btnDown;
            gameObject.transform.position = 
                new Vector3(transform.position.x, transform.position.y - 0.17846f, 0);
            GetComponent<CircleCollider2D>().enabled = false;

            foreach (GameObject block in blocks)
                Destroy(block);
        }
    }

}