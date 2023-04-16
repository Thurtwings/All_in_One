using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThurtwingsGames.Fruits
{
    public class Fruit : MonoBehaviour
    {
        public GameObject whole;
        public GameObject sliced;
        [SerializeField]ParticleSystem juice; 
        Rigidbody fruitRb;
        Collider fruitCollider;
        private void Awake()
        {
            fruitRb = GetComponent<Rigidbody>();
            fruitCollider = GetComponent<Collider>();
        }
        void Slice(Vector3 direction, Vector3 pos, float force)
        {
            FindObjectOfType<FruitNinjaManager>().IncreaseScore();
            whole.SetActive(false);
            sliced.SetActive(true);
            
            fruitCollider.enabled = false;
            juice.Play();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            sliced.transform.rotation = Quaternion.Euler(0, 0, angle);

            Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody slice in slices)
            {
                slice.velocity = fruitRb.velocity;
                slice.AddForceAtPosition(direction * force, pos, ForceMode.Impulse);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Blade blade = other.GetComponent<Blade>();
                Slice(blade.direction, blade.transform.position, blade.sliceForce);
            }
        }



        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
