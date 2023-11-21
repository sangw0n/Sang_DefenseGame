using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{
    public static DeckManager instance { get; private set; }
    public List<SlotData> slotDatas = new List<SlotData>();

    [Header("# Deck Var")]
    [SerializeField] private GameObject deckPrefab;
    [SerializeField] private GameObject R_deckPrefab; // 해제 x 
    [SerializeField] private int playerUnit = 1;
    [SerializeField] private int lockUnit = 1;

    private Button button;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    private void Start()
    {
        GameObject deckPanel = GameObject.Find("ButtonPanel");

        for (int i = 0; i < playerUnit; i++)
        {
            GameObject Obj = Instantiate(deckPrefab, deckPanel.transform, false);
            Obj.name = "Deck_" + i;

            SlotData slot = new SlotData();
            slot.isEmpty = true;
            slot.slotObj = Obj;
            slotDatas.Add(slot);
        }
        for (int i = 0; i < lockUnit; i++)
        {
            GameObject R_Obj = Instantiate(R_deckPrefab, deckPanel.transform, false);
            R_Obj.name = "R_Deck_" + i;

            SlotData slot = new SlotData();
            slot.isEmpty = false; ;
            slot.slotObj = R_Obj;
            slotDatas.Add(slot);
        }
    }
}
