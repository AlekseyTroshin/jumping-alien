using UnityEngine;

public class BG : MonoBehaviour
{

    private float length, startPos;
    public GameObject cameraBg;
    public float paralaxEffect;

    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = cameraBg.transform.position.x * (1 - paralaxEffect);
        float dist = cameraBg.transform.position.x * paralaxEffect;

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (temp > startPos + length)
        {
            startPos += length;
        }
        else if (temp < startPos - length)
        {
            startPos -= length;
        }
    }

}
