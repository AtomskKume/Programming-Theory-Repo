using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestLogHandler : MonoBehaviour {

    //23A496
    private Color aceptedQuestColor = new Color(0.1372549f, 0.6431373f, 0.5849689f, 1f);
    //727272
    private Color completeQuestColor = new Color(0.4433962f, 0.4433962f, 0.4433962f, 1f);

    private InventoryHandler inventoryHandler;
    private Player player;

    public GameObject contentQuestLog;
    public GameObject questLogItemPrefab;
    public UIQuest uiQuest;

    private List<Quest> questLog = new List<Quest>();

    private void Awake() {
        inventoryHandler = GetComponent<InventoryHandler>();
        player = GetComponent<Player>();
    }

    public void AddQuestLog(Quest quest) {
        int questIndex = FindItem(quest);

        if (questIndex == -1) {
            questLog.Add(quest);
            quest.isAcepted = true;
            player.target.GetComponent<NPCBase>().GiveQuest(quest);
            inventoryHandler.ReviweQuestItems();
            UpdateQuestLog();
        }
    }
    public void RemoveQuestLog(Quest quest) {
        int questIndex = FindItem(quest);
        
        if (questIndex != -1) {
            questLog.RemoveAt(questIndex);
            quest.isAcepted = false;
            UpdateQuestLog();
        }
    }

    private int FindItem(Quest item) {
        int i = 0;
        int posItem = -1;
        foreach (Quest quest in questLog) {
            if (quest.questId == item.questId) {
                posItem = i;
            }
            i++;
        }
        return posItem;
    }

    private void UpdateQuestLog() {
        CleanQuestLog();
        foreach (Quest quest in questLog) {
            if (!quest.isComplete) {
                GameObject questItem = Instantiate(questLogItemPrefab, contentQuestLog.transform);
                questItem.transform.Find("QuestButton/TextButton").gameObject.GetComponent<TextMeshProUGUI>().text = quest.questTitle;
            
                if (quest.isAcepted) {
                    questItem.transform.Find("QuestButton/TextButton").gameObject.GetComponent<TextMeshProUGUI>().color = aceptedQuestColor;
                }
                if (quest.isDone) {
                    questItem.transform.Find("QuestButton/TextButton").gameObject.GetComponent<TextMeshProUGUI>().color = completeQuestColor;
                }

                questItem.GetComponent<QuestListItem>().AddQuestLink(quest, uiQuest);
            }
        }
    }

    private void CleanQuestLog() {
        Transform[] ts = contentQuestLog.transform.GetComponentsInChildren<Transform>();
        
        if(ts != null){
            foreach (Transform t in ts) {
                if(t.gameObject.name != "QuestLogContent") {
                    Destroy(t.gameObject);
                }
            }
        }
    }

    //POLYMORPHISM
    public void UpdateQuestComplete(GameObject item, int amount) {
        foreach(Quest quest in questLog) {
            quest.UpdateQuest(item, amount);
        }
        UpdateQuestLog();
    }

    public void UpdateQuestComplete(NPCBase npc) {
        foreach (Quest quest in questLog) {
            quest.UpdateQuest(npc);
        }
        UpdateQuestLog();
    }
    //POLYMORPHISM
}
