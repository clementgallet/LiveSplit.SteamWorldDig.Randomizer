# LiveSplit.SteamWorldDig.Randomizer
Randomizer for the game SteamWorld Dig

## What does the Randomizer do?
It randomizes all upgrades on upgrade podiums and all caves. Caves which are accessed from another cave, like `archaea_cave_generator_1` from `archaea_cave_run`, are not randomized.

It also ensures that the resulting layout is possible to finish. There are two possible difficulties: `Casual` and `Speedrunner`. In `Casual` difficulty, you won't have to:

- Perform difficult wall jumps
- Use rocket jumps from dynamite or other explosions
- Enter Vectron without Static Dash or Fall Dampeners
- Dig the Oldworld rock layer with dynamite

## Setting up the Randomizer in LiveSplit
- Go to the [releases](https://github.com/clementgallet/LiveSplit.SteamWorldDig.Randomizer/releases) section in this repository.
- Download the latest LiveSplit.SteamWorldDig.Randomizer.dll
- Place the LiveSplit.SteamWorldDig.Randomizer.dll inside the LiveSplit/Components folder
- Launch the game
- Open LiveSplit, edit your layout, add Control -> SteamWorld Dig Randomizer
- In the settings, choose a difficulty or input a custom seed
- Press `Randomize` and have fun! You can now start a new game, and close LiveSplit if you want