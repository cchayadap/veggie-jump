// main.js
// Wires everything together: game loop, camera/score, state management.

const canvas = document.getElementById('gameCanvas');
const ctx = canvas.getContext('2d');
const overlay = document.getElementById('overlay');
const gameOverScreen = document.getElementById('gameOver');
const scoreLabel = document.getElementById('scoreLabel');
const finalScoreLabel = document.getElementById('finalScore');
const startBtn = document.getElementById('startBtn');
const restartBtn = document.getElementById('restartBtn');

setupTouchInput(canvas);

let player, platforms, cameraY, score, highestY, gameState;

function resetGame() {
  player = createPlayer(canvas.width, canvas.height);
  platforms = generateInitialPlatforms(canvas.width, canvas.height);
  cameraY = 0;
  score = 0;
  highestY = player.y;
  gameState = 'playing';
}

function gameLoop() {
  if (gameState === 'playing') {
    updatePlayer(player, canvas.width);
    updatePlatforms(platforms, canvas.width);

    const landedOn = checkPlatformCollision(player, platforms);
    if (landedOn) {
      player.vy = JUMP_VELOCITY;
    }

    // Camera follows the player upward only (never scrolls back down)
    const targetCameraY = player.y - canvas.height * 0.6;
    if (targetCameraY < cameraY) {
      cameraY = targetCameraY;
    }

    // Score increases the higher you climb
    if (player.y < highestY) {
      highestY = player.y;
      score = Math.floor((canvas.height - 80 - highestY) / 10);
      if (score < 0) score = 0;
      scoreLabel.textContent = score;
    }

    recyclePlatforms(platforms, cameraY, canvas.width, canvas.height);

    // Fall off the bottom of the screen = game over
    if (player.y - cameraY > canvas.height + 60) {
      endGame();
    }

    draw();
  }

  requestAnimationFrame(gameLoop);
}

function draw() {
  ctx.clearRect(0, 0, canvas.width, canvas.height);
  drawPlatforms(ctx, platforms, cameraY);
  drawPlayer(ctx, player, cameraY);
}

function endGame() {
  gameState = 'gameover';
  finalScoreLabel.textContent = `Score: ${score}`;
  gameOverScreen.classList.remove('hidden');
}

startBtn.addEventListener('click', () => {
  overlay.classList.add('hidden');
  resetGame();
});

restartBtn.addEventListener('click', () => {
  gameOverScreen.classList.add('hidden');
  resetGame();
});

requestAnimationFrame(gameLoop);
