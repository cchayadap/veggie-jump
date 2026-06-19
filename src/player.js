// player.js
// Handles the player character: physics, movement, drawing, collision.

const GRAVITY = 0.4;
const JUMP_VELOCITY = -10;
const MOVE_SPEED = 4;
const PLAYER_SIZE = 32;

function createPlayer(canvasWidth, canvasHeight) {
  return {
    x: canvasWidth / 2 - PLAYER_SIZE / 2,
    y: canvasHeight - 80,
    width: PLAYER_SIZE,
    height: PLAYER_SIZE,
    vy: JUMP_VELOCITY,
    facing: 1, // 1 = right, -1 = left
  };
}

function updatePlayer(player, canvasWidth) {
  // Horizontal movement
  if (Input.left) {
    player.x -= MOVE_SPEED;
    player.facing = -1;
  }
  if (Input.right) {
    player.x += MOVE_SPEED;
    player.facing = 1;
  }

  // Wrap around screen edges (classic Doodle Jump behavior)
  if (player.x + player.width < 0) player.x = canvasWidth;
  if (player.x > canvasWidth) player.x = -player.width;

  // Vertical physics
  player.vy += GRAVITY;
  player.y += player.vy;
}

// Checks collision only when falling, and only against the top of a platform
function checkPlatformCollision(player, platforms) {
  if (player.vy <= 0) return null; // only land while falling

  for (const p of platforms) {
    const withinX = player.x + player.width > p.x && player.x < p.x + p.width;
    const playerBottom = player.y + player.height;
    const wasAbove = playerBottom - player.vy <= p.y + 4; // was above platform last frame
    const nowOverlapping = playerBottom >= p.y && playerBottom <= p.y + p.height + 10;

    if (withinX && wasAbove && nowOverlapping) {
      return p;
    }
  }
  return null;
}

function drawPlayer(ctx, player, cameraY) {
  const screenY = player.y - cameraY;

  ctx.save();
  ctx.translate(player.x + player.width / 2, screenY + player.height / 2);
  ctx.scale(player.facing, 1);

  // Body
  ctx.fillStyle = '#3d348b';
  ctx.beginPath();
  ctx.roundRect(-player.width / 2, -player.height / 2, player.width, player.height, 8);
  ctx.fill();

  // Eye
  ctx.fillStyle = '#fff';
  ctx.beginPath();
  ctx.arc(6, -4, 6, 0, Math.PI * 2);
  ctx.fill();
  ctx.fillStyle = '#000';
  ctx.beginPath();
  ctx.arc(8, -4, 3, 0, Math.PI * 2);
  ctx.fill();

  ctx.restore();
}
