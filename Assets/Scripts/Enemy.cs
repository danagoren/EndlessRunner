using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector2 speed = new Vector2(-9f, 0);
    [SerializeField] private Vector3 outOfFramePoint = new Vector3(-10f, 0, 0);
    private void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = speed; //go left
    }

    private void Update()
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