using UnityEngine;

public class Water : MonoBehaviour
{
    
    private float timer = 0;
    private float timerHit = 0;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2f)
        {
            timer = 0;
            transform.localScale = new Vector3(-1f , 1f, 1f); 
        }
        else if (timer >= 1f)
        {
            transform.localScale = new Vector3(1f , 1f, 1f); 
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().inWater = true;
            timerHit += Time.deltaTime;
            if (timerHit >= 2)
            {
                collision.gameObject.GetComponent<Player>().RecountHP(-1);
                timerHit = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().inWater = false;
            timerHit = 0;
        }
    }

}
