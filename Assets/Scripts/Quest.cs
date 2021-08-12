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
    public bool isComplete = false;

    [SerializeField] protected Quest nextQuest;
            
    public string questTitle;
    [TextArea] public string questText;

    public bool isObjectQuest;
    public GameObject questObject;
    public int objectsAmount;
    public int actualAmount;
    
    public bool isPlaceQuest;
    public Vector3 place;
    public float distance;

    public bool needTalkToNpc;
    public NPCBase npc;

//ENCAPSULATION

//ABSTRACTION

  //POLYMORPHISM
    public virtual void Complete(GameObject gameObject, int num) { }

    public virtual void Complete(Vector3 place, float distance) { }

    public virtual void Complete (NPCBase npc) { }
  //POLYMORPHISM

//ABSTRACTION
}
