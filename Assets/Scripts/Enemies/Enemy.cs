using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private bool isHit = false;
    public GameObject drop = null;

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player" && !isHit)
        {
            collision.gameObject.GetComponent<Player>().RecountHP(-1);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 5f, ForceMode2D.Impulse);
        }
            
    }

    public IEnumerator Death()
    {
        if (drop != null)
        {
            Instantiate(drop, transform.position, Quaternion.identity);
        }

        isHit = true;
        GetComponent<Animator>().SetBool("death", true);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = false;
        transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public void StartDeath()
    {
        StartCoroutine(Death());
    }
    
}
