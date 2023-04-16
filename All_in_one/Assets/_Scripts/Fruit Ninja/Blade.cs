using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThurtwingsGames.Fruits
{
    public class Blade : MonoBehaviour
    {
        Collider bladeCollider;
        bool isSlicing;
        Camera mainCam;

        TrailRenderer bladeTrail;
        internal Vector3 direction { get; private set; }
        public float sliceForce = 5;
        [SerializeField] float minSliceVelocity = .01f;
        private void Awake()
        {
            bladeCollider = GetComponent<Collider>();
            mainCam = Camera.main;
            bladeTrail = GetComponentInChildren<TrailRenderer>();
        }
        private void Update()
        {

            if (Input.GetMouseButtonDown(0))
                StartSlicing();
            else if (Input.GetMouseButtonUp(0))
                StopSlicing();
            else if (isSlicing)
                ContinueSlicing();

            //if (Input.touchCount == 1) // user is touching the screen with a single touch
            //{
            //    Touch touch = Input.GetTouch(0); // cache the touch
            //    if (touch.phase == TouchPhase.Began) //check for the first touch
            //    {
            //        StartSlicing();
            //    }
            //    else if (touch.phase == TouchPhase.Ended)
            //    {
            //        StopSlicing();
            //    }
            //    else if (isSlicing)
            //    {
            //        ContinueSlicing();
            //    }
            //}
        }

        private void OnEnable()
        {
            StopSlicing();
        }
        private void OnDisable()
        {
            StopSlicing();
        }

        private void StartSlicing()
        {
            Vector3 newPosition = mainCam.ScreenToWorldPoint(Input.mousePosition);

            newPosition.z = 0f;
            transform.position = newPosition;
            isSlicing = true;
            bladeCollider.enabled = true;
            bladeTrail.enabled = true;
            bladeTrail.Clear();
        }
        private void StopSlicing()
        {
            isSlicing = false;
            bladeCollider.enabled = false;
            bladeTrail.enabled = false;
        }
        private void ContinueSlicing()
        {
            Vector3 newPosition = mainCam.ScreenToWorldPoint(Input.mousePosition);

            newPosition.z = 0f;

            direction = newPosition - transform.position;

            float velocity = direction.magnitude / Time.deltaTime;
            bladeCollider.enabled = velocity > minSliceVelocity;

            transform.position = newPosition;
        }
    }
}
