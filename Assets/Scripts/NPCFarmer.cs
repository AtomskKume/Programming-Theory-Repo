using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INHERITANCE
public class NPCFarmer : NPCBase {
    

    void Awake() {
        npcJob = "Farmer";
        uiQuest = GameObject.Find("Quests").GetComponent<UIQuest>();
        activeScreen = gameObject.transform.Find("ActiveScreen").gameObject;
    }
    

    // Update is called once per frame
    void Update() {
        
    }

    
}
