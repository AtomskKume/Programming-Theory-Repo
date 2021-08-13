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
    public InventoryHandler inventoryHandler;

    private void Awake() {
        ClearQuestLinks();
    }

    public void ClearQuestLinks() {
        GameObject[] questLinks = GameObject.FindGameObjectsWithTag("QuestLink");
        foreach(GameObject questLink in questLinks) {
            Destroy(questLink);
        }
    }

    public void OpenQuestList(string title, string introduction, List<Quest> questList, NPCBase npc) {
        questListScreen.SetActive(true);
        questLogHandler.UpdateQuestComplete(npc);
        actualQuestList = questList;
        titleMainText.text = title;
        contentMainText.text = introduction.Replace("$p", GameManager.instance.playerName);
        CreateQuestLinks(questList);
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)questListPanel.transform);
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

                if (quest.isAcepted && quest.isDone) {
                    questItem.transform.Find("QuestButton/TextButton").gameObject.GetComponent<TextMeshProUGUI>().color = completeQuestColor;
                }

                questItem.GetComponent<QuestListItem>().AddQuestLink(quest, this);
            }
        }
    }

    public void OpenDetailQuest(Quest quest) {
        questDetailScreen.SetActive(true);
        titleDetailText.text = quest.questTitle;
        contentDetailText.text = quest.questText;

        
        acceptDetailButton.gameObject.SetActive(!quest.isAcepted);
        acceptDetailButton.onClick.RemoveAllListeners();
        acceptDetailButton.onClick.AddListener(() => { questLogHandler.AddQuestLog(quest); OpenDetailQuest(quest); if (questListScreen.activeSelf) { CreateQuestLinks(); } });

        removeDetailButton.gameObject.SetActive(quest.isAcepted);
        completeDetailButton.onClick.RemoveAllListeners();
        removeDetailButton.onClick.AddListener(() => { questLogHandler.RemoveQuestLog(quest); CloseDetailQuest(); if (questListScreen.activeSelf) { CreateQuestLinks(); } });
        
        if(quest.isAcepted && quest.isDone && questListScreen.activeSelf) {
            completeDetailButton.gameObject.SetActive(true);
            completeDetailButton.onClick.RemoveAllListeners();
            completeDetailButton.onClick.AddListener(()=> { 
                quest.CompleteQuest(); 
                questLogHandler.RemoveQuestLog(quest); 
                CloseDetailQuest();
                if (questListScreen.activeSelf) {
                    CreateQuestLinks(); 
                }
                Debug.Log(quest.questTitle);
                if (quest.isObjectQuest) {
                    inventoryHandler.RemoveItemToInventory(quest.questObject, quest.objectsAmount);
                }
                GetReward(quest);
            });
        } else {
            completeDetailButton.gameObject.SetActive(false);
        }

        objetiveDetailText.text = "";
        if (quest.isObjectQuest) {
            objetiveDetailText.text = $"- {quest.questObject.name}: {quest.actualAmount}/{quest.objectsAmount} \n";
        }
        if (quest.isPlaceQuest) {

        }
        if (quest.needTalkToNpc) {
            objetiveDetailText.text += $"- Talk to {quest.npc.GetNpcQuestName()} \n";
        }

        if(quest.reward.Count > 0) {
            objetiveDetailText.text += "\n\nRewards:\n";
            foreach (QuestReward reward in quest.reward) {
                objetiveDetailText.text += $"- {reward.item.name}: {reward.amount}";
            }
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)questDetailPanel.transform);
    }

    public void CloseDetailQuest() {
        questDetailScreen.SetActive(false);
    }

    private void GetReward(Quest quest) {
        if(quest.reward.Count > 0) {
            foreach (QuestReward reward in quest.reward) {
                inventoryHandler.AddItemToInventory(reward.item, reward.amount);
            }
        }
    }
}
