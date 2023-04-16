using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThurtwingsGames
{
    public class Puzzle_Main : MonoBehaviour
    {
        [SerializeField] internal int x_tiles_amount = 3;


        // Start is called before the first frame update
        void Start()
        {
            CreateNewPuzzle();
        }

        private void CreateNewPuzzle()
        {
            for (int i = 0; i < x_tiles_amount; i++)
            {
                for (int j = 0; j < x_tiles_amount; j++)
                {
                    GameObject newTile = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    newTile.transform.position = -Vector2.one * (x_tiles_amount - 1) * .5f + new Vector2(j, i);
                    newTile.transform.parent = this.transform;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
