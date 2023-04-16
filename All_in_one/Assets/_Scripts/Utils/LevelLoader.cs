using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ThurtwingsGames.Utils
{
    public class LevelLoader : MonoBehaviour
    {
        public static LevelLoader Instance;


        private void OnEnable()
        {
            Instance = this;
        }

        public void ReloadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}
