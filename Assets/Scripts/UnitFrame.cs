using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitFrame : MonoBehaviour
{

    [SerializeField]
    private CharacterSelection _characterSelection;
    [SerializeField]
    private IconGenerator _iconGenerator;

    //public Sprite icon;

    public GameObject iconSlot;


    // Start is called before the first frame update
    void Start()
    {
        //if (CompareTag("PlayerUnitFrame"))
        //{
        //    if (_characterSelection.chosenClass == 0)
        //    {
        //        icon = _iconGenerator.generatedPlayerIcons[0]; //warrior icon
        //    }
        //    if (_characterSelection.chosenClass == 1)
        //    {
        //        icon = _iconGenerator.generatedPlayerIcons[1]; //mage icon
        //    }
        //    iconSlot.GetComponent<Image>().sprite = icon;
        //}
    }

    public void SetIcon(Sprite sprite)
    {
        iconSlot.GetComponent<Image>().sprite = sprite;
    }
}
