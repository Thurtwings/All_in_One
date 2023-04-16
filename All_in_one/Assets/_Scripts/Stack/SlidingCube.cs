using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThurtwingsGames.Utils;

namespace ThurtwingsGames.Stack
{
    public class SlidingCube : MonoBehaviour
    {
        public static SlidingCube CurrentCube { get; private set; }
        public static SlidingCube LastCube { get; private set; }
        public MoveDirection MoveDirection { get; set; }

        [SerializeField] float moveSpeed = 1f;

        private void OnEnable()
        {
            if (LastCube == null)
                LastCube = GameObject.Find("Base Cube").GetComponent<SlidingCube>();


            CurrentCube = this;
            transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y, LastCube.transform.localScale.z);
            moveSpeed += .01f;
        }
        
        internal void Stop()
        {
            moveSpeed = 0;
            float hangover = GetHangover();
            float max = MoveDirection == MoveDirection.Z ? LastCube.transform.localScale.z : LastCube.transform.localScale.x;

            if (Mathf.Abs(hangover) >= max)
            {
                LastCube = null;
                CurrentCube = null;
                ThurtwingsUtilities.Instance.ReloadCurrentScene();
            }

            float direction = hangover > 0 ? 1f : -1f;

            if (MoveDirection == MoveDirection.Z)
                SplitCubeOnZ(hangover, direction);
            else
                SplitCubeOnX(hangover, direction);

            LastCube = this;
        }

        private float GetHangover()
        {
            if(MoveDirection == MoveDirection.Z)
                return transform.position.z - LastCube.transform.position.z;
            else
                return transform.position.x - LastCube.transform.position.x;

        }

        private void SplitCubeOnX(float hangover, float direction)
        {
            
            
            float newXSize = LastCube.transform.localScale.x - Mathf.Abs(hangover);
            float fallingBlockSize = transform.localScale.x - newXSize;

            float newXPosition = LastCube.transform.position.x + (hangover / 2);
            transform.localScale = new Vector3(newXSize , transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

            float cubeEdge = transform.position.x + (newXSize / 2 * direction);
            float fallingBlockZPosition = cubeEdge + fallingBlockSize / 2 * direction;

            SpawnDropCube(fallingBlockZPosition, fallingBlockSize);
        }
        private void SplitCubeOnZ(float hangover, float direction)
        {
            
            
            float newZSize = LastCube.transform.localScale.z - Mathf.Abs(hangover);
            float fallingBlockSize = transform.localScale.z - newZSize;

            float newZPosition = LastCube.transform.position.z + (hangover / 2);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
            transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

            float cubeEdge = transform.position.z + (newZSize / 2 * direction);
            float fallingBlockZPosition = cubeEdge + fallingBlockSize / 2 * direction;

            SpawnDropCube(fallingBlockZPosition, fallingBlockSize);


        }

        private void SpawnDropCube(float fallingBlockPosition, float fallingBlockSize)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if(MoveDirection == MoveDirection.Z)
            {
                cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
                cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockPosition);
            }
            else
            {
                cube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z );
                cube.transform.position = new Vector3(fallingBlockPosition, transform.position.y, transform.position.z );
            }
            
            cube.AddComponent<Rigidbody>();

            Destroy(cube.gameObject, 2f);
        }

        

        void Update()
        {
            if(MoveDirection == MoveDirection.Z)
                transform.position += moveSpeed * Time.deltaTime * transform.forward;
            else
                transform.position += moveSpeed * Time.deltaTime * transform.right;

          
        }
    }
}
