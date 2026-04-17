using UnityEngine;
using System.Collections;

public class AdvanceAirPatrol : MonoBehaviour
{
    
    public Transform[] points;
    public float Speed = 2f;
    public float WaitTime = 4f;
    public bool CanGo = true;
    public int i = 1;

    private void Start()
    {
        gameObject.transform.position = new Vector3(points[0].position.x, points[0].position.y, transform.position.z);
    }

    private void Update()
    {
        if (CanGo)
            transform.position = Vector3.MoveTowards(transform.position, points[i].position, Speed * Time.deltaTime);

        if (transform.position == points[i].position)
        {
            if (i < points.Length - 1)
                i++;
            else
                i = 0;
            CanGo = false;
            Transform t = points[i];
            points[i] = points[i];
            points[i] = t;
            StartCoroutine(WaitTimeFunc());
            
        }
    }

    private IEnumerator WaitTimeFunc()
    {
        yield return new WaitForSeconds(WaitTime);
        CanGo = true;
    }

}
