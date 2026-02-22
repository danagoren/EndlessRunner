using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] MiniGameManager miniGameManager;
    private Rigidbody2D rb;

    public Sprite[] runSprites;
    private int animFrames;
    public Sprite jumpSprite;
    public float jumpSpeed = 20;
    private SpriteRenderer sr;
    private int animCount;
    private GameObject player;
    private float animTimePeriod = 0.1f; //animation time per frame
    private float animTimePassed;
    private bool isJumping;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = runSprites[0];
        animTimePassed = 0;
        animCount = 0;
        animFrames = runSprites.Length;
        isJumping = false;
    }

    void Update()
    {
        Movement();
    }
    void Movement()
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

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isJumping == false)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
            sr.sprite = jumpSprite;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))//drop to ground
        {
            isJumping = false;
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
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
