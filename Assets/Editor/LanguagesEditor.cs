using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

[CustomEditor(typeof(Languages))]
public class LanguageEditor : Editor
{

    private string[] choices;
    private int choices_index = 0;
    private string currentLanguage;

    private string newLanguage;

    private string keyName;
    private string keyText;

    public override void OnInspectorGUI()
    {
        DrawChangeLanguage();
        DrawAddLanguage();
        DrawRemoveLanguage();
        DrawManageKeys();
        DrawSave();
    }
    public void DrawChangeLanguage()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        Languages languages = (Languages)target;
        choices = languages.langs.Select(x=>x.language).ToArray();
        choices_index = EditorGUILayout.Popup("Current Language", choices_index, choices, EditorStyles.popup);

        if(languages.langs != null){
            
            if(choices_index < choices.Length) {
                currentLanguage = choices[choices_index];
                languages.changeLanguage(currentLanguage);
            }
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }
    public void DrawAddLanguage()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        Languages languages = (Languages)target;
        newLanguage = EditorGUILayout.TextField("New Language",newLanguage);
        if(GUILayout.Button("Add Language"))
        {
            languages.addLanguage(newLanguage);
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    
    }
    public void DrawRemoveLanguage()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        Languages languages = (Languages)target;
        if(choices.Length > 0 && choices != null && Array.IndexOf(choices, currentLanguage) != -1){ 
            if(GUILayout.Button("Remove Language"))
            {
                languages.removeLanguage(currentLanguage);
            }
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }
    public void DrawManageKeys()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        Languages languages = (Languages)target;
        if(choices.Length > 0 && choices != null && Array.IndexOf(choices, currentLanguage) != -1){ 
            keyName = EditorGUILayout.TextField("Name", keyName);
            keyText = EditorGUILayout.TextField("Text", keyText);

            if(GUILayout.Button("Add Key"))
            {
                languages.addTextOption(currentLanguage, keyName, keyText);
                keyName = "";
                keyText = "";
            }

            if(languages.langs != null){
                Languages.Language selectedLanguage = languages.langs.Find(lang => lang.language == currentLanguage);
                List<string> keys = selectedLanguage.textValues.Select(x=>x.key).ToList();
                foreach (string key in keys)
                {
                    Languages.TextOption textValue = selectedLanguage.textValues.Find(activeLanguage => activeLanguage.key == key);
                    textValue.val = EditorGUILayout.TextField(key, textValue.val);
                    if(GUILayout.Button("Delete Key"))
                    {
                        languages.removeTextOption(currentLanguage,key);
                    }
                    
                }
            }
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }

    public void DrawSave()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        Languages languages = (Languages)target;
        if(choices.Length > 0 && choices != null  && Array.IndexOf(choices, currentLanguage) != -1){ 
            if(GUILayout.Button("Save Changes"))
            {
                EditorUtility.SetDirty(languages);
            }
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }
}
