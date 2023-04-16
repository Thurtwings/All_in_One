using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ThurtwingsGame.PuzzleTile
{
    public class Tile : MonoBehaviour
    {
        public event Action<Tile> OnTilePressed;
        public event Action OnFinishMoving;
        public Vector2Int coord;
        public Vector2Int startingCoord;
        
        private void OnMouseDown()
        {
            OnTilePressed?.Invoke(this);
        }

        public void MoveTo(Vector2 targetPos, float duration)
        {
            StartCoroutine(SlidingAnimation(targetPos, duration));
        }
        public void Init(Vector2Int startingCoord, Texture2D image)
        {
            this.startingCoord = startingCoord;
            coord = startingCoord;

            GetComponent<MeshRenderer>().material.shader = Shader.Find("Unlit/Texture");
            GetComponent<MeshRenderer>().material.mainTexture = image;
        }

        IEnumerator SlidingAnimation(Vector2 targetPos, float duration)
        {
            Vector2 initialPos = transform.position;
            float percent = 0;
            while (percent < 1)
            {
                percent += Time.deltaTime / duration;
                transform.position = Vector2.Lerp(initialPos, targetPos, percent);
                yield return null;
            }
            if(OnFinishMoving != null)
            {
                OnFinishMoving();
            }
        }

        public bool IsAtStartingCoord()
        {
            return coord == startingCoord;
        }

    }
}
