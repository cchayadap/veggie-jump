using UnityEngine;

namespace VeggieJump.Enemy
{
    /// <summary>
    /// Chopo relentlessly climbs from below the camera. If Chopo catches up
    /// to the player, it's game over - this gives "fall off the bottom"
    /// real narrative weight (Chopo is eating the veggie family!).
    /// Attach to a Chopo GameObject tagged "Chopo" with a Trigger Collider2D.
    /// </summary>
    public class ChopoChaser : MonoBehaviour
    {
        [SerializeField] private float baseSpeed = 1.2f;
        [SerializeField] private float speedIncreasePerSecond = 0.02f;
        [SerializeField] private Transform mainCamera;

        private float elapsed;

        private void Update()
        {
            elapsed += Time.deltaTime;
            float currentSpeed = baseSpeed + speedIncreasePerSecond * elapsed;

            transform.position += Vector3.up * (currentSpeed * Time.deltaTime);
        }

        /// <summary>
        /// Call this from GameManager on restart to reset Chopo below the
        /// camera's starting view.
        /// </summary>
        public void ResetPosition(Vector3 startPos)
        {
            transform.position = startPos;
            elapsed = 0f;
        }
    }
}
