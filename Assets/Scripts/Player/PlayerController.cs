using UnityEngine;
using VeggieJump.Input;
using VeggieJump.Core;

namespace VeggieJump.Player
{
    /// <summary>
    /// Controls the veggie character: horizontal steering, auto-bounce jump
    /// on platform landing, and screen wraparound (walk off one edge,
    /// appear on the other - classic Doodle Jump behaviour).
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 6f;
        [SerializeField] private float jumpForce = 12f;

        [Header("Screen Wrap")]
        [SerializeField] private float screenHalfWidth = 3f; // matches camera ortho size * aspect

        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        private bool isDead;

        public float HighestY { get; private set; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            HighestY = transform.position.y;
        }

        private void Update()
        {
            if (isDead) return;

            float dir = InputManager.Instance != null ? InputManager.Instance.MoveDirection : 0f;
            Vector2 velocity = rb.linearVelocity;
            velocity.x = dir * moveSpeed;
            rb.linearVelocity = velocity;

            // Flip sprite to face movement direction
            if (dir != 0 && spriteRenderer != null)
                spriteRenderer.flipX = dir < 0;

            WrapAroundScreen();

            if (transform.position.y > HighestY)
                HighestY = transform.position.y;
        }

        private void WrapAroundScreen()
        {
            Vector3 pos = transform.position;
            if (pos.x < -screenHalfWidth) pos.x = screenHalfWidth;
            else if (pos.x > screenHalfWidth) pos.x = -screenHalfWidth;
            transform.position = pos;
        }

        /// <summary>Called by PlatformBehaviour when the player lands on top of a platform.</summary>
        public void Bounce()
        {
            Vector2 velocity = rb.linearVelocity;
            velocity.y = jumpForce;
            rb.linearVelocity = velocity;
        }

        public bool IsFalling => rb.linearVelocity.y < 0;

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Caught by Chopo = game over, regardless of fall state
            if (other.CompareTag("Chopo"))
            {
                Die();
            }
        }

        public void Die()
        {
            if (isDead) return;
            isDead = true;
            GameManager.Instance.GameOver();
        }
    }
}
