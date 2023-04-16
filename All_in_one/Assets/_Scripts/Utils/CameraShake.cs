using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThurtwingsGames.Utils
{
    public class CameraShake : MonoBehaviour
    {
        public AnimationCurve animationCurve;
        public IEnumerator CameraShaker(float duration = 1f)
        {
            Vector3 startingPosition = transform.position;
            float elapsedTime = 0f;
            while(elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float strenght = animationCurve.Evaluate(elapsedTime / duration);
                transform.position = startingPosition + Random.insideUnitSphere * strenght;
                yield return null;
            }

            transform.position = startingPosition;
        }

        
    }
}
