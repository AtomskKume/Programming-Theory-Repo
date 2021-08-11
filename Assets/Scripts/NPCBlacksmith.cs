using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE
public class NPCBlacksmith : NPCBase {
   
    
    void Awake() {
        npcJob = "Blacksmith";
        uiQuest = GameObject.Find("Quests").GetComponent<UIQuest>();
        activeScreen = gameObject.transform.Find("ActiveScreen").gameObject;
    }

    // Update is called once per frame
    void Update() {
        
    }
}
