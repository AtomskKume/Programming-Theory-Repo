using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIQuest : MonoBehaviour {
    // A42323
    private Color newQuestColor = new Color(0.6415094f, 0.1361695f, 0.1361695f, 1f);
    //23A496
    private Color aceptedQuestColor = new Color(0.1372549f, 0.6431373f, 0.5849689f, 1f);
    //727272
    private Color completeQuestColor = new Color(0.4433962f, 0.4433962f, 0.4433962f, 1f);

    private List<Quest> actualQuestList;

    //Quest list screen
    public GameObject questListScreen;
    public GameObject questListPanel;
    public TextMeshProUGUI titleMainText;
    public TextMeshProUGUI contentMainText;
    public Button cancelListButton;
    public GameObject questListItemPrefab;

    //Quest detail screen
    public GameObject questDetailScreen;
    public GameObject questDetailPanel;
    public TextMeshProUGUI titleDetailText;
    public TextMeshProUGUI contentDetailText;
    public TextMeshProUGUI objetiveDetailText;
    public Button cancelDetailButton;
    public Button acceptDetailButton;
    public Button removeDetailButton;
    public Button completeDetailButton;

    //QuestLog
    public QuestLogHandler questLogHandler;

    private void Awake() {
        ClearQuestLinks();
    }

    public void ClearQuestLinks() {
        GameObject[] questLinks = GameObject.FindGameObjectsWithTag("QuestLink");
        foreach(GameObject questLink in questLinks) {
            Destroy(questLink);
        }
    }

    public void OpenQuestList(string title, string introduction, List<Quest> questList) {
        actualQuestList = questList;
        titleMainText.text = title;
        contentMainText.text = introduction;
        CreateQuestLinks(questList);
        questListScreen.SetActive(true);
    }

    public void CloseQuestList() {
        ClearQuestLinks();
        actualQuestList = null;
        questListScreen.SetActive(false);
        questDetailScreen.SetActive(false);
    }

    private void CreateQuestLinks() {
        CreateQuestLinks(actualQuestList);
    }
    private void CreateQuestLinks(List<Quest> questList) {
        ClearQuestLinks();
        foreach (Quest quest in questList) {
            if(quest.isActive && !quest.isComplete) {
                GameObject questItem = Instantiate(questListItemPrefab, questListPanel.transform);
                questItem.transform.Find("QuestButton/TextButton").gameObject.GetComponent<TextMeshProUGUI>().text = quest.questTitle;
                if (quest.isAcepted) {
                    questItem.transform.Find("QuestButton/TextButton").gameObject.GetComponent<TextMeshProUGUI>().color = aceptedQuestColor;
                } else {
                    questItem.transform.Find("QuestButton/TextButton").gameObject.GetComponent<TextMeshProUGUI>().color = newQuestColor;
                }

                if (quest.isComplete) {
                    questItem.transform.Find("QuestButton/TextButton").gameObject.GetComponent<TextMeshProUGUI>().color = completeQuestColor;
                }

                questItem.GetComponent<QuestListItem>().AddQuestLink(quest, this);
            }
        }
    }

    public void OpenDetailQuest(Quest quest) {
        titleDetailText.text = quest.questTitle;
        contentDetailText.text = quest.questText;

        acceptDetailButton.gameObject.SetActive(!quest.isAcepted);
        acceptDetailButton.onClick.AddListener(() => { questLogHandler.AddQuestLog(quest); OpenDetailQuest(quest); if (questListScreen.activeSelf) { CreateQuestLinks(); } });
        removeDetailButton.gameObject.SetActive(quest.isAcepted);
        removeDetailButton.onClick.AddListener(() => { questLogHandler.RemoveQuestLog(quest); CloseDetailQuest(); if (questListScreen.activeSelf) { CreateQuestLinks(); } });
        completeDetailButton.gameObject.SetActive(quest.isAcepted && !quest.isComplete);

        if (quest.isObjectQuest) {
            objetiveDetailText.text = $"- {quest.questObject.name}: {quest.actualAmount}/{quest.objectsAmount} \n";
        }
        if (quest.isPlaceQuest) {

        }
        if (quest.needTalkToNpc) {

        }

        questDetailScreen.SetActive(true);
    }

    public void CloseDetailQuest() {
        questDetailScreen.SetActive(false);
    }

}
