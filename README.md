Nemanja Petrovic
Blue Sky Interview Documentation
Inventory system

Primary purpose of the test was to implement a functional inventory system that includes selling, buying and equipping items, namely outfits.
The system was designed to be modular thus allowing implementations into future systems (with some changes).

The style chosen out of the 2 suggested (provided ones) was a pixel art style, some of the art used was provided by Blue Galaxy, some of it acquired (free) from Freepic or OpenGameArt .
Initially I identified the key functionalities – item management, item equipping, item buying/selling. The inventory system consists of three main parts:
-The player and their inventory.
-The NPC and their inventory.
-Item management.

I used scriptable objects for the outfits/items with some of their functionality currently unused. Scriptable objects provide a convenient way to store and access data, they allow simplified state saving with future implementations, easily modifiable parameters consequently making them both developer and designer friendly.
I opted for the singleton pattern for the 2 managers:
Game Manager and Inventory manager.

Due to them being global they reduced the number of references required, on top of being easy to use allowing me to speed up the development process.
As an alternative to the Singleton Pattern I would have opted for Event Systems to not rely on the global states.

Reused Code: “Audio Manager” and “Sound Manager” scripts, I use them often in other projects due to their plug & play capabilities.

Personal Assessment: Keeping in mind the time constraint and a portion of the time being spent on systems (enemy behavior, friendly/hostile NPC etc.) that were rendered unnecessary for the goal of the test - the functionality is there, and its modularity allows for future reiterations for larger game scopes. There are a few things I’d change – code redundancy being the main one, as well as improving the inventory system both from programming and visual perspective. All in all there’s plenty of space for improvement.




