using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestListItem : MonoBehaviour
{
    private Quest vinculatedQuest;

    public void AddQuestLink(Quest quest, UIQuest uiQuest) {
        vinculatedQuest = quest;

        transform.Find("QuestButton").gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        transform.Find("QuestButton").gameObject.GetComponent<Button>().onClick.AddListener(()=> {uiQuest.OpenDetailQuest(vinculatedQuest); });
    }

}
