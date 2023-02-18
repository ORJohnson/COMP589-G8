Painted 2D Terrain Tiles: Basic Set

2D art assets by David Baumgart

dgbaumgart@gmail.com
dgbaumgart.com

Last Update: v1.4 in September 2021

Description:
This is a set of lovingly hand-painted 2D terrain tiles suitable for an overworld, a strategy game, or boardgame-like visuals. 
- 14 biomes x 4 variations = 56 tiles total 
- painted at 256x384 pixels so that trees, hills, and mountains can overlap the tiles behind 
- can be used in a square grid or staggered faux-hex style grid 
- includes 47 alpha-background decor objects like trees, rocks, and grass
- includes an example scene which uses Unity's Tilemap feature

Terrain types included: 
- grassy plains 
- forest 
- ocean
- calm ocean
- desert dunes 
- marshland 
- snow-capped mountains
- hills
- dirt
- woodland
- highland
- shrubland
- base (generic empty tiles)
- void (stars)


Usage:
These square-format terrain tiles are intended to be drawn so that tiles in rows lower on the screen are drawn overtop of tiles in rows higher on the screen. This will ensure that trees, hills, and mountains will appear to stand out. You can make this a square grid or a staggered square grid to get a hex-grid like effect.

These are distributed at the same size that I painted them. If you want larger tiles, I recommend using one of those machine learning image upscalers that's keyed to scale painted pictures; the results are really nice.


Unity Tilemap example notes:
I've created the example scene using the Unity default of 100 pixels per unit which results in a grid with tiles 2.56x2.56 Unity units. If you change the import settings on the tile assets to 256 pixels per unit, you can change the in-scene grid and the palette grid to 1x1 Unity units.

If you find that the tiles are not overlapping correctly, make sure the render mode is set to "Chunk" and that any terrain sprites that appear within the same Tilemap are contained within a sprite atlas. Sometimes the tiles do not appear to overlap correctly in the editor scene but will display correctly in play mode.

If you're having any issues, feel free to email me and we'll figure it out.


Future:
If these prove popular, I may wish to do further tiles to add to this set, and further types of tilesets like this. I do have a passion for terrain tiles, after all. So drop me a line if you've got something particular in mind.


Changelog:
1.0: 
	- First Release (plains, forest, water, dunes, marsh, mountains)
1.1: 
	- Added dirt, base, void, hills tile types.
	- Added underground layers (dirt, water, void).
	- Slight colour adjustments
	- Fixed minor visual glitches
1.2:
	- Tile borders lightened
	- Dirt & generic edges improved
1.3:
	- Added woodland, shrubland, and highlands tile types
	- Added 34 decor objects 
	- Improved mountains
	- Fixed stray pixels, alpha holes

1.4 - 2021 September 09:
	- Added 13 decor objects (large mountains, ponds, tree clusters)
	- Redid example scene to support Unity's Tilemap feature
	- Slightly changed name of set for consistency with other new features (name was too long, unique portion was at the end)
	- Removed unneeded "tile sheets" (the Sprite Atlas feature makes them unnecessary)
	- Added "calm ocean" tiles for less noisy water