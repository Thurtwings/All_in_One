using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThurtwingsGames
{
    public class CubeController : MonoBehaviour
    {
        [SerializeField] private float _rollSpeed = 300;
        private bool _isMoving;

        private void Update()
        {
            if (_isMoving) return;

            if (Input.GetKey(KeyCode.Q)) 
                StartCoroutine(Roll(Vector3.left));
            else if (Input.GetKey(KeyCode.D)) 
                StartCoroutine(Roll(Vector3.right));
            else if (Input.GetKey(KeyCode.Z)) 
                StartCoroutine(Roll(Vector3.forward));
            else if (Input.GetKey(KeyCode.S)) 
                StartCoroutine(Roll(Vector3.back));


        }

        private IEnumerator Roll(Vector3 Direction)
        {
            _isMoving = true;
            float remainingAngle = 90f;
            Vector3 rotationCenter = transform.position + Direction / 2 + Vector3.down / 2;
            Vector3 rotationAxis = Vector3.Cross(Vector3.up, Direction);

            while(remainingAngle > 0)
            {
                float rotationAngle = Mathf.Min(Time.deltaTime * _rollSpeed, remainingAngle);
                transform.RotateAround(rotationCenter, rotationAxis, rotationAngle);
                remainingAngle -= rotationAngle;
                yield return null;
            }

            _isMoving = false;
        }

        
    }
}
