using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLanguage : MonoBehaviour
{
    public Languages languagesList;
    public TextObjects[] textObjects;

    void Update()
    {
        foreach(TextObjects obj in textObjects){
            Languages.TextOption textValue = languagesList.currentLanguage.textValues.Find(x => x.key == obj.key);
            obj.text.text = textValue.val;
        }
    }
}
