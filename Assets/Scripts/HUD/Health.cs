using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] GameObject pointIcon;
    [SerializeField] GameObject points;
    [SerializeField] Color filledColor;
    [SerializeField] Color emptyColor;

    private List<Image> images = new List<Image>();

    private float health, lastHealth;
    private float maxHealth, lastMaxHealth;

    void Start()
    {
        lastHealth = health;
        maxHealth = player.maxHealth;
        lastMaxHealth = maxHealth;

        // Add points based on health count
        RefreshPoints();
    }

    // Update is called once per frame
    void Update()
    {
        health = player.health;
        maxHealth = player.maxHealth;
        if (lastHealth != health || lastMaxHealth != maxHealth)
        {
            UpdateHealth();
        }
    }

    private void UpdateHealth()
    {
        // Extended health
        if (lastMaxHealth != maxHealth)
        {
            RefreshPoints();
            lastMaxHealth = maxHealth;
        }

        // Fill all (if healed)
        if (lastHealth < health)
        {
            float healedCount = health;

            foreach (Image image in images)
            {

                if (image.color != filledColor)
                {
                    Image targetImage = image;
                    CanvasGroup canvasGroup = targetImage.transform.GetChild(0).GetComponent<CanvasGroup>();

                    // Flash white
                    LeanTween.value(gameObject, 0f, 1f, 0.25f).setOnUpdate((float val) =>
                    {
                        canvasGroup.alpha = val;
                    }).setOnComplete(() =>
                    {
                        targetImage.color = filledColor; // Change to empty color

                        // Fade out the flash
                        LeanTween.value(gameObject, 1f, 0f, 0.25f).setOnUpdate((float val) =>
                        {
                            canvasGroup.alpha = val;
                        });
                    });
                }

                healedCount--;
                if (healedCount == 0)
                {
                    break;
                }
            }
        }

        // Empty some
        float pointCount = maxHealth - health;
        int i = images.Count - 1;

        while (pointCount > 0)
        {
            if (images[i].color != emptyColor)
            {
                // Flash only on health loss
                if (lastHealth > health)
                {
                    Image targetImage = images[i];
                    CanvasGroup canvasGroup = targetImage.transform.GetChild(0).GetComponent<CanvasGroup>();

                    // Flash white
                    LeanTween.value(gameObject, 0f, 1f, 0.25f).setOnUpdate((float val) =>
                    {
                        canvasGroup.alpha = val;
                    }).setOnComplete(() =>
                    {
                        targetImage.color = emptyColor; // Change to empty color

                        // Fade out the flash
                        LeanTween.value(gameObject, 1f, 0f, 0.25f).setOnUpdate((float val) =>
                        {
                            canvasGroup.alpha = val;
                        });
                    });
                }
                else
                {
                    images[i].color = emptyColor;
                }

            }

            i--;
            pointCount--;
        }

        lastHealth = health;
    }

    private void RefreshPoints()
    {
        // Clear Points
        foreach (Transform point in points.transform)
        {
            Destroy(point.gameObject);
        }

        // Add points based on health
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject newPoint = Instantiate(pointIcon);
            newPoint.transform.SetParent(points.transform, false);
            newPoint.SetActive(true);

            // Add to list
            images.Add(newPoint.GetComponent<Image>());
        }
    }
}
