using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThurtwingsGame.PuzzleTile
{
    public static class ImageSlicer
    {
        public static Texture2D[,] GetSlices(Texture2D image, int tilesPerLine)
        {
            int imageSize = Mathf.Min(image.width, image.height);
            int tileSize = imageSize / tilesPerLine;

            Texture2D[,] blocks = new Texture2D[tilesPerLine, tilesPerLine];

            for (int y = 0; y < tilesPerLine; y++)
            {
                for (int x = 0; x < tilesPerLine; x++)
                {
                    Texture2D tile = new Texture2D(tileSize, tileSize);
                    tile.wrapMode = TextureWrapMode.Clamp;
                    tile.SetPixels(image.GetPixels(x * tileSize, y * tileSize, tileSize, tileSize));
                    tile.Apply();
                    blocks[x, y] = tile;
                }
            }

            return blocks;
        }
    }
}
