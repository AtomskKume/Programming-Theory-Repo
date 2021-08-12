using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quest : MonoBehaviour {

    //ENCAPSULATION
    public string questId;
    public bool isActive = false;
    public bool isAcepted = false;
    public bool isDone = false;
    public bool isComplete = false;

    public NPCBase previousNpc;
    public NPCBase npcToComplete;

    [SerializeField] protected Quest nextQuest;
            
    public string questTitle;
    [TextArea] public string questText;

    public bool isObjectQuest;
    public bool isObjectQuestDone = false;
    public GameObject questObject;
    public int objectsAmount;
    public int actualAmount;
    
    public bool isPlaceQuest;
    public bool isPlaceQuestDone = false;
    public Vector3 place;
    public float distance;

    public bool needTalkToNpc;
    public bool needTalkToNpcDone = false;
    public NPCBase npc;

    public List<QuestReward> reward;

    //ENCAPSULATION
   

//ABSTRACTION

  //POLYMORPHISM
    public virtual void UpdateQuest(GameObject gameObject, int num) {
        if (isObjectQuest) {
            if(gameObject.name == questObject.name) {
                actualAmount = num;
                if(actualAmount >= objectsAmount) {
                    actualAmount = objectsAmount;
                    isObjectQuestDone = true;
                } else {
                    isObjectQuestDone = false;
                }
                QuestIsDone();
            }
        }
    }

    public virtual void UpdateQuest(Vector3 place, float distance) { }

    public virtual void UpdateQuest(NPCBase npc) {
        if (needTalkToNpc) { 
            if(npc.name == this.npc.name) {
                needTalkToNpcDone = true;
                QuestIsDone();
            }
        }
    }
  //POLYMORPHISM

    public void CompleteQuest() {
        isComplete = true;
        if(nextQuest!= null) {
            nextQuest.isActive = true;
        }
    }

    private void QuestIsDone() {
        int countOfDone = 0;
        int doneCount = 0;
        if (isObjectQuest) countOfDone++;
        if (isPlaceQuest) countOfDone++;
        if (needTalkToNpc) countOfDone++;

        if (isObjectQuestDone) doneCount++;
        if (isPlaceQuestDone) doneCount++;
        if (needTalkToNpcDone) doneCount++;
        
        if (countOfDone == doneCount && countOfDone != 0 && doneCount != 0) isDone = true;
        
    }
//ABSTRACTION
}
