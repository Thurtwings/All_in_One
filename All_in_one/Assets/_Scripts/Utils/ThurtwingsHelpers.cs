using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using System.Text;

namespace ThurtwingsGames.Utils
{
    public static class ThurtwingsHelpers 
    {
        private static Camera _camera;
        /// <summary>
        /// Call camera.main is quite expensive, basically, 
        /// Unity is looping through all the objects in the 
        /// scene hierarchy and look for the gameobject with 
        /// the tag MainCamera.
        /// This allows us to get only once the reference for it, and it's always available 
        /// </summary>
        public static Camera Camera => _camera ??= Camera.main;
        //{
        //    get
        //    {
        //        if (_camera == null) _camera = Camera.main;
        //        return _camera;
        //    }
        //}



        private static readonly Dictionary<float, WaitForSeconds> WaitDictionnary = new Dictionary<float, WaitForSeconds>();
        /// <summary>
        /// Allows to cache the Coroutines WaitForSeconds()
        /// Everytime we create a new WaitForSeconds we allocate more garbages witch the GC has to come and clean up
        /// Implemented as "yield return ThurtwingsHelpers.GetWait(timeDesired);"
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static WaitForSeconds GetWait(float time)
        {
            if (WaitDictionnary.TryGetValue(time, out var wait)) return wait;

            WaitDictionnary[time] = new WaitForSeconds(time);
            return WaitDictionnary[time];
        }



        private static PointerEventData _eventDataCurrentPosition;
        private static List<RaycastResult> _raycastResults;
        /// <summary>
        /// Detect wether our mouse cursor nor our finger touch is over any UI element
        /// </summary>
        /// <returns></returns>
        public static bool IsOverUI()
        {
            _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            _raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(_eventDataCurrentPosition, _raycastResults); 
            return _raycastResults.Count > 0;
        }


        /// <summary>
        /// This method allows us to answer the question : 
        ///     How do we spawn a particles effect on the canvas, or a 3D object?
        ///     This also allows us to transform World coordinate to UI Coordinate
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Vector2 FindWorldPositionOfCanvasElement(RectTransform element)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(element, element.position, Camera, out var result);
            return result;
        }


        /// <summary>
        /// The name is self explanatory
        /// This is an extension method of the Transform class (transform.DeleteAllChildren)
        /// </summary>
        /// <param name="t"></param>
        public static void DeleteAllChildren(this Transform t)
        {
            foreach (Transform child in t)
            {
                UnityEngine.Object.Destroy(child.gameObject);
            }
        }

    }



    
}

namespace ThurtwingsGames.Utils.Save
{
    public enum Game { re_AA, re_ColorSwitch, re_CutTheRope, re_Stack, re_FlappyBird, re_FruitNinja, Hangman, re_LineRider, Mastermind, re_SpaceInvader, Memory, TicTacToe }
    public class SaveManagement : MonoBehaviour
    {
        public static SaveData SaveData;
        private BinaryFormatter _formatter = new BinaryFormatter();
        public static Game currentGameToSave;

        public static string SavePath => $"{Application.persistentDataPath}/save.{currentGameToSave.ToString()}";

        public void SaveMySettings(Game currentGameToSave)
        {
            var json = JsonUtility.ToJson(SaveData);

            using (var stream = new FileStream(SavePath, FileMode.Create))
            {
                _formatter.Serialize(stream, EncryptDecrypt(json));
            }
        }
        public void Load()
        {

            if (!File.Exists(SavePath))
            {
                SaveData = new SaveData()
                {
                    //Level = 1,
                    //Music = true,
                    //Sound = true,
                    //Vibrate = true,
                    //CurrentCharacter = CharacterType.Archer,
                    //UnlockedCharacters = new List<CharacterType>() { CharacterType.Archer }
                };

                SaveMySettings(currentGameToSave);
            }
            using (var stream = new FileStream(SavePath, FileMode.Open))
            {
                var data = (string)_formatter.Deserialize(stream);
                SaveData = JsonUtility.FromJson<SaveData>(EncryptDecrypt(data));
            }
        }

        //[MenuItem("Developer/Delete Saved Game")]
        public static void DeleteSave()
        {
            if (File.Exists(SavePath)) File.Delete(SavePath);
        }

        private static string EncryptDecrypt(string textToEncrypt)
        {
            var inSb = new StringBuilder(textToEncrypt);
            var outSb = new StringBuilder(textToEncrypt.Length);
            for (var i = 0; i < textToEncrypt.Length; i++)
            {
                var c = inSb[i];
                c = (char)(c ^ 129);
                outSb.Append(c);
            }
            return outSb.ToString();
        }

    }
    [Serializable]
    public class SaveData
    {


    }



    [Serializable]
    public class SaveDataFruitNinja : SaveData
    {
        //Fruit Ninja
        public int FruitNinja_HighScore = 0;

    }

    [Serializable]
    public class SaveDataMastermind : SaveData
    {
        //Mastermind


    }

    [Serializable]
    public class SaveDataStack : SaveData
    {
        //Stack
        public int Stack_HighScore = 0;
    }
}

