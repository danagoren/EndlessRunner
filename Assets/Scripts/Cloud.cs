using UnityEngine;

public class Cloud : MonoBehaviour
{
    public Vector2 speed = new Vector2(-3f, 0);
    public Vector3 outOfFramePoint = new Vector3(-10f, 0, 0);
    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = speed; //go left
    }

    void Update()
    {
        if (transform.position.x < outOfFramePoint.x) //when out of frame, destroy
        {
            Destroy(gameObject);
        }
    }

    public void Init(Vector3 point)
    {
        outOfFramePoint = point;
    }
}