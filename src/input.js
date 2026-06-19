// input.js
// Tracks whether the player is steering left or right.
// Exposes a global `Input` object used by player.js

const Input = {
  left: false,
  right: false,
};

window.addEventListener('keydown', (e) => {
  if (e.key === 'ArrowLeft' || e.key === 'a' || e.key === 'A') Input.left = true;
  if (e.key === 'ArrowRight' || e.key === 'd' || e.key === 'D') Input.right = true;
});

window.addEventListener('keyup', (e) => {
  if (e.key === 'ArrowLeft' || e.key === 'a' || e.key === 'A') Input.left = false;
  if (e.key === 'ArrowRight' || e.key === 'd' || e.key === 'D') Input.right = false;
});

// Touch support: tap/hold left half or right half of the canvas
function setupTouchInput(canvas) {
  const handleTouch = (touches, active) => {
    if (!active || touches.length === 0) {
      Input.left = false;
      Input.right = false;
      return;
    }
    const rect = canvas.getBoundingClientRect();
    const x = touches[0].clientX - rect.left;
    if (x < rect.width / 2) {
      Input.left = true;
      Input.right = false;
    } else {
      Input.right = true;
      Input.left = false;
    }
  };

  canvas.addEventListener('touchstart', (e) => handleTouch(e.touches, true));
  canvas.addEventListener('touchmove', (e) => handleTouch(e.touches, true));
  canvas.addEventListener('touchend', () => handleTouch([], false));
  canvas.addEventListener('touchcancel', () => handleTouch([], false));
}
