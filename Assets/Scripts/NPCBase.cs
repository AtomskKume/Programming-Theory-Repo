using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        uiQuest.titleMainText.text = $"{npcName} - {npcJob}";
        uiQuest.contentMainText.text = introduction;
        uiQuest.questListScreen.SetActive(true);
    }
    public virtual void CloseQuestUI() {
        uiQuest.questListScreen.SetActive(false);
    }

    public void Selected() {
        activeScreen.gameObject.SetActive(true);
    }
    public void Deselected() {
        activeScreen.gameObject.SetActive(false);
    }

}
