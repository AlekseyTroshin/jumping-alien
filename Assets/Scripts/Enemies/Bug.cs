using System.Collections;
using UnityEngine;

public class Bug : MonoBehaviour
{
    
    private float _speed = 4f;
    private bool _isWait = false;
    private bool _isHidden = false;
    public float _timeWait = 4f;
    public Transform point;

    private void Start()
    {
        point.transform.position = new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z);
    }

    private void Update()
    {
        if (_isWait == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, point.position, _speed * Time.deltaTime);
        }
        if (transform.position == point.position)
        {
            if (_isHidden)
            {
                point.transform.position = new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z);
                _isHidden = false;
            }
            else
            {
                point.transform.position = new Vector3(transform.position.x, transform.position.y - 1.2f, transform.position.z);
                _isHidden = true;
            }

            _isWait = true;

            StartCoroutine(Waiting());
        }
    }

    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(_timeWait);
        _isWait = false;
    }

}
