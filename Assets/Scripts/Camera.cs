using UnityEngine;

public class Camera : MonoBehaviour
{
    
    private float speed = 3f;
    public Transform target;

    public void Start()
    {
        Application.targetFrameRate = 60;

        transform.position = new Vector3(
            target.transform.position.x, 
            target.transform.position.y,
            target.transform.position.z
        );

    }

    public void Update()
    {
        Vector3 position = target.position;
        position.z = -10;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
    }

}
