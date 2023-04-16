using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThurtwingsGames.Stack
{
    public class StackManager : MonoBehaviour
    {
        public static event Action OnCubeSpawned = delegate { };

        private StackCubeSpawner[] spawners;
        private int spawnerIndex;
        private StackCubeSpawner currentSpawner;

        private void Awake()
        {
            spawners = FindObjectsOfType<StackCubeSpawner>();


        }
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (SlidingCube.CurrentCube != null)
                    SlidingCube.CurrentCube.Stop();

                Camera.main.transform.position += new Vector3(0, .1f, 0);
                spawnerIndex = spawnerIndex == 0 ? 1 : 0;
                currentSpawner = spawners[spawnerIndex];
                currentSpawner.SpawnCube();
                OnCubeSpawned();
            }
        }


    }
}
