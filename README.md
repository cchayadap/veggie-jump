# Veggie Jump 🥦🥕🍅

Chopo is eating the veggie family — help them escape! Steer left and right to land on platforms and climb as high as you can. Fall to the bottom (or let Chopo catch you) and it's game over.

This repo contains the **C# scripts** for the game. Since a full Unity project includes large auto-generated folders (`Library/`, `Temp/`, etc.) that shouldn't go in git, your friend needs to create the actual Unity project once, then drop these scripts in.

## One-time setup (whoever has Unity installed does this)

1. Open Unity Hub → **New Project** → **2D (Core)** template → name it `VeggieJump`
2. Close Unity, then copy the contents of this repo's `Assets/Scripts/` folder into your new project's `Assets/Scripts/` folder (overwrite the empty one Unity made)
3. Also copy `.gitignore` and `.gitattributes` from this repo into your new Unity project's root (replace Unity's defaults)
4. Open the project in Unity — it'll recompile the scripts automatically

## Scene setup (wiring it up in the Editor)

These scripts assume the following GameObjects exist in your scene. None of this can be scaffolded from text since Unity scenes are editor-built — this is where you and your friend will spend your first session together:

| GameObject | Components to add | Notes |
|---|---|---|
| **Player** | `SpriteRenderer`, `Rigidbody2D` (Gravity Scale ~3), `BoxCollider2D` or `CircleCollider2D`, `PlayerController.cs` | Tag it `Player`. Freeze Z rotation in Rigidbody2D constraints. |
| **PlatformNormal** (prefab) | `SpriteRenderer`, `BoxCollider2D` (set **Is Trigger** ✅), `PlatformBehaviour.cs` (Type = Normal) | Drag into `Assets/Prefabs/` to make it a prefab |
| **PlatformMoving** (prefab) | Same as above, `PlatformBehaviour.cs` (Type = Moving) | |
| **PlatformSpawner** (empty GameObject) | `PlatformSpawner.cs` | Drag both platform prefabs + Player + Main Camera into the Inspector fields |
| **Chopo** | `SpriteRenderer`, `BoxCollider2D` (Is Trigger ✅), `ChopoChaser.cs` | Tag it `Chopo`. Start it below the screen. |
| **Main Camera** | `CameraFollow.cs` | Set to Orthographic. Drag Player in. |
| **GameManager** (empty GameObject) | `GameManager.cs` | Drag Player in. |
| **InputManager** (empty GameObject) | `InputManager.cs` | No fields to wire |
| **Canvas → UIManager** | `UIManager.cs` | Drag in your Score Text, Game Over Panel, Final Score Text, Restart Button |

Tags you'll need to create (Inspector → Tag → Add Tag): `Player`, `Chopo`. Layers are optional for this scope but useful later if you want collision layers to ignore Chopo passing through platforms.

## Project structure

```
Assets/
├── Scripts/
│   ├── Input/
│   │   └── InputManager.cs      # keyboard + touch steering
│   ├── Player/
│   │   └── PlayerController.cs  # movement, bounce, screen wrap, death
│   ├── Platforms/
│   │   ├── PlatformBehaviour.cs # per-platform bounce + moving drift
│   │   └── PlatformSpawner.cs   # procedural generation + recycling
│   ├── Enemy/
│   │   └── ChopoChaser.cs       # rises from below, catches player = game over
│   ├── Core/
│   │   ├── GameManager.cs       # score, game over, restart
│   │   └── CameraFollow.cs      # upward-only camera scroll
│   └── UI/
│       └── UIManager.cs         # HUD + game over panel wiring
├── Prefabs/    (empty for now - platforms go here once you build them)
├── Sprites/    (empty for now - drop veggie/Chopo art here)
└── Scenes/     (your main game scene goes here)
```

## Working on this with a friend

Same branch workflow as before, just with Unity's quirks in mind:

```bash
git checkout -b feature/your-feature-name
# make changes in Unity
git add .
git commit -m "Describe what changed"
git push origin feature/your-feature-name
```

**Unity-specific gotchas for two people sharing a project:**

- **Only one person should edit a Scene file at a time.** `.unity` scene files are very prone to merge conflicts since they're large serialized files. Whoever isn't editing the scene should work in Scripts, Prefabs, or Sprites instead.
- **Set Unity to Visible Meta Files + Force Text serialization**: `Edit → Project Settings → Editor → Asset Serialization → Force Text`, and `Version Control Mode → Visible Meta Files`. Do this **before** your first commit — it makes `.meta` files and prefabs git-diffable instead of binary blobs.
- **Use Git LFS** for sprites/audio (already configured in `.gitattributes`) — run `git lfs install` once per machine before pulling art assets.

## Suggested split

- One person: `PlayerController` + `InputManager` (movement feel)
- Other person: `PlatformSpawner` + `PlatformBehaviour` + `ChopoChaser` (difficulty/pacing)
- Together: scene wiring, `GameManager`, UI

## Roadmap ideas

- [ ] Swap placeholder shapes for veggie sprites (carrot, tomato, broccoli...) and a Chopo character
- [ ] Crumbling/breakable platforms (enum value already reserved in `PlatformBehaviour`)
- [ ] Springs/boosters for bigger jumps
- [ ] Chopo speeds up over time (already does — tune `speedIncreasePerSecond`)
- [ ] Mobile build settings (Android/iOS) + app icon
- [ ] Sound effects: jump, Chopo growl, game over
- [ ] High score saved with `PlayerPrefs`
- [ ] Simple title screen / character select (pick which veggie you play as)
