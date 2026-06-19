# Sky Hopper 🦘

A Doodle-Jump-style endless platformer. Steer left and right to land on platforms and climb as high as you can — fall off the bottom and it's game over.

## How to play

- **Desktop:** Arrow keys or `A` / `D` to move left/right
- **Mobile/touch:** Tap and hold the left or right half of the screen

## Running it locally

No build tools, no npm install — just open the file:

```bash
git clone <your-repo-url>
cd doodle-jump
open index.html   # or just double-click it in your file explorer
```

That's it. It's plain HTML/CSS/JS, so it runs straight from the file system.

## Project structure

```
doodle-jump/
├── index.html        # Page shell + canvas
├── style.css         # Visual styling
├── src/
│   ├── input.js      # Keyboard + touch input handling
│   ├── platforms.js  # Platform generation, movement, drawing
│   ├── player.js     # Player physics, drawing, collision
│   └── main.js        # Game loop that ties everything together
└── assets/           # (sprites/sounds go here later)
```

Scripts load in this order in `index.html`: `input.js` → `platforms.js` → `player.js` → `main.js`. Keep that order if you add new files that depend on each other.

## Working on this with a friend

Suggested workflow so you don't overwrite each other's work:

1. **Push this repo to GitHub** (see below) and add your friend as a collaborator.
2. **Never commit directly to `main`.** Each of you works on a feature branch:
   ```bash
   git checkout -b feature/moving-platforms
   # make changes
   git add .
   git commit -m "Add moving platform type"
   git push origin feature/moving-platforms
   ```
3. **Open a Pull Request on GitHub** when a feature is ready, and have the other person review/merge it.
4. **Pull before you start working** each session:
   ```bash
   git checkout main
   git pull
   ```

Splitting work by file works well here since the codebase is modular:
- One person owns `player.js` + `input.js` (movement/physics feel)
- The other owns `platforms.js` (level generation, platform types, difficulty)
- Collaborate on `main.js` together since it ties both sides together

## Pushing to GitHub

```bash
# Create a new repo on github.com first, then:
git remote add origin <your-repo-url>
git branch -M main
git push -u origin main
```

## Ideas for next features

- [ ] Score persistence (localStorage high score)
- [ ] Breakable / disappearing platforms
- [ ] Springs/boosters for bigger jumps
- [ ] Enemies to avoid
- [ ] Sprite art instead of placeholder shapes
- [ ] Sound effects on jump/game over
- [ ] Difficulty scaling (platforms get sparser as you climb)
