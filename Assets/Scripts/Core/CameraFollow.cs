using UnityEngine;

namespace VeggieJump.Core
{
    /// <summary>
    /// Camera follows the player upward only - never scrolls back down,
    /// matching classic Doodle Jump camera behaviour. Attach to Main Camera.
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float verticalOffset = 1.5f;
        [SerializeField] private float smoothSpeed = 5f;

        private float highestCameraY;

        private void Start()
        {
            highestCameraY = transform.position.y;
        }

        private void LateUpdate()
        {
            if (player == null) return;

            float targetY = player.position.y + verticalOffset;

            if (targetY > highestCameraY)
            {
                highestCameraY = Mathf.Lerp(highestCameraY, targetY, smoothSpeed * Time.deltaTime);
            }

            transform.position = new Vector3(transform.position.x, highestCameraY, transform.position.z);
        }
    }
}
