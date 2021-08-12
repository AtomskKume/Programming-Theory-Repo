using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIQuest : MonoBehaviour {
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


    private void Awake() {
        ClearQuestLinks();
        //cancelListButton.onClick.AddListener(() => { Debug.Log("List"); questListScreen.SetActive(false); });
        //cancelDetailButton.onClick.AddListener(()=> { Debug.Log("Detail");  questDetailScreen.SetActive(false); });
    }

    public void ClearQuestLinks() {
        GameObject[] questLinks = GameObject.FindGameObjectsWithTag("QuestLink");
        foreach(GameObject questLink in questLinks) {
            Destroy(questLink);
        }
    }

    public void OpenQuestList(string title, string introduction, List<Quest> questList) {
        titleMainText.text = title;
        contentMainText.text = introduction;
        CreateQuestLinks(questList);
        questListScreen.SetActive(true);
    }

    public void CloseQuestList() {
        Debug.Log("List");
        ClearQuestLinks();
        questListScreen.SetActive(false);
        questDetailScreen.SetActive(false);
    }

    private void CreateQuestLinks(List<Quest> questList) {
        ClearQuestLinks();
        foreach (Quest quest in questList) {
            if(quest.isActive && !quest.isComplete) {
                GameObject questItem = Instantiate(questListItemPrefab, questListPanel.transform);
                questItem.transform.Find("QuestButton/TextButton").gameObject.GetComponent<TextMeshProUGUI>().text = quest.questTitle;
                questItem.GetComponent<QuestListItem>().AddQuestLink(quest, this);
            }
        }
    }

    public void OpenDetailQuest(Quest quest) {
        titleDetailText.text = quest.questTitle;
        contentDetailText.text = quest.questText;

        acceptDetailButton.gameObject.SetActive(!quest.isAcepted);
        removeDetailButton.gameObject.SetActive(quest.isAcepted);
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
        Debug.Log("Detail");
        questDetailScreen.SetActive(false);
    }

}
