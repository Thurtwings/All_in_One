using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThurtwingsGames.Fruits
{
    public class Bomb : MonoBehaviour
    {
        Rigidbody rb;
        [SerializeField] ParticleSystem BombExplosion;
        public GameObject ticking;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Blade blade = other.GetComponent<Blade>();
                blade.enabled = false;
                StartCoroutine(CountdownBeforeExplosion());
                rb.isKinematic = true;
            }
        }

        IEnumerator CountdownBeforeExplosion(float duration = 2f)
        {
            FindObjectOfType<FruitNinjaManager>().GameOver();
            yield return new WaitForSeconds(duration);
            //play cool explosion
            ticking.SetActive(false);
            BombExplosion.Play();
            //Start CameraShaker
            StartCoroutine(FindObjectOfType<ThurtwingsGames.Utils.CameraShake>().CameraShaker());
            
            

        }


    }
}
