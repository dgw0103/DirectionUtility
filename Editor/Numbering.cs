using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;

public class Numbering
{
    private static GameObject numberIconPrefab;
    private const string path = "DirectionUtility/";
    private static readonly string canvasPrefabGUID = "5496e1b104fd394428a082d7a38bc4cd";
    private static readonly string numberIconPrefabGUID = "a410fe040b94f9d468330865e410538d";



    [MenuItem(path + "Add number icon")]
    private static void AddNumberIcon()
    {
        GameObject canvasPrefab = CanvasPrefab;
        GameObject canvasInScene = FindObjectsByType().SingleOrDefault((x) => PrefabUtility.GetCorrespondingObjectFromSource(x) == canvasPrefab);

        if (canvasInScene == null)
        {
            canvasInScene = PrefabUtility.InstantiatePrefab(canvasPrefab) as GameObject;
        }

        GameObject numberIconPrefab = NumberIconPrefab;
        GameObject numberIcon = PrefabUtility.InstantiatePrefab(numberIconPrefab, canvasInScene.transform) as GameObject;
        int numberIconCount =
            canvasInScene.GetComponentsInChildren<Image>().Count((x) => PrefabUtility.GetCorrespondingObjectFromSource(x).gameObject == numberIconPrefab);
        SetNumber(numberIcon, numberIconCount - 1);
    }
    private static GameObject CanvasPrefab
    {
        get
        {
            return AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(canvasPrefabGUID), typeof(GameObject)) as GameObject;
        }
    }
    private static GameObject[] FindObjectsByType()
    {
        
#if UNITY_2023_1_OR_NEWER
        return Object.FindObjectsByType<GameObject>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
#elif UNITY_2020_1_OR_NEWER
        return Object.FindObjectsOfType<GameObject>(false);
#else
        return Object.FindObjectsOfType<GameObject>();
#endif
    }
    private static GameObject NumberIconPrefab
    {
        get
        {
            if (numberIconPrefab == null)
            {
                numberIconPrefab = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(numberIconPrefabGUID), typeof(GameObject)) as GameObject;
            }

            return numberIconPrefab;
        }
    }
    private static void SetNumber(GameObject numberIcon, int number)
    {
        numberIcon.name = number.ToString();
        Text text = numberIcon.transform.GetChild(0).GetComponent<Text>();
        text.text = number.ToString();
    }
    [MenuItem(path + "Update number")]
    private static void UpdateNumber()
    {
        GameObject canvasInScene = FindObjectsByType().SingleOrDefault((x) => PrefabUtility.GetCorrespondingObjectFromSource(x) == CanvasPrefab);


        if (canvasInScene == null)
        {
            return;
        }

        GameObject numberIconPrefab = NumberIconPrefab;
        int number = 0;
        foreach (var item in canvasInScene.GetComponentsInChildren<Transform>().
            Where((x) => PrefabUtility.GetCorrespondingObjectFromSource(x.gameObject) == NumberIconPrefab))
        {
            SetNumber(item.gameObject, number++);
        }
    }
}