using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//INHERITANCE
public class NPCBase : MonoBehaviour {
    [SerializeField] protected List<Quest> questList;
    [SerializeField] protected string npcName;
    [SerializeField, TextArea] protected string introduction;

    protected string npcJob;
    protected bool isTarget;
    protected UIQuest uiQuest;
    public GameObject activeScreen;
    

    public virtual void OpenQuestUI() {
        uiQuest.OpenQuestList($"{npcName} - {npcJob}", introduction, questList, this);
    }
    public virtual void CloseQuestUI() {
        uiQuest.CloseQuestList();
    }

    public void Selected() {
        activeScreen.gameObject.SetActive(true);
    }
    public void Deselected() {
        activeScreen.gameObject.SetActive(false);
    }

    public void GiveQuest(Quest quest) {
        if(quest.isAcepted && quest.npcToComplete!= null) {
            quest.previousNpc = this;
            quest.npcToComplete.questList.Add(quest);
            int questIndex = FindItem(quest);
            if(questIndex != -1) {
                questList.RemoveAt(questIndex);
            }
        }
    }

    public string GetNpcQuestName() {
        return $"{npcName} - {npcJob}";
    }

    private int FindItem(Quest item) {
        int i = 0;
        int posItem = -1;
        foreach (Quest quest in questList) {
            if (quest.questId == item.questId) {
                posItem = i;
            }
            i++;
        }
        return posItem;
    }
}
