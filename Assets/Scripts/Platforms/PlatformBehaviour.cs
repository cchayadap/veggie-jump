using UnityEngine;
using VeggieJump.Player;

namespace VeggieJump.Platforms
{
    public enum PlatformType
    {
        Normal,
        Moving,
        Crumbling // reserved for a future feature - see README roadmap
    }

    /// <summary>
    /// Attach to each platform prefab. Handles side-to-side drift for
    /// "Moving" platforms and bounces the player upward when landed on.
    /// </summary>
    public class PlatformBehaviour : MonoBehaviour
    {
        public PlatformType Type = PlatformType.Normal;

        [Header("Moving platform settings")]
        [SerializeField] private float moveSpeed = 1.5f;
        [SerializeField] private float moveRange = 1.5f;

        private Vector3 startPos;
        private int direction = 1;

        private void Start()
        {
            startPos = transform.position;
            direction = Random.value < 0.5f ? -1 : 1;
        }

        private void Update()
        {
            if (Type != PlatformType.Moving) return;

            transform.position += Vector3.right * (direction * moveSpeed * Time.deltaTime);

            if (Mathf.Abs(transform.position.x - startPos.x) >= moveRange)
            {
                direction *= -1;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null && player.IsFalling)
            {
                player.Bounce();
            }
        }
    }
}
