using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private LayerMask ignoreLayer;
    [SerializeField] private float gravityScaleOnPullDown = 10f;
    private new Rigidbody2D rigidbody2D;
    private CircleCollider2D circleCollider2D;

    [SerializeField]
    private float moveSpeed = 10;

    public void Reset()
    {
        rigidbody2D.gravityScale = 0f;
        circleCollider2D.excludeLayers = 0;
    }

    public void PullDown()
    {
        rigidbody2D.gravityScale = gravityScaleOnPullDown;
        circleCollider2D.excludeLayers = ignoreLayer;
    }

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        rigidbody2D.velocity = rigidbody2D.velocity.normalized * moveSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Strings.wall))
        {
            AudioManager.instance.Play(Strings.bounceAudio);
        }
    }
}