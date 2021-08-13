using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainManager : MonoBehaviour {
    public TMP_InputField playerNameInput;
    
    public void StartPrototype() {
        GameManager.instance.playerName = playerNameInput.text;
        SceneManager.LoadScene(1);
    }

}
