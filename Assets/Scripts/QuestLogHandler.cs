using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestLogHandler : MonoBehaviour {

    public GameObject contentQuestLog;
    public GameObject questLogItemPrefab;
    public UIQuest uiQuest;

    private List<Quest> questLog = new List<Quest>();
    public void AddQuestLog(Quest quest) {
        int questIndex = FindItem(quest);

        if (questIndex == -1) {
            questLog.Add(quest);
            quest.isAcepted = true;
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
            GameObject questItem = Instantiate(questLogItemPrefab, contentQuestLog.transform);
            questItem.transform.Find("QuestButton/TextButton").gameObject.GetComponent<TextMeshProUGUI>().text = quest.questTitle;
            questItem.GetComponent<QuestListItem>().AddQuestLink(quest, uiQuest);
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
}
