using TMPro;
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;



namespace ThurtwingsGames.Utils
{
    /// <summary>
    /// Require an instance in the scene 
    /// </summary>
    public class ThurtwingsUtilities : MonoBehaviour
    {

        public static ThurtwingsUtilities Instance;

        private void OnEnable()
        {
            Instance = this;
        }



        #region Screen Related
        public void SetScreenOrientation(bool isPortrait, bool isLandscape, bool isAuto)
        {
            if (isPortrait) Screen.orientation = ScreenOrientation.Portrait;
            else if (isLandscape) Screen.orientation = ScreenOrientation.LandscapeRight;
            else if (isAuto) Screen.orientation = ScreenOrientation.AutoRotation;
        }

        #endregion

        #region Scene Management
        public void ReloadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void LoadSceneByName(string name, LoadSceneMode sceneMode)
        {
            SceneManager.LoadScene(name, sceneMode);
        }
        public void LoadSceneByBuildIndex(int buildIndex, LoadSceneMode sceneMode)
        {
            SceneManager.LoadScene(buildIndex, sceneMode);
        }
        public void LoadasynchronousSceneByBuildIndex(int buildIndex, LoadSceneMode sceneMode)
        {
            SceneManager.LoadSceneAsync(buildIndex, sceneMode);
        }
        public void LoadasynchronousSceneByName(string name, LoadSceneMode sceneMode)
        {
            SceneManager.LoadSceneAsync(name, sceneMode);
        }
        #endregion

        #region Camera Related
        #region Camera Shake w/o Cinemachine
        /// <summary>
        /// Duration is fine with 1 sec
        /// animation curve is used to blend in and out with ease
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="animationCurve"></param>
        /// <returns></returns>
        public IEnumerator CameraShakerWithCurve(float duration, AnimationCurve animationCurve)
        {
            Vector3 startingPosition = transform.position;
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float strenght = animationCurve.Evaluate(elapsedTime / duration);
                transform.position = startingPosition + UnityEngine.Random.insideUnitSphere * strenght;
                yield return null;
            }

            transform.position = startingPosition;
        }

        /// <summary>
        /// Duration is fine with 1 sec. Hard shake
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        public IEnumerator CameraShakerWithAnimationCurve(float duration = 1f)
        {
            Vector3 startingPosition = transform.position;
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                transform.position = startingPosition + UnityEngine.Random.insideUnitSphere;
                yield return null;
            }

            transform.position = startingPosition;
        }
        #endregion


        #endregion

        #region Button Related
        /// <summary>
        /// Opens the specified link parameter in a new browser tab / window 
        /// Usefull for credits, or website links
        /// </summary>
        /// <param name="link"></param>
        public void OpenAppLink(string link)
        {
            Application.OpenURL(link);
        }


        #endregion

        #region Text Related
        /// <summary>
        /// Has to be called in Update
        /// </summary>


        Mesh mesh;

        Vector3[] vertices;
        public void WobbleTextWaveEffect(TMP_Text textComponent)
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

        Vector2 Wobble(float time, float sinValue, float cosValue)
        {
            return new Vector2(Mathf.Sin(time * sinValue), Mathf.Cos(time * cosValue));
        }
        public void WobblePerVertexEffect(TMP_Text textComponent, float sinValue, float cosValue)
        {
            textComponent.ForceMeshUpdate();


            mesh = textComponent.mesh;
            vertices = mesh.vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 offset = Wobble(Time.time + i, sinValue, cosValue);
                vertices[i] = vertices[i] + offset;
            }

            mesh.vertices = vertices;
            textComponent.canvasRenderer.SetMesh(mesh);

        }

        #endregion


    }
}
