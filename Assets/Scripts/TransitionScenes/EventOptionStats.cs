using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EventOptionStats: MonoBehaviour {

    public string optionText;

    public bool changeHP;
    public int HPChange = 0;

    public bool changeMaxHP;
    public int maxHPChange = 0;

    public bool changeFinesse;
    public int finesseChange = 0;

    public bool changeArmour;
    public int armourChange = 0;

    public int GetHPChange()
    {
        return HPChange;
    }

    public int GetMaxHPChange()
    {
        return maxHPChange;
    }

    public int GetFinesseChange()
    {
        return finesseChange;
    }

    public int GetArmourChange()
    {
        return armourChange;
    }

    public string GetOptionText()
    {
        return optionText;
    }

}

[CustomEditor(typeof(EventOptionStats))]
public class MyScriptEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as EventOptionStats;

        myScript.changeHP = GUILayout.Toggle(myScript.changeHP, "changeHP");
        myScript.changeMaxHP = GUILayout.Toggle(myScript.changeMaxHP, "changeMaxHP");
        myScript.changeFinesse = GUILayout.Toggle(myScript.changeFinesse, "changeFinesse");
        myScript.changeArmour = GUILayout.Toggle(myScript.changeArmour, "changeArmour");

        if (myScript.changeHP)
        {
            myScript.HPChange = EditorGUILayout.IntSlider("HP Change field:", myScript.HPChange, -100, 100);
        }
        if (myScript.changeMaxHP)
        {
            myScript.maxHPChange = EditorGUILayout.IntSlider("Max HP Change field:", myScript.maxHPChange, -100, 100);
        }
        if (myScript.changeFinesse)
        {
            myScript.finesseChange = EditorGUILayout.IntSlider("Finesse Change field:", myScript.finesseChange, -100, 100);
        }
        if (myScript.changeArmour)
        {
            myScript.armourChange = EditorGUILayout.IntSlider("Armour Change field:", myScript.armourChange, -10, 10);
        }
        myScript.optionText = EditorGUILayout.TextField("Option Text field:", myScript.optionText);

    }
}
