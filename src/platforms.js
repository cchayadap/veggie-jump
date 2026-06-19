// platforms.js
// Handles platform generation, drawing, and scrolling recycling.

const PLATFORM_WIDTH = 70;
const PLATFORM_HEIGHT = 16;

// Platform types: 'normal' (static), 'moving' (drifts left/right)
function createPlatform(x, y, type = 'normal') {
  return {
    x,
    y,
    width: PLATFORM_WIDTH,
    height: PLATFORM_HEIGHT,
    type,
    direction: Math.random() < 0.5 ? -1 : 1, // for moving platforms
    speed: 1 + Math.random() * 1.5,
  };
}

function generateInitialPlatforms(canvasWidth, canvasHeight) {
  const platforms = [];

  // Guaranteed platform right under the player to start
  platforms.push(createPlatform(canvasWidth / 2 - PLATFORM_WIDTH / 2, canvasHeight - 40, 'normal'));

  let y = canvasHeight - 120;
  while (y > -canvasHeight) {
    const x = Math.random() * (canvasWidth - PLATFORM_WIDTH);
    const type = Math.random() < 0.2 ? 'moving' : 'normal';
    platforms.push(createPlatform(x, y, type));
    y -= 70 + Math.random() * 40; // vertical gap between platforms
  }

  return platforms;
}

function updatePlatforms(platforms, canvasWidth) {
  for (const p of platforms) {
    if (p.type === 'moving') {
      p.x += p.direction * p.speed;
      if (p.x <= 0 || p.x + p.width >= canvasWidth) {
        p.direction *= -1;
      }
    }
  }
}

// Removes platforms that have scrolled below the screen, and adds new ones above
function recyclePlatforms(platforms, cameraY, canvasWidth, canvasHeight) {
  // Remove platforms far below the visible camera area
  for (let i = platforms.length - 1; i >= 0; i--) {
    if (platforms[i].y > cameraY + canvasHeight + 100) {
      platforms.splice(i, 1);
    }
  }

  // Find the highest (smallest y) platform currently tracked
  let topY = canvasHeight;
  for (const p of platforms) {
    if (p.y < topY) topY = p.y;
  }

  // Keep generating new platforms above the current top
  while (topY > cameraY - canvasHeight) {
    topY -= 70 + Math.random() * 40;
    const x = Math.random() * (canvasWidth - PLATFORM_WIDTH);
    const type = Math.random() < 0.2 ? 'moving' : 'normal';
    platforms.push(createPlatform(x, topY, type));
  }
}

function drawPlatforms(ctx, platforms, cameraY) {
  for (const p of platforms) {
    const screenY = p.y - cameraY;
    ctx.fillStyle = p.type === 'moving' ? '#ffb703' : '#4caf50';
    ctx.beginPath();
    ctx.roundRect(p.x, screenY, p.width, p.height, 6);
    ctx.fill();
  }
}
