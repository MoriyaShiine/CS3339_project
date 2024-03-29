using UnityEngine;
using UnityEngine.UI;

public class SysManager
{
    public static Camera mainCam;
    public static Canvas canvas;
    public static GameObject canvasObj, quitObj;
    public static readonly Font DEFAULT_FONT;
    public static readonly Sprite DEFAULT_BUTTON;
    public static Sprite[] sprites;
    public static Level currentLevel;
    public static Tooltip tooltip;

    static SysManager()
    {
        DEFAULT_FONT = Resources.GetBuiltinResource<Font>(
            "Arial.ttf");
        DEFAULT_BUTTON = null;
        sprites = Resources.LoadAll<Sprite>("Graphics");

        InterfaceTool.defaultFont = DEFAULT_FONT;
    }

    [RuntimeInitializeOnLoadMethod]
    static void StartApplication()
    {
        mainCam = new GameObject("Main Camera")
            .AddComponent<Camera>();
        mainCam.tag = "MainCamera";
        mainCam.orthographic = true;
        mainCam.backgroundColor = new Color(0.2f, 0.2f, 0.5f);
        mainCam.gameObject.AddComponent<AnimatedCamera>();

        InitializeCanvas();
    }

    public static void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }

    public static void SetLevel(Level level) {
        if (currentLevel != null) {
            currentLevel.Destroy();
        }
        currentLevel = level;
        // go to main menu
        if (level == null) {
            GameObject.Destroy(canvasObj);
            InitializeCanvas();
        }
    }

    public static Level GetStage1() {
        return new Stage1();
    }

    public static Level GetStage2() {
        return new Stage2(true, false, false, true, false, true, false);
    }

    public static Level GetStage3() {
        return new Stage3();
    }

    private static void InitializeCanvas() {
        canvasObj = InterfaceTool.CanvasSetup(
            "Main Canvas", null, out canvas);
        canvasObj.AddComponent<MainMenu>();
        tooltip = new Tooltip(canvas.transform);
        CreateQuitButton();
    }

    private static void CreateQuitButton()
    {
        quitObj = InterfaceTool.ButtonSetup("Quit", canvas.transform,
            out Image quitImg, out Button quitButton, null, () => {
            SetLevel(null);
        });
        InterfaceTool.FormatRect(quitImg.rectTransform,
            new Vector2(180, 60), Level.DEF_VEC,
            Level.DEF_VEC, Level.DEF_VEC,
            new Vector2(-825, 475));
        quitImg.color = new Color(0.3F, 0.3F, 0.3F);
        Text quitText = InterfaceTool.CreateHeader("Quit",
            quitImg.transform, new Vector2(0, 40),
            new Vector2(0, -50), 24);
        quitText.alignment = TextAnchor.MiddleCenter;
        quitText.color = Color.black;
        quitObj.SetActive(false);
    }
}
