# Procedural Generation In Unity

This game is based around learning how to procedurally generate various types of levels in unity and to learn how to use a MYSQL Database to store player data.

## Database

A database was created on 000webhost.com using PHPMyAdmin the database consisted of
 - id - Unique to all players
 - username - Unique to all players
 - salt - Used to hash the password of the user.
 - hash - Hash used to store the password so it is not stored in plain text
 - score - Players Score when they complete the game.

![Screenshot](https://github.com/MrSpeedy68/Unity-Procedural-Generation/blob/main/Assets/Screenshots/DB.png?raw=true)

## Log in screen

When the user starts the game up they are greeted with a screen to register or to login.
A newly registered user will be given a new id they need to write in a username that doesn't already exist in the database and then their information is saved onto the MYSQL database to allow them to login.

![Screenshot](https://github.com/MrSpeedy68/Unity-Procedural-Generation/blob/main/Assets/Screenshots/Register.png?raw=true)
![Screenshot](https://github.com/MrSpeedy68/Unity-Procedural-Generation/blob/main/Assets/Screenshots/Login.png?raw=true)

Once the player is signed in then they can proceede to start a new game or continue from where they have left off which is done by saving the users level and temporary score in Unity's PlayerPrefs.

![Screenshot](https://github.com/MrSpeedy68/Unity-Procedural-Generation/blob/main/Assets/Screenshots/PlayerLoggedin.png?raw=true)

## Level 1 - Procedural Maze Generation

The first level is a demonstration of the **Recursive Backtracking Algorithm** where a procedural maze is created.

![Screenshot](https://github.com/MrSpeedy68/Unity-Procedural-Generation/blob/main/Assets/Screenshots/Maze1.png?raw=true)

The level consists of 4 randomly placed pickups and 4 AI which wander randomly and if they detect the player through sight, hearing will chase the player. If the player collides with these AI the level will then restart. The player moves onto the next level when all pickupable objects are gathered.

![Screenshot](https://github.com/MrSpeedy68/Unity-Procedural-Generation/blob/main/Assets/Screenshots/MazeFPS.png?raw=true)

## Level 2 - Procedurally Generated Terrain Mesh

In this level a procedurally generated mesh was created using **Perlin Noise** this script consists of various parameters that allow you to fine tune the terrain to make it more smoother or more mountainous.

![Screenshot](https://user-images.githubusercontent.com/47033681/164525082-b898f45b-6ae2-4c2d-a47d-715e2156442a.png)

![Screenshot](https://github.com/MrSpeedy68/Unity-Procedural-Generation/blob/main/Assets/Screenshots/Terrain1.png?raw=true)

The players objective is once again to find all the pickupable items in the level to proceede onto the final level.

![Screenshot](https://github.com/MrSpeedy68/Unity-Procedural-Generation/blob/main/Assets/Screenshots/TerrainFPS.png?raw=true)

## Level 3 - Procedurally Generated Solar System From XML

In this level a procedurally generated solar system was created by reading in data from an XML file that had the following layout.

``<planets>
	
	<planet name = "Earth" diameter = "1" distancetoSun = "1" rotationPeriod = "1" orbitalVelocity = "1" color = "#1fbce8">
	</planet>

	<planet name = "Mercury" diameter = "0.3" distancetoSun = "0.4" rotationPeriod = "58.8" orbitalVelocity = "1.59" color = "#eda136"> </planet>

	<planet name = "Venus" diameter = "0.95" distancetoSun = "0.72" rotationPeriod = "-244.0" orbitalVelocity = "1.18" color = "#f0d5ad"> 
	</planet>

	<planet name = "Mars" diameter = "0.53" distancetoSun = "1.52" rotationPeriod = "1.03" orbitalVelocity = "1.88" color = "#d33d25"> 
	</planet>

	<planet name = "Jupiter" diameter = "11.21" distancetoSun = "5.20" rotationPeriod = "0.42" orbitalVelocity = "0.44" color = "#dfaf68"> 
	</planet>
	
	<planet name = "Saturn" diameter = "9.45" distancetoSun = "9.58" rotationPeriod = "0.45" orbitalVelocity = "0.33" color = "#fef1bb"> 
	</planet>

	<planet name = "Uranus" diameter = "4.01" distancetoSun = "19.20" rotationPeriod = "-0.72" orbitalVelocity = "0.23" color = "#9adcd2"> 
	</planet>
	
	<planet name = "Neptune" diameter = "3.88" distancetoSun = "30.05" rotationPeriod = "0.67" orbitalVelocity = "0.18" color = "#045791"> 
	</planet>
	
	<planet name = "Pluto" diameter = "0.19" distancetoSun = "39.48" rotationPeriod = "6.41" orbitalVelocity = "0.16" color = "#a6915c"> 
	</planet>


</planets>
``

This file was read in and the data was parsed to instatiate new planets which followed the paramaters of the values with their diameter, distance to the sun, their rotation, orbital velocity around the sun and their color.

![Screenshot](https://github.com/MrSpeedy68/Unity-Procedural-Generation/blob/main/Assets/Screenshots/Space1.png?raw=true)

The objective for the player is to collect all the satelite objects rotating around planets to complete the level.

![Screenshot](https://github.com/MrSpeedy68/Unity-Procedural-Generation/blob/main/Assets/Screenshots/SpaceFPS.png?raw=true)

Once the user completes the game their new score is saved to the database showing their new score and old previous score.


 
