using UnityEngine;

namespace Master.QSpaceCode.Services.ServicesClasses.SettingsServiceSubclasses
{
    public class PlayerPrefsStorage
    {
        public void CheckDefaultInt(string name, int value)
        {
            if (!PlayerPrefs.HasKey(name)) PlayerPrefs.SetInt(name, value);
        }

        public int GetInt(string name)
        {
            if (PlayerPrefs.HasKey(name)) return PlayerPrefs.GetInt(name);
            return 0;
        }

        public void SetInt(string name, int value)
        {
            PlayerPrefs.SetInt(name, value);
        }

        public void CheckDefaultFloat(string name, float value)
        {
            if (!PlayerPrefs.HasKey(name)) PlayerPrefs.SetFloat(name, value);
        }

        public float GetFloat(string name)
        {
            if (PlayerPrefs.HasKey(name)) return PlayerPrefs.GetFloat(name);
            return 0;
        }
        
        public void SetFloat(string name, float value)
        {
            PlayerPrefs.SetFloat(name, value);
        }

        public void Save()
        {
            PlayerPrefs.Save();
        }
    }
}