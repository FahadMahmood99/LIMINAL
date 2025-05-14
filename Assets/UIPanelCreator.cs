using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPanelCreator : MonoBehaviour
{
    [Header("Panel Settings")]
    public Color panelColor = new Color(0, 0, 0, 0.9f);
    public Color buttonColor = new Color(0.2f, 0.2f, 0.2f, 1f);
    public Color buttonHoverColor = new Color(0.3f, 0.3f, 0.3f, 1f);
    public Color textColor = Color.white;
    public int fontSize = 36;
    public int buttonFontSize = 24;
    public Vector2 buttonSize = new Vector2(200, 50);
    public float buttonSpacing = 20f;

    [Header("References")]
    public Canvas mainCanvas;
    public string prefabPath = "Assets/Prefabs/UI/";

    public void CreateMainMenuPanel()
    {
        GameObject panel = CreatePanel("MainMenuPanel");
        AddTitle(panel, "Main Menu", new Vector2(0, 100));
        AddButton(panel, "StartButton", "Start Game", new Vector2(0, 0));
        AddButton(panel, "QuitButton", "Quit Game", new Vector2(0, -buttonSize.y - buttonSpacing));
        SaveAsPrefab(panel, "MainMenuPanel");
    }

    public void CreatePauseMenuPanel()
    {
        GameObject panel = CreatePanel("PauseMenuPanel");
        AddTitle(panel, "Paused", new Vector2(0, 100));
        AddButton(panel, "ResumeButton", "Resume", new Vector2(0, 0));
        AddButton(panel, "MainMenuButton", "Main Menu", new Vector2(0, -buttonSize.y - buttonSpacing));
        AddButton(panel, "QuitButton", "Quit Game", new Vector2(0, -2 * (buttonSize.y + buttonSpacing)));
        SaveAsPrefab(panel, "PauseMenuPanel");
    }

    public void CreateSplashScreenPanel()
    {
        GameObject panel = CreatePanel("SplashScreenPanel");
        AddTitle(panel, "Game Title", new Vector2(0, 100));
        AddSubtitle(panel, "Press any key to continue", new Vector2(0, -50));
        SaveAsPrefab(panel, "SplashScreenPanel");
    }

    public void CreateGameOverPanel()
    {
        GameObject panel = CreatePanel("GameOverPanel");
        AddTitle(panel, "Game Over", new Vector2(0, 100));
        AddText(panel, "GameOverText", "You've Escaped!", new Vector2(0, 0));
        AddButton(panel, "RestartButton", "Restart", new Vector2(0, -100));
        AddButton(panel, "MainMenuButton", "Main Menu", new Vector2(0, -100 - buttonSize.y - buttonSpacing));
        SaveAsPrefab(panel, "GameOverPanel");
    }

    private GameObject CreatePanel(string name)
    {
        GameObject panel = new GameObject(name);
        panel.transform.SetParent(mainCanvas.transform, false);

        RectTransform rectTransform = panel.AddComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        Image image = panel.AddComponent<Image>();
        image.color = panelColor;

        return panel;
    }

    private void AddTitle(GameObject panel, string text, Vector2 position)
    {
        GameObject titleObj = new GameObject("Title");
        titleObj.transform.SetParent(panel.transform, false);

        RectTransform rectTransform = titleObj.AddComponent<RectTransform>();
        rectTransform.anchoredPosition = position;
        rectTransform.sizeDelta = new Vector2(400, 100);

        TMP_Text tmpText = titleObj.AddComponent<TextMeshProUGUI>();
        tmpText.text = text;
        tmpText.fontSize = fontSize;
        tmpText.color = textColor;
        tmpText.alignment = TextAlignmentOptions.Center;
    }

    private void AddSubtitle(GameObject panel, string text, Vector2 position)
    {
        GameObject subtitleObj = new GameObject("Subtitle");
        subtitleObj.transform.SetParent(panel.transform, false);

        RectTransform rectTransform = subtitleObj.AddComponent<RectTransform>();
        rectTransform.anchoredPosition = position;
        rectTransform.sizeDelta = new Vector2(400, 50);

        TMP_Text tmpText = subtitleObj.AddComponent<TextMeshProUGUI>();
        tmpText.text = text;
        tmpText.fontSize = buttonFontSize;
        tmpText.color = textColor;
        tmpText.alignment = TextAlignmentOptions.Center;
    }

    private void AddText(GameObject panel, string name, string text, Vector2 position)
    {
        GameObject textObj = new GameObject(name);
        textObj.transform.SetParent(panel.transform, false);

        RectTransform rectTransform = textObj.AddComponent<RectTransform>();
        rectTransform.anchoredPosition = position;
        rectTransform.sizeDelta = new Vector2(400, 50);

        TMP_Text tmpText = textObj.AddComponent<TextMeshProUGUI>();
        tmpText.text = text;
        tmpText.fontSize = buttonFontSize;
        tmpText.color = textColor;
        tmpText.alignment = TextAlignmentOptions.Center;
    }

    private void AddButton(GameObject panel, string name, string text, Vector2 position)
    {
        GameObject buttonObj = new GameObject(name);
        buttonObj.transform.SetParent(panel.transform, false);

        RectTransform rectTransform = buttonObj.AddComponent<RectTransform>();
        rectTransform.anchoredPosition = position;
        rectTransform.sizeDelta = buttonSize;

        Image image = buttonObj.AddComponent<Image>();
        image.color = buttonColor;

        Button button = buttonObj.AddComponent<Button>();
        ColorBlock colors = button.colors;
        colors.highlightedColor = buttonHoverColor;
        button.colors = colors;

        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(buttonObj.transform, false);

        RectTransform textRectTransform = textObj.AddComponent<RectTransform>();
        textRectTransform.anchorMin = Vector2.zero;
        textRectTransform.anchorMax = Vector2.one;
        textRectTransform.offsetMin = Vector2.zero;
        textRectTransform.offsetMax = Vector2.zero;

        TMP_Text tmpText = textObj.AddComponent<TextMeshProUGUI>();
        tmpText.text = text;
        tmpText.fontSize = buttonFontSize;
        tmpText.color = textColor;
        tmpText.alignment = TextAlignmentOptions.Center;
    }

    private void SaveAsPrefab(GameObject panel, string name)
    {
        #if UNITY_EDITOR
            if (!System.IO.Directory.Exists(prefabPath))
            {
                System.IO.Directory.CreateDirectory(prefabPath);
            }
            UnityEditor.PrefabUtility.SaveAsPrefabAsset(panel, prefabPath + name + ".prefab");
            DestroyImmediate(panel);
        #endif
    }
} 