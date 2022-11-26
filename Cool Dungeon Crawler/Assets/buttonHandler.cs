using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class buttonHandler : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject aboutPanel;
    public GameObject Player;
    public GameObject healthLabel;

    public StatsHandler playerStats;
    public int playerHealth;
    public int playerDamage;
    

    public bool isInventoryOpen;
    private void Start() {
        playerStats = Player.transform.GetChild(1).GetComponent<StatsHandler>();
        playerHealth=playerStats.GetCurrentHealth();
        playerDamage=playerStats.GetCurrentAttack();
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isInventoryOpen)
            {
                isInventoryOpen=false;
                aboutPanel.SetActive(false);
                Inventory.SetActive(false);
                Time.timeScale=1f;
                
            }
            else
            {
                Time.timeScale=0f;
                isInventoryOpen=true;
                Inventory.SetActive(true);
                
            }
        }

    }
  public void menuExit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying=false;
        #endif
        Application.Quit();
    }
    public void About()
    {
        aboutPanel.SetActive(true);
    }

}
