using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



namespace ThurtwingsGame.PuzzleTile
{
    public enum PuzzleState { Solved, Shuffling, InProgress }
    public class Puzzle : MonoBehaviour
    {
        
        PuzzleState puzzleState;

        public Texture2D image;
        public int tilesPerLine = 4;
        public int shuffleLenght = 12;
        public float defaultMoveDuration = .2f;
        public float shuffletMoveDuration = .1f;
        public Button shuffleButton;
        Tile emptyTile;
        Tile[,] tiles;
        Queue<Tile> inputs;
        bool tileIsMoving;
        int shuffleMoveRemaining = 0;
        Vector2Int previousShuffleOffset;
        public List<Vector2Int> shuffleMoves;
        void Start()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                image = Resources.Load("Android.png") as Texture2D;
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                image = Resources.Load("IOS.png") as Texture2D;
            }
            else
            {
                image = Resources.Load("Unity_Logo.png") as Texture2D;
            }
            CreatePuzzle(tilesPerLine);
            shuffleMoves = new List<Vector2Int>();
            StartShuffle();

            
        }

        public void StartShuffle()
        {
            puzzleState = PuzzleState.Shuffling;
            shuffleMoveRemaining = shuffleLenght;
            emptyTile.gameObject.SetActive(false);
            shuffleButton.gameObject.SetActive(false);
            MakeNextShuffleMove();
        }
        
        void CreatePuzzle(int tilesPerLine)
        {
            tiles = new Tile[tilesPerLine, tilesPerLine];
            
            Texture2D[,] imageSlices = ImageSlicer.GetSlices(image, tilesPerLine);
            for (int y = 0; y < tilesPerLine; y++)
            {
                for (int x = 0; x < tilesPerLine; x++)
                {
                    GameObject tileObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    tileObject.transform.position = -Vector2.one * (tilesPerLine - 1) * .5f + new Vector2(x, y);
                    tileObject.transform.parent = transform;

                    Tile tile = tileObject.AddComponent<Tile>();
                    tile.OnTilePressed += PlayerMoveTileInput;
                    tile.OnFinishMoving += OnTileFinishedMoving;
                    tile.Init(new Vector2Int(x, y), imageSlices[x, y]);
                    tiles[x, y] = tile;

                    if (y == 1 && x == tilesPerLine - 2)
                    {
                        emptyTile = tile;
                    }
                }
            }

            Camera.main.orthographicSize = tilesPerLine * 1.25f;
            inputs = new Queue<Tile>();
        }

        void PlayerMoveTileInput(Tile tileToMove)
        {
            if(puzzleState == PuzzleState.InProgress)
            {
                inputs.Enqueue(tileToMove);
                MakeNextPlayerMove();
            }
            
        }
        void MakeNextPlayerMove()
        {
            while (inputs.Count > 0 && !tileIsMoving)
            {
                MoveTile(inputs.Dequeue(), defaultMoveDuration);
            }
        }
        void MoveTile(Tile tileToMove, float duration)
        {
            if ((tileToMove.coord - emptyTile.coord).sqrMagnitude == 1)
            {
                tiles[tileToMove.coord.x, tileToMove.coord.y] = emptyTile;
                tiles[emptyTile.coord.x, emptyTile.coord.y] = tileToMove;


                Vector2Int targetCoord = emptyTile.coord;
                emptyTile.coord = tileToMove.coord;
                tileToMove.coord = targetCoord;

                Vector2 targetPosition = emptyTile.transform.position;
                emptyTile.transform.position = tileToMove.transform.position;
                tileToMove.MoveTo(targetPosition, duration);
                tileIsMoving = true;
            }
        }

        void OnTileFinishedMoving()
        {
            tileIsMoving = false;
            CheckIfSolved();
            if (puzzleState == PuzzleState.InProgress)
            {
                MakeNextPlayerMove();
            }
            else if (puzzleState == PuzzleState.Shuffling)
            { 
                if (shuffleMoveRemaining > 0)
                {
                    MakeNextShuffleMove();

                }
                else
                {
                    puzzleState = PuzzleState.InProgress;
                }
            }
        }

        void MakeNextShuffleMove()
        {
            Vector2Int[] offsets = { new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0,-1) };
            int randomIndex = Random.Range(0, offsets.Length);
            for (int i = 0; i < offsets.Length; i++)
            {
                Vector2Int offset = offsets[(randomIndex + i) % offsets.Length];
                shuffleMoves.Add(offset);
                if(offset != previousShuffleOffset * -1)
                {
                    Vector2Int moveTileCoord = emptyTile.coord + offset;

                    if (moveTileCoord.x >= 0 && moveTileCoord.x < tilesPerLine && moveTileCoord.y >= 0 && moveTileCoord.y < tilesPerLine)
                    {
                        MoveTile(tiles[moveTileCoord.x, moveTileCoord.y], shuffletMoveDuration);
                        shuffleMoveRemaining--;
                        previousShuffleOffset = offset;
                        break;
                    }
                }
                
            }
            
        }

        

        void CheckIfSolved()
        {
            foreach (Tile tile in tiles)
            {
                if (!tile.IsAtStartingCoord())
                {
                    return;
                }
            }

            puzzleState = PuzzleState.Solved;
            emptyTile.gameObject.SetActive(true);
            emptyTile.transform.GetComponent<Renderer>().material.color = new Color(255, 255, 255, Mathf.Lerp(0, 255, Time.deltaTime*2));
            shuffleButton.gameObject.SetActive(true);
        }
    }
}
