using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject target;
    private List<InventoryItem> auxInventory;
    private Vector3 place;


    private RaycastHit hit;

    


    public class InventoryItem
    {
        public GameObject item;
        public int amount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            bool isHit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);

            if(isHit) {
                if(hit.transform.gameObject.tag == "NPC") {
                    QuitTarget();
                    target = hit.transform.gameObject;
                    target.GetComponent<NPCBase>().Selected();
                } else {
                    QuitTarget();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            if(target != null && target.gameObject.tag == "NPC") {
                target.GetComponent<NPCBase>().OpenQuestUI();
            }
        }
    }

    private void QuitTarget() {
        if (target != null) {
            target.GetComponent<NPCBase>().CloseQuestUI();
            target.GetComponent<NPCBase>().Deselected();
        }
        target = null;
    }

}
