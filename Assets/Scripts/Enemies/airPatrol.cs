using System.Collections;
using UnityEngine;

public class airPatrol : MonoBehaviour
{
    
    public Transform point1;
    public Transform point2;
    public float speed = 2f;
    private bool _canGo = true;
    private float _waitTime = 1f;

    private void Start()
    {
        gameObject.transform.position = new Vector3(point1.position.x, point1.position.y, transform.position.z);
    }

    private void Update()
    {
        if (_canGo)
            transform.position = Vector3.MoveTowards(transform.position, point1.position, speed * Time.deltaTime);

        if (transform.position == point1.position)
        {
            _canGo = false;
            Transform t = point1;
            point1 = point2;
            point2 = t;
            StartCoroutine(WaitTimeFunc());
            
        }
    }

    private IEnumerator WaitTimeFunc()
    {
        yield return new WaitForSeconds(_waitTime);
        if (transform.rotation.y == 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else 
            transform.eulerAngles = new Vector3(0, 0, 0);
        _canGo = true;
    }

}
