using UnityEngine;

namespace VeggieJump.Input
{
    /// <summary>
    /// Reads steering input from keyboard (for editor testing) and touch
    /// (left half / right half of screen) for mobile builds.
    /// Other scripts read InputManager.Instance.MoveDirection (-1, 0, or 1).
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        /// <summary>-1 = left, 0 = none, 1 = right</summary>
        public float MoveDirection { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Update()
        {
            float direction = 0f;

            // Keyboard (editor / desktop testing)
            if (UnityEngine.Input.GetKey(KeyCode.LeftArrow) || UnityEngine.Input.GetKey(KeyCode.A))
                direction = -1f;
            else if (UnityEngine.Input.GetKey(KeyCode.RightArrow) || UnityEngine.Input.GetKey(KeyCode.D))
                direction = 1f;

            // Touch (mobile)
            if (UnityEngine.Input.touchCount > 0)
            {
                Touch touch = UnityEngine.Input.GetTouch(0);
                direction = touch.position.x < Screen.width / 2f ? -1f : 1f;
            }

            MoveDirection = direction;
        }
    }
}
