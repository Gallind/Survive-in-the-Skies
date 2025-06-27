
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class BuySlotTests
{
    [Test]
    public void BuySlot_IsAvailable_SetsAvailableUI()
    {
        // Arrange
        GameObject buySlotObject = new GameObject();
        BuySlot buySlot = buySlotObject.AddComponent<BuySlot>();
        Image image = buySlotObject.AddComponent<Image>();
        Button button = buySlotObject.AddComponent<Button>();

        Sprite availableSprite = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 4, 4), Vector2.zero);
        Sprite unAvailableSprite = Sprite.Create(Texture2D.blackTexture, new Rect(0, 0, 4, 4), Vector2.zero);

        buySlot.availableSprite = availableSprite;
        buySlot.unAvailableSprite = unAvailableSprite;
        buySlot.isAvailable = true;

        // Act
        // Use reflection to call the private Start method, which calls UpdateAvailabilityUI
        MethodInfo updateUIMethod = typeof(BuySlot).GetMethod("UpdateAvailabilityUI", BindingFlags.NonPublic | BindingFlags.Instance);
        updateUIMethod.Invoke(buySlot, null);

        // Assert
        Assert.AreEqual(availableSprite, image.sprite);
        Assert.IsTrue(button.interactable);
    }

    [Test]
    public void BuySlot_IsUnavailable_SetsUnavailableUI()
    {
        // Arrange
        GameObject buySlotObject = new GameObject();
        BuySlot buySlot = buySlotObject.AddComponent<BuySlot>();
        Image image = buySlotObject.AddComponent<Image>();
        Button button = buySlotObject.AddComponent<Button>();

        Sprite availableSprite = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 4, 4), Vector2.zero);
        Sprite unAvailableSprite = Sprite.Create(Texture2D.blackTexture, new Rect(0, 0, 4, 4), Vector2.zero);

        buySlot.availableSprite = availableSprite;
        buySlot.unAvailableSprite = unAvailableSprite;
        buySlot.isAvailable = false;

        // Act
        MethodInfo updateUIMethod = typeof(BuySlot).GetMethod("UpdateAvailabilityUI", BindingFlags.NonPublic | BindingFlags.Instance);
        updateUIMethod.Invoke(buySlot, null);

        // Assert
        Assert.AreEqual(unAvailableSprite, image.sprite);
        Assert.IsFalse(button.interactable);
    }
}
