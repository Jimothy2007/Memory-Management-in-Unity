# Memory Management in Unity
This is my project for the GDT-110 "Exploring Memory Management in Game Engines" assignment. All required resources are listed below which includes a Word document with the essay portion of the assignment and the link to the GitHub repository.

- Link to the Word Document: https://1drv.ms/w/c/1bae3a119f73a11b/IQDSNVwRdQM-Rp-npGH6PTcSATtABrEotgrOlNtlOIXdbwQ?e=NsjEGW
- Link to the GitHub repository: https://github.com/Jimothy2007/Memory-Management-in-Unity

How to access the project:
- Open the repository link then clone it using GitHub desktop.
- In the Unity Hub, select add project from disk and select the folder of the GitHub repository.
- It should show up as a Unity project, then just open the project.

How to see if it works:
- Load the scene called "SampleScene", you should see two cubes in the world.
- Press play and you will see the bullets spawning and moving upwards.
- The bullets use object pooling to save on performance, to see this in action, you can open the scripts with visual studio.
- The cubes will also get a wood texture, this is done through a texture management system that assigns the texture to an object and reuses the same texture if another object wants to use it.
- You can also add your own texture by adding the desired texture to the Assets/Resources/Textures folder, then on one or both of the cubes change the Texture path under the TextureTestObject script to Textures/nameoftexture.
- When you press play, it should change the texture and output a message to the console.

How to check memory usage:
- This project has a package which allows you to check the memory usage of the project.
- Go to Window -> Analysis -> Memory Profiler
- With the memory profiler open, press play and then select "Capture New Snapshot"
- Unity will take a measurement of the memory usage while the program is running.
- You can change the rate at which the bullets spawn in the code then take another screenshot and compare if you want.

My memory findings:

I took four screenshots of my memory usage to monitor how well the object pooling and texture allocations work.
- When not running the program, my memory usage was 4.38 GB.
- When running the program with bullets spawning every 200 frames, my memory usage was also 4.38 GB.
- When running the program with bullets spawning every 100 frames, my memory usage was 5.16 GB.
- When running the program with bullets spawning every 50 frames, my memory usage was 5.02 GB.
This shows that the object pooling and texture allocations work great at minimizing memory usage because it barely increases the memory usage even after increasing the amount of bullets on screen.

What I learned:

Memory management is really important for games because it can improve the performance of your game by a lot. Things like reusing textures helps avoid memory fragmentation and allows the memory that would be used for another texture to be used for something that needs it more. Object pooling is also essential for any game that has a lot of projectiles because it avoids continuously allocating and deallocating memory for each projectile which will cause major drops in frame rate. Overall, the better you can manage memory, the better your game will run.
