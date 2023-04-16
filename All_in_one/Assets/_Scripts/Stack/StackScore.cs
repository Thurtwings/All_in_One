using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

namespace ThurtwingsGames.Stack
{
    public class StackScore : MonoBehaviour
    {
        private int score;
        private int highScore;
        [SerializeField] TMP_Text scoreText;
        [SerializeField] TMP_Text highScoreText;
        string path;


        // Start is called before the first frame update
        void Start()
        {
            //StackManager.OnCubeSpawned += StackManager_OnCubeSpawned;
            //NewGame();

            //path = Application.persistentDataPath + "/stackPlayer.dat";

            //Load();
        }

        private void NewGame()
        {
            score = 0;
            scoreText.text = score.ToString();
            highScore = 0;
            highScoreText.text = highScore.ToString();
        }

        private void OnDestroy()
        {
            StackManager.OnCubeSpawned -= StackManager_OnCubeSpawned;
        }
        private void StackManager_OnCubeSpawned()
        {
            score++;
            scoreText.text = score.ToString();
        }

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
    }
    //private bool PlaceTile()
    //{
    //    Transform t = theStack[stackIndex].transform;

    //    if (isMovingOnX)
    //    {
    //        float deltaX = lastTilePosition.x - t.position.x;
    //        if (Mathf.Abs(deltaX) > ERROR_MARGIN)
    //        {
    //            //CUT THE TILE
    //            combo = 0;
    //            stackBounds.x -= Mathf.Abs(deltaX);
    //            if (stackBounds.x <= 0)
    //                return false;

    //            float middle = lastTilePosition.x + t.localPosition.x / 2;
    //            t.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);
    /*CreateRubble
    (
        new Vector3((t.position.x > 0)
            ? t.position.x + (t.localScale.x / 2)
            : t.position.x - (t.localScale.x / 2)
            , t.position.y
            , t.position.z),
        new Vector3(Mathf.Abs(deltaX), 1, t.localScale.y)
    );*/
    //            t.localPosition = new Vector3(middle - (lastTilePosition.x / 2), scoreCount, lastTilePosition.z);
    //        }
    //        else
    //        {
    //            if (combo > COMBO_START_GAIN)
    //            {
    //                stackBounds.x += STACK_BOUNDS_GAIN;
    //                if (stackBounds.x > BOUNDS_SIZE)
    //                    stackBounds.x = BOUNDS_SIZE;

    //                float middle = lastTilePosition.x + t.localPosition.x / 2;
    //                t.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);
    //                t.localPosition = new Vector3(middle - (lastTilePosition.x / 2), scoreCount, lastTilePosition.y);
    //            }

    //            combo++;
    //            t.localPosition = new Vector3(lastTilePosition.x, scoreCount, lastTilePosition.z);
    //        }
    //    }
    //    else
    //    {
    //        float deltaZ = lastTilePosition.z - t.position.z;
    //        if (Mathf.Abs(deltaZ) > ERROR_MARGIN)
    //        {
    //            //CUT THE TILE
    //            combo = 0;
    //            stackBounds.y -= Mathf.Abs(deltaZ);
    //            if (stackBounds.y <= 0)
    //                return false;

    //            float middle = lastTilePosition.z + t.localPosition.z / 2;
    //            t.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);
    //            CreateRubble
    //            (
    //                new Vector3(t.position.x
    //                    , t.position.y
    //                    , (t.position.z > 0)
    //                    ? t.position.z + (t.localScale.z / 2)
    //                    : t.position.z - (t.localScale.z / 2)),
    //                new Vector3(Mathf.Abs(deltaZ), 1, t.localScale.z)
    //            );
    //            t.localPosition = new Vector3(lastTilePosition.x, scoreCount, middle - (lastTilePosition.z / 2));
    //        }
    //        else
    //        {
    //            if (combo > COMBO_START_GAIN)
    //            {
    //                if (stackBounds.y > BOUNDS_SIZE)
    //                    stackBounds.y = BOUNDS_SIZE;

    //                stackBounds.y += STACK_BOUNDS_GAIN;
    //                float middle = lastTilePosition.z + t.localPosition.z / 2;
    //                t.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);
    //                t.localPosition = new Vector3(lastTilePosition.x, scoreCount, middle - (lastTilePosition.z / 2));
    //            }
    //            combo++;
    //            t.localPosition = new Vector3(lastTilePosition.x, scoreCount, lastTilePosition.z);
    //        }
    //    }

    //    secondaryPosition = (isMovingOnX)
    //        ? t.localPosition.x
    //        : t.localPosition.z;
    //    isMovingOnX = !isMovingOnX;

    //    return true;
    //}
}
