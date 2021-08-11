using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quest : MonoBehaviour {

//ENCAPSULATION
    public bool isAcepted { get; private set;}
    [SerializeField]
    private bool isActiveQuest = false;
    [SerializeField]
    protected Quest nextQuest;
    
    public string questTitle;
    [TextArea]
    public string questText;

    public bool isObjectQuest;
    [SerializeField] private GameObject questObject;
    [SerializeField] private int objectsAmount;
    
    public bool isPlaceQuest;
    [SerializeField] private Vector3 place;
    [SerializeField] private float distance;

    public bool needTalkToNpc;
    [SerializeField] private NPCBase npc;

    private GameObject questListScreen;
    private TextMeshProUGUI titleMainText;
    private TextMeshProUGUI contentMainText;
    private Button CancelButton;


//ENCAPSULATION

//ABSTRACTION

  //POLYMORPHISM
    public virtual void Complete(GameObject gameObject, int num) { }

    public virtual void Complete(Vector3 place, float distance) { }

    public virtual void Complete (NPCBase npc) { }
  //POLYMORPHISM

    public void QuestAcepted() {
        isAcepted = true;
    }

    public void RemoveQuest() {
        isAcepted = false;
    }
//ABSTRACTION
}
