For the Performance aware space shooter I built upon what was started in the lectures. I decided to remove the player's movement in the Y axis as I felt it wasn't needed for the game I was making, opting for a space invaders type of movement. I also clamped the x movement range between -8 and 8 to prevent the player from moving outside of the camera view.

For the enemies I simply made them move in the direction of the player. When they collide with the player the game quits. I was going to make the scene reload but I struggled with getting it working with DOTS so I ended up doing this instead.
The enemy spawn system works by spawning the enemy prefab above the screen at a set interval. I made the spawn position on the x axis be anywhere between -8 and 8.
I also made the spawn rate increase over time using a timer.

The collision detection works using the Unity Physics package and it detects collision based on Layers. I initially wanted to make the laser disappear upon contact but its collision seems to still be present at the point of contact and I wasn't able to figure out a solution.