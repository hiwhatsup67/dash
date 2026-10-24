# Dash and Fight

A small top-down 2D Unity prototype made for the INACAP TILE22 assignment.

## Run

- Open `Executable/DashAndFight.exe`.
- Or open `Assets/Scenes/Game.unity` with Unity `6000.6.7f1` and press Play.

## Controls

- `WASD`: move
- `Left Shift`: run
- Mouse: aim
- Left mouse button: shoot
- `Space`: hop forward
- `F`: clear nearby bullets once; collect the purple star to recharge it
- `R`: restart after game over

## Included

- A bounded top-down arena with three wall obstacles and a laser hazard.
- A player with health, walking/running, shooting, a visual top-down hop, and bullet clearing.
- Two patrolling enemies that shoot at the player.
- Bouncing bullets, enemy contact damage, a health HUD, score HUD, game-over screen, audio, and restart.
- Four collectible stars: maximum health, movement speed, fire rate, and bullet-clear recharge.
- Layers for Player, Enemy, Wall, Bullet, and Boundary. Bullet-to-bullet collision is disabled.

The jump is intentionally a top-down hop: the player moves two units forward while the visual rises and falls. It is not platformer gravity.

## Scripts

- `Health`: stores health, applies damage, and raises health/death events.
- `GameManager`: shows game over, disables the player on death, and reloads on `R`.
- `ScoreKeeper`: stores score and collected-star totals.
- `AudioPlayer`: plays one-shot game sound effects.
- `Projectile`: launches, damages health targets, and reflects from walls.
- `DamageZone`: applies timed damage while a target remains in a trigger.
- `PlayerMovement`: reads movement input, runs with Shift, and tracks facing direction.
- `PlayerJump`: handles the charged top-down hop and ignores walls while airborne.
- `PlayerShooter`: aims at the mouse and fires player bullets.
- `BulletClearAbility`: clears nearby bullets with `F` and can be recharged.
- `EnemyPatrol`: moves an enemy horizontally between patrol limits.
- `EnemyShooter`: fires toward the player on a timer.
- `EnemyDeath`: awards score and destroys a defeated enemy.
- `Star`: applies its configured upgrade when collected.
- `HealthUI`: displays current and maximum player health.
- `ScoreUI`: displays score and star count.

## Deliberately Left Out

- No menus, save system, settings screen, or additional levels.
- `hurt.wav` is retained with the supplied assets but is not used by the provided scripts.
- The prototype uses simple supplied sprites and one test arena rather than final art or animation.

## Project Notes

- Unity version: `6000.6.7f1`.
- The only build scene is `Assets/Scenes/Game.unity`.
- The Windows build is in `Executable/`.
