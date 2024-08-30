﻿# Downloads

You will find a compiled program ready to use in the [releases tab](https://github.com/traso56/ThreatVisualizer/releases), extract the zip file contents somewhere and click the executable file

# How to use

You will need .NET 8 to run the program and it's windows only, I don't really do UI and didn't have the time to learn a UI framework sadly.
But well the code is there if you want to adapt it to other platforms and it may get updated eventually

[Here is a video explanation on how to use](https://youtu.be/dBdf1QqLtE0)

# Some useful paths for files

1. **Vanilla threat config files:** C:\Program Files (x86)\Steam\steamapps\common\Rain World\RainWorld_Data\StreamingAssets\music\procedural
2. **Downpour threat config files:** C:\Program Files (x86)\Steam\steamapps\common\Rain World\RainWorld_Data\StreamingAssets\mods\moreslugcats\music\procedural
3. **Workshop mods threat config files:** C:\Program Files (x86)\Steam\steamapps\workshop\content\312520\\[modId]\music\procedural

## Sound files in vanilla and downpour are 0 bytes
This is because the actual sound files are in a bundle somewhere else.
You can use a program like `AssetStudioGUI` to open the file located in
C:\Program Files (x86)\Steam\steamapps\common\Rain World\RainWorld_Data\StreamingAssets\AssetBundles\ The file is named `music_procedural`

Or you can check this [repository](https://github.com/cookiecaker/Rain-World-Sounds/tree/main/Threat%20Music), Make sure to convert the song files to .ogg as the songs are .wav in that repository

Workshop sound files will work just fine