using TMPro;
using UnityEngine;

namespace Master.QSpaceCode.PlayerUi
{
    public class PlayerImage : MonoBehaviour
    {
        [SerializeField] private TMP_Text playerName;
        [SerializeField] private GameObject masterIcon;

        public void SetInfo(string newName, bool isMaster)
        {
            playerName.text = newName;
            masterIcon.SetActive(isMaster);
        }
    }
}