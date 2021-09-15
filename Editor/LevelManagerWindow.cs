using System.Collections.Generic;
using System.IO;
using Master.QSpaceCode.Configs;
using Master.QSpaceCode.Game;
using UnityEditor;
using UnityEngine;

namespace Master.QSpaceCode.Editor
{
    public sealed class LevelManagerWindow : EditorWindow
    {
        private static string levelNameToSave;
        private static string levelNameToLoad;
        private static readonly Dictionary<LevelObject, LevelObject> LevelObjects = new Dictionary<LevelObject, LevelObject>();
        private string objectsCountText;
        private static LevelObjectsDB levelObjectsDB;

        [MenuItem("QSpace/Level Manager")]
        public static LevelManagerWindow Init()
        {
            levelNameToSave = string.Empty;
            LevelObjects.Clear();
            return GetWindow<LevelManagerWindow>("Level Manager");
        }

        private void OnEnable()
        {

        }

        private void OnGUI()
        {
            EditorGUILayout.Space();
            
            if (GUILayout.Button("Обновить список объектов", GUILayout.Width(250)))
            {
                LevelObjects.Clear();
                var allObjects = FindObjectsOfType<LevelObject>();

                foreach (var levelObject in allObjects)
                {
                    var prefab = PrefabUtility.GetCorrespondingObjectFromSource(levelObject);
                    if (prefab) LevelObjects.Add(levelObject, prefab);
                }

                objectsCountText = $"Найдено {LevelObjects.Count} объектов";
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField(objectsCountText, GUILayout.Width(250));
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Имя уровня для сохранения");
            levelNameToSave = EditorGUILayout.TextField(levelNameToSave, GUILayout.Width(250));
            EditorGUILayout.Space();
            if (GUILayout.Button("Сохранить уровень", GUILayout.Width(250)))
            {
                var levelContainer = new LevelContainer
                {
                    prefabsNames = new string[LevelObjects.Count],
                    positions = new Vector3[LevelObjects.Count],
                    rotations = new Quaternion[LevelObjects.Count],
                    sizes = new Vector3[LevelObjects.Count]
                };

                if (!levelObjectsDB) levelObjectsDB = Resources.Load<LevelObjectsDB>("LevelObjectsDB");

                var id = 0;

                foreach (var levelObject in LevelObjects)
                {
                    var prefab = levelObject.Value;
                    var prefabName = prefab.name;
                    var transform = levelObject.Key.transform;

                    if (!levelObjectsDB.names.Contains(prefabName))
                    {
                        levelObjectsDB.names.Add(prefabName);
                        levelObjectsDB.prefabs.Add(prefab);
                        EditorUtility.SetDirty(levelObjectsDB);
                    }

                    levelContainer.prefabsNames[id] = prefabName;
                    levelContainer.positions[id] = transform.position;
                    levelContainer.rotations[id] = transform.rotation;
                    levelContainer.sizes[id] = transform.lossyScale;

                    id++;
                }

                var containerJson = JsonUtility.ToJson(levelContainer, true);
                var path = $"Assets/Master/Resources/Levels/{levelNameToSave}.json";
                File.WriteAllText(path, containerJson);
                AssetDatabase.Refresh();
            }
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Имя уровня для загрузки");
            levelNameToLoad = EditorGUILayout.TextField(levelNameToLoad, GUILayout.Width(250));
            EditorGUILayout.Space();
            if (GUILayout.Button("Загрузить уровень", GUILayout.Width(250)))
            {
                if (!levelObjectsDB) levelObjectsDB = Resources.Load<LevelObjectsDB>("LevelObjectsDB");

                var assets = Resources.LoadAll("Levels/");
                var textAssets = new Dictionary<string, string>();
                foreach (var jsonContainer in assets)
                {
                    if (jsonContainer is TextAsset textAsset) textAssets.Add(jsonContainer.name, textAsset.text);
                }
                if (!textAssets.ContainsKey(levelNameToLoad)) return;
                var levelText = textAssets[levelNameToLoad];
                var levelContainer = JsonUtility.FromJson<LevelContainer>(levelText);

                for (var i = 0; i < levelContainer.prefabsNames.Length; i++)
                {
                    var prefabName = levelContainer.prefabsNames[i];
                    var newPosition = levelContainer.positions[i];
                    var newRotation = levelContainer.rotations[i];
                    var newScale = levelContainer.sizes[i];
                    LevelObject prefab = null;

                    for (int j = 0; j < levelObjectsDB.names.Count; j++)
                    {
                        if (prefabName != levelObjectsDB.names[j]) continue;
                        prefab = levelObjectsDB.prefabs[j]; break;
                    }
                    
                    if (prefab == null) continue;

                    var levelObject = PrefabUtility.InstantiatePrefab(prefab) as LevelObject;
                    var transform = levelObject.transform;
                    transform.position = newPosition;
                    transform.rotation = newRotation;
                    transform.localScale = newScale;
                }
            }
            // this.Repaint();
        }
    }
}