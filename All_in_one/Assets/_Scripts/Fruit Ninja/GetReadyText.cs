using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThurtwingsGames.Utils;
using TMPro;
namespace ThurtwingsGames
{
    public class GetReadyText : MonoBehaviour
    {
        TMP_Text text;
        public float sinWaveStrenght = 2.5f;
        public float cosinWaveStrenght = 3.8f;
        private void Start()
        {
            text = GetComponent<TMP_Text>();
            StartCoroutine(GetReadyCoroutine());
        }
        void Update()
        {
            ThurtwingsUtilities.Instance.WobblePerVertexEffect(text, sinWaveStrenght, cosinWaveStrenght);
        }

        IEnumerator GetReadyCoroutine()
        {
            text.text = "Pret?";
            yield return new WaitForSeconds(1.0f);
            text.text = "3";
            yield return new WaitForSeconds(1.0f);
            text.text = "2";
            yield return new WaitForSeconds(1.0f);
            text.text = "1";
            yield return new WaitForSeconds(1.0f);
            text.text = "C'est Parti!";
            yield return new WaitForSeconds(2.0f);
            text.gameObject.SetActive(false);
        }
    }
}
