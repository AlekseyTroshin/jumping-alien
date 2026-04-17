using System.Collections;
using UnityEngine;

public class Bollet : MonoBehaviour
{
    
    public float _speed = 1f;
    private float _timeToDisable = 5f;

    private void Start()
    {
        StartCoroutine(SetDisabled());
    }

    private void Update()
    {
        transform.Translate(Vector2.down * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopCoroutine(SetDisabled());
        gameObject.SetActive(false);
    }

    private IEnumerator SetDisabled()
    {
        yield return new WaitForSeconds(_timeToDisable);
        gameObject.SetActive(false);
    }
    

}
