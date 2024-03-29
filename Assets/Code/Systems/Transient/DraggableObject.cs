using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraggableObject
{
    private Image image;

    public DraggableObject(GameObject levelObj, string name, int currentSlot, DropLocationList dropLocationList, Vector2 position) {
        InterfaceTool.ImgSetup(name, levelObj.transform, out image, SysManager.sprites[9], true);
        InterfaceTool.FormatRect(image.rectTransform, new Vector2(150, 150), position);
        Text text = InterfaceTool.CreateHeader(name, GetTF(), new Vector2(0, 60), new Vector2(0, -110), 24);
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.black;
        SysManager.tooltip.AssignTooltip(image.transform);
        new DragAndSnapEvent(image, dropLocationList, currentSlot);
    }

    public Transform GetTF() {
        return image.transform;
    }

    public void SetPosition(Vector2 vec) {
        image.rectTransform.anchoredPosition = vec;
    }
}
