using System;
using Master.QSpaceCode.Game;
using Master.QSpaceCode.Static;
using UnityEditor;
using UnityEngine;

namespace Master.QSpaceCode.Editor
{
    public sealed class LevelManagerWindow : EditorWindow
    {
        private string levelNameToSave;
        private string levelNameToLoad;
        private int levelTypeId;
        private string[] levelsTypes;
        private int levelSectorId;
        private string[] levelSectors;

        [MenuItem("QSpace/Level Manager")]
        public static LevelManagerWindow ShowWindow()
        {
            return GetWindow<LevelManagerWindow>("Level Manager");
        }

        private void OnEnable()
        {
            levelNameToSave = string.Empty;
            levelNameToLoad = string.Empty;

            var levelTypesArray = Enum.GetValues(typeof(LevelType));
            levelsTypes = new string[levelTypesArray.Length];
            var t = 0;
            foreach (var type in levelTypesArray)
            {
                levelsTypes[t] = type.ToString();
                t++;
            }

            var levelSectorsArray = Enum.GetValues(typeof(LevelSector));
            levelSectors = new string[levelSectorsArray.Length];
            var s = 0;
            foreach (var type in levelSectorsArray)
            {
                levelSectors[s] = type.ToString();
                s++;
            }
        }

        private void OnGUI()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Имя уровня для сохранения");
            levelNameToSave = EditorGUILayout.TextField(levelNameToSave, GUILayout.Width(250));
            EditorGUILayout.Space();
            levelTypeId = EditorGUILayout.Popup(levelTypeId, levelsTypes, GUILayout.Width(250));
            levelSectorId = EditorGUILayout.Popup(levelSectorId, levelSectors, GUILayout.Width(250));
            EditorGUILayout.Space();
            if (GUILayout.Button("Сохранить уровень", GUILayout.Width(250)))
            {
                LevelManager.SaveCurrentLevel(levelNameToSave, (LevelType) levelTypeId, (LevelSector) levelSectorId);
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Имя уровня для загрузки");
            levelNameToLoad = EditorGUILayout.TextField(levelNameToLoad, GUILayout.Width(250));
            EditorGUILayout.Space();
            if (GUILayout.Button("Загрузить уровень", GUILayout.Width(250)))
            {
                var levels = LevelManager.GetAllLevels();
                var level = new LevelContainer();

                foreach (var container in levels)
                {
                    if (container.levelName != levelNameToLoad) continue;
                    level = container;
                    break;
                }

                if (level.levelName != levelNameToLoad) return;

                levelNameToSave = level.levelName;
                levelSectorId = (int) level.levelSector;
                levelTypeId = (int) level.levelType;

                foreach (var levelObject in level.levelObjects)
                {
                    var patch = $"Prefabs/LevelObjects/{levelObject.prefabName}";
                    var newLevelObjectPrefab = Resources.Load<LevelObject>(patch);
                    var newLevelObject = PrefabUtility.InstantiatePrefab(newLevelObjectPrefab) as LevelObject;
                    if (!newLevelObject) continue;
                    var transform = newLevelObject.transform;
                    transform.localPosition = levelObject.position;
                    transform.localRotation = levelObject.rotation;
                    transform.localScale = levelObject.scale;
                }
            }

            EditorGUILayout.Space();
            if (GUILayout.Button("Очистить сцену", GUILayout.Width(250)))
            {
                LevelManager.ClearLevel();
            }
        }
    }
}