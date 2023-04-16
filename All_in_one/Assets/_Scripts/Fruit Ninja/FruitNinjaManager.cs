using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using TMPro;
using ThurtwingsGames.Utils;


namespace ThurtwingsGames.Fruits
{
    public class FruitNinjaManager : MonoBehaviour
    {
        public TMP_Text scoreText;
        public TMP_Text highScoreText;
        public TMP_Text getReadyText;
        public Image fadeImage;
        public int score;
        int highScore;
        Animator animator;
        private string path;

        
        void Start()
        {
            NewGame();
            animator = GetComponent<Animator>();
            path = Application.persistentDataPath + "/fruitPlayer.dat";
            Load();
            

        }
        
        void NewGame()
        {
            score = 0;
            scoreText.text = score.ToString();
            highScore = 0;
            highScoreText.text = highScore.ToString();
            
        }

        public void IncreaseScore()
        {
            score++;
            scoreText.text = score.ToString();
        }

        public void GameOver()
        {
            FindObjectOfType<FruitsSpawner>().enabled = false;
            animator.Play("FruitNinja_GameOver");
            Save();
        }
        private void OnApplicationQuit()
        {
            Save();
        }
        #region Save Load
        private void Save()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(path);
            if (highScore <= score)
            {
                highScore = score;
            }
            bf.Serialize(file, highScore);
            file.Close();
        }

        private void Load()
        {
            if (File.Exists(path))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(path, FileMode.Open);
                highScore = (int)bf.Deserialize(file);
                highScoreText.text = highScore.ToString();
                file.Close();
            }
            else
                NewGame();
        }
        #endregion

    }
}
