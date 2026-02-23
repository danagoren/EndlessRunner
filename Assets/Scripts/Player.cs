using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MiniGameManager miniGameManager;
    [SerializeField] private Sprite[] runSprites;
    [SerializeField] private Sprite jumpSprite;
    [SerializeField] private float jumpSpeed = 20;
    private Rigidbody2D rb;
    private int animFrames;
    private SpriteRenderer sr;
    private int animCount;
    private GameObject player;
    private float animTimePeriod = 0.1f; //animation time per frame
    private float animTimePassed;
    private bool isJumping;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = runSprites[0];
        animTimePassed = 0;
        animCount = 0;
        animFrames = runSprites.Length;
        isJumping = false;
    }

    private void Update()
    {
        Movement();
    }
    private void Movement()
    {
        Jump();
        if (isJumping == false)
        {
            animTimePassed += Time.deltaTime;
            if (animTimePassed >= animTimePeriod)
            {
                animTimePassed = 0;
                sr.sprite = runSprites[animCount % animFrames];
                animCount++;
            }
        }
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isJumping == false)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
            sr.sprite = jumpSprite;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))//drop to ground
        {
            isJumping = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))//jump from ground
        {
            isJumping = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))//loss
        {
            miniGameManager.Loss();
        }   
    }
}
