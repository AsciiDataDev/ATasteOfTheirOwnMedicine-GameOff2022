using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveTextTrigger : MonoBehaviour
{
    public GameObject shootSlowGrabText;
    public GameObject robotDestroyed;
    public GameObject aTasteOfTheirOwnMedicineText;
    public GameObject lookBackText;
    void Update()
    {
        if(robotDestroyed == null){
            Destroy(shootSlowGrabText);
            aTasteOfTheirOwnMedicineText.SetActive(true);
            lookBackText.SetActive(true);
        }
    }
}
