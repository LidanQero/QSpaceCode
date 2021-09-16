using System.Collections.Generic;
using System.IO;
using Master.QSpaceCode.Configs;
using Master.QSpaceCode.Game;
using UnityEditor;
using UnityEngine;

namespace Master.QSpaceCode.Static
{
    public static class LevelManager
    {
        private static LevelObjectsDB levelObjectsDB;
        private static Dictionary<string, LevelObject> dictionary;

        public static void SaveCurrentLevel(string levelName, LevelType levelType, LevelSector levelSector)
        {
            if (string.IsNullOrEmpty(levelName)) return;

            InitDictionary();
            
            var allObjects = Object.FindObjectsOfType<LevelObject>();
            var levelObjects = new Dictionary<LevelObject, LevelObject>();
            
            foreach (var levelObject in allObjects)
            {
                var prefab = PrefabUtility.GetCorrespondingObjectFromSource(levelObject);
                if (prefab) levelObjects.Add(levelObject, prefab);
            }

            var levelContainer = new LevelContainer
            {
                levelName = levelName,
                levelType = levelType,
                levelSector = levelSector,
                levelObjects = new LevelObjectInfo[levelObjects.Count]
            };

            var i = 0;
            foreach (var levelObject in levelObjects)
            {
                var transform = levelObject.Key.transform;
                var levelObjectInfo = new LevelObjectInfo
                {
                    prefabName = levelObject.Value.name,
                    position = transform.localPosition,
                    rotation = transform.localRotation,
                    scale = transform.localScale
                };
                levelContainer.levelObjects[i] = levelObjectInfo;
                i++;

                if (!dictionary.ContainsKey(levelObject.Value.name))
                {
                    dictionary.Add(levelObject.Value.name, levelObject.Value);
                }
            }
            
            var containerJson = JsonUtility.ToJson(levelContainer, true);
            var path = $"Assets/Master/Resources/Levels/{levelContainer.levelName}.json";
            File.WriteAllText(path, containerJson);
            
            SaveDictionary();
            
            AssetDatabase.Refresh();
        }

        public static List<LevelContainer> GetAllLevels()
        {
            InitDictionary();

            for (var i = 0; i < levelObjectsDB.prefabsStores.Count; i++)
            {
                var prefabsStore = levelObjectsDB.prefabsStores[i];
                dictionary.Add(prefabsStore.prefabName, prefabsStore.prefabObject);
            }

            var assets = Resources.LoadAll("Levels/");
            var textAssets = new List<string>();
            var levels = new List<LevelContainer>();

            for (var i = 0; i < assets.Length; i++)
            {
                var asset = assets[i];
                if (asset is TextAsset textAsset) textAssets.Add(textAsset.text);
            }

            for (var i = 0; i < textAssets.Count; i++)
            {
                var textAsset = textAssets[i];
                var container = JsonUtility.FromJson<LevelContainer>(textAsset);

                for (var l = 0; l < container.levelObjects.Length; l++)
                {
                    var levelObject = container.levelObjects[l];
                    levelObject.prefab = dictionary[levelObject.prefabName];
                    container.levelObjects[l] = levelObject;
                }

                levels.Add(container);
            }

            return levels;
        }

        public static void ClearLevel()
        {
            var allObjects = Object.FindObjectsOfType<LevelObject>();
            var levelObjects = new List<LevelObject>();
            
            foreach (var levelObject in allObjects)
            {
                var prefab = PrefabUtility.GetCorrespondingObjectFromSource(levelObject);
                if (prefab) levelObjects.Add(levelObject);
            }

            while (levelObjects.Count > 0)
            {
                Object.DestroyImmediate(levelObjects[0].gameObject);
                levelObjects.RemoveAt(0);
            }
        }

        private static void InitDictionary()
        {
            if (!levelObjectsDB) levelObjectsDB = Resources.Load<LevelObjectsDB>("LevelObjectsDB");
            
            if (dictionary != null)
            {
                dictionary.Clear();
            }
            else
            {
                dictionary = new Dictionary<string, LevelObject>();
            }
        }

        private static void SaveDictionary()
        {
            if (dictionary.Count == levelObjectsDB.prefabsStores.Count) return;
            
            levelObjectsDB.prefabsStores.Clear();
            
            foreach (var levelObject in dictionary)
            {
                var prefabStore = new PrefabStore()
                {
                    prefabName = levelObject.Value.name,
                    prefabObject = levelObject.Value
                };
                
                levelObjectsDB.prefabsStores.Add(prefabStore);
            }
            
            EditorUtility.SetDirty(levelObjectsDB);
        }
    }
}