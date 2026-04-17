using UnityEngine;

public class MagicCarpet : MonoBehaviour
{

    public Transform left, right;

    private  void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RaycastHit2D leftWall = Physics2D.Raycast(left.position, Vector2.left, 0.2f);
            RaycastHit2D rightWall = Physics2D.Raycast(right.position, Vector2.right, 0.2f);


            if ((Input.GetAxis("Horizontal") > 0 &&
                !rightWall.collider &&
                (collision.transform.position.x > transform.position.x)) ||
                (Input.GetAxis("Horizontal") < 0 &&
                !leftWall.collider &&
                (collision.transform.position.x < transform.position.x))
            )
            transform.position = new Vector3(
                collision.gameObject.transform.position.x,
                transform.position.y,
                transform.position.z
            );
        }
    }

}
