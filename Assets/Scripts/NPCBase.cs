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
        uiQuest.OpenQuestList($"{npcName} - {npcJob}", introduction, questList);
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
}
