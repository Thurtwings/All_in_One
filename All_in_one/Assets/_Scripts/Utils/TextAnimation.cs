using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ThurtwingsGames.Utils
{
    public class TextAnimation : MonoBehaviour
    {
        
        public TMP_Text textComponent;
        public bool isGenerating;
        public string generatingText;
        public string gameText;
        private void Update()
        {
            
            WobbleText();
        }

        public void WobbleText()
        {
            textComponent.ForceMeshUpdate();
            var textInfo = textComponent.textInfo;

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                var charInfo = textInfo.characterInfo[i];

                if (!charInfo.isVisible)
                    continue;

                var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

                for (int j = 0; j < 4; j++)
                {
                    var origin = verts[charInfo.vertexIndex + j];
                    verts[charInfo.vertexIndex + j] = origin + new Vector3(0, Mathf.Sin(Time.time * 2f + origin.x * .01f) * 10f, 0);
                }


            }

            for (int i = 0; i < textInfo.meshInfo.Length; i++)
            {
                var meshInfo = textInfo.meshInfo[i];
                meshInfo.mesh.vertices = meshInfo.vertices;
                textComponent.UpdateGeometry(meshInfo.mesh, i);
            }
        }
    }
}
