using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/Languages", order = 1)]
public class Languages : ScriptableObject
{
   public List<Language> langs;
   public Language currentLanguage;

    [Serializable]
    public class Language {
        public string language;
        public List<TextOption> textValues;
    }

    [Serializable]
    public class TextOption {
        public string key;
        public string val;
    }

    public void addLanguage(string newLang)
    {
        if (langs == null){
            langs = new List<Language>();
        }
        Language lang = new Language();
        lang.language = newLang;
        lang.textValues = new List<TextOption>();
        langs.Add(lang);
    }
    
    public void removeLanguage(string language)
    {
        langs.RemoveAll(x => x.language == language);
    }
    public void changeLanguage(string language){
        currentLanguage = langs.Find(lang => lang.language == language);
    }

    public void addTextOption(string language, string name, string text){
        Language langToUpdate = langs.Find(lang => lang.language == language);
        if(langToUpdate != null){
            TextOption textValue = new TextOption();
            textValue.key = name;
            textValue.val = text;
            langToUpdate.textValues.Add(textValue);
        }
    } 
    public  void removeTextOption(string language, string key){
        Language langToRemove = langs.Find(lang => lang.language == language);
        langToRemove.textValues.RemoveAll(x => x.key == key);
    }
}
