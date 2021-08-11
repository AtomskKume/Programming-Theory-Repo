using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIQuest : MonoBehaviour {
    public GameObject questListScreen;
    public GameObject questListPanel;
    public TextMeshProUGUI titleMainText;
    public TextMeshProUGUI contentMainText;
    public Button CancelButton;
    public GameObject questListItemPrefab;

    private void Awake() {
        ClearQuestLinks();
        CancelButton.onClick.AddListener(() => { questListScreen.SetActive(false); });
    }

    public void ClearQuestLinks() {
        GameObject[] questLinks = GameObject.FindGameObjectsWithTag("QuestLink");
        foreach(GameObject questLink in questLinks) {
            Destroy(questLink);
        }
    }
}
