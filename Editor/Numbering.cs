using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Numbering
{
    private const string path = "DirectionUtility/";



    [MenuItem(path + "Add number icon")]
    private static void AddNumberIcon()
    {
        Canvas canvas = Object.FindFirstObjectByType<Canvas>();

        if (canvas == null)
        {
            canvas = CreateCanvas();
        }

        Debug.Log(AssetDatabase.GUIDToAssetPath("a410fe040b94f9d468330865e410538d"));
    }
    private static Canvas CreateCanvas()
    {
        GameObject canvasObject = ObjectFactory.CreateGameObject("Canvas", typeof(Canvas), typeof(CanvasScaler));
        Canvas canvas = canvasObject.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        return canvas;
    }
    private static void SetNumber(GameObject numberIcon, int number)
    {
        Text text = numberIcon.transform.GetChild(0).GetComponent<Text>();
        text.text = number.ToString();
    }
}