using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemBar : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    
    private List<VisualElement> slotImages = new List<VisualElement>();
    private List<Label> slotLabels = new List<Label>();
    
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        slotImages = root.Query<VisualElement>(className: "ItemSlotImage").ToList();
        slotLabels = root.Query<Label>(className: "ItemSlotLabel").ToList();
        
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (var i = 0; i < slotImages.Count; ++i)
        {
            if (playerInventory.HasIndex(i))
            {
                var sprite = playerInventory.GetSpriteIndex(i);

                if (sprite != null)
                {
                    slotImages[i].style.backgroundImage =
                        new StyleBackground(sprite.texture);
                }
                else
                {
                    slotImages[i].style.backgroundImage = StyleKeyword.None;
                }
                slotLabels[i].text = playerInventory.GetCountIndex(i).ToString();
                
                slotImages[i].style.display = DisplayStyle.Flex;
                slotLabels[i].style.display = DisplayStyle.Flex;
            }
            else
            {
                slotImages[i].style.display = DisplayStyle.None;
                slotLabels[i].style.display = DisplayStyle.None;
            }
        }
    }
}
