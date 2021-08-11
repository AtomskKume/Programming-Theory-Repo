using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIQuest : MonoBehaviour {
    public GameObject questListScreen;
    public TextMeshProUGUI titleMainText;
    public TextMeshProUGUI contentMainText;
    public Button CancelButton;
    public GameObject questListItemPrefab;

    private void Awake() {
        CancelButton.onClick.AddListener(() => { questListScreen.SetActive(false); });
    }
}
