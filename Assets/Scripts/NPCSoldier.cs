using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE
public class NPCSoldier : NPCBase {
   
    void Awake() {
        npcJob = "Soldier";
        uiQuest = GameObject.Find("Quests").GetComponent<UIQuest>();
        activeScreen = gameObject.transform.Find("ActiveScreen").gameObject;
    }

    // Update is called once per frame
    void Update() {
        
    }
}
