using UnityEngine;

public class EnemyWorm : MonoBehaviour
{

    public float speed = 1f;
    public int move = 1;
    public Transform groundDetect = null;

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetect.position, Vector2.down, 1f);

        if (groundInfo.collider == false)
        {
            if (move == 1)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                move = 2;
            }
            else if (move == 2)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                move = 1;
            }
        }
    }

}
