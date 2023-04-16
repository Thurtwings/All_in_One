using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThurtwingsGames.Stack
{
    public class StackCubeSpawner : MonoBehaviour
    {
        [SerializeField] SlidingCube cubePrefab;
        [SerializeField] MoveDirection moveDirection;
        int level;
        public void SpawnCube()
        {

            var cube = Instantiate(cubePrefab);

            if (SlidingCube.LastCube != null && SlidingCube.LastCube.gameObject != GameObject.Find("Base Cube"))
            {

                float x = moveDirection == MoveDirection.X ? transform.position.x : SlidingCube.LastCube.transform.position.x;
                float z = moveDirection == MoveDirection.Z ? transform.position.z : SlidingCube.LastCube.transform.position.z;


                cube.transform.position = new Vector3(x, SlidingCube.LastCube.transform.position.y + cubePrefab.transform.localScale.y, z);

            }
            else
            {
                cube.transform.position = transform.position;
            }

            cube.MoveDirection = moveDirection;
            cube.name = level + "";
            cube.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.HSVToRGB(level / 100f % 1f, 1f, 1f));
            level+=3;
        }


    }
}
