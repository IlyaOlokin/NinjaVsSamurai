using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private float swipeSpeed;
    [SerializeField] private Transform targetDist;
    [SerializeField] private int PageCount;
    [SerializeField] private PageIndicatorManager pim;
    [SerializeField] private GameObject[] levelButtons;
    [SerializeField] private Sprite activatedSprite;
    [SerializeField] private Sprite notCompletedSprite;
    [SerializeField] private float deactivatedButtonAlpha;
    [SerializeField] private Color completedTextColor;
    [SerializeField] private Color notCompletedTextColor;

    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;
    [SerializeField] private float deactivatedArrowAlpha;
    
    [SerializeField] private GameObject[] worldBlocks;

        
    
    private float dist;
    private int currentPage = 1;
    private bool needToMove = false;
    private float targetXPos = 0;

    private void Start()
    {
        dist = Math.Abs(targetDist.position.x - transform.position.x);
        ActivateLevelButtons(LevelsCompleted());
        ResetArrows(currentPage);
        ResetWorldBlocks();
    }

    private void Update()
    {
        needToMove = transform.position.x != targetXPos;
    }

    private void FixedUpdate()
    {
        if (needToMove)
        {
            transform.position = Vector2.MoveTowards(transform.position,
            new Vector2(targetXPos, transform.position.y), swipeSpeed);
        }
    }

    public void StartSwipeRight()
    {
        if (currentPage == PageCount) return;
        targetXPos -= dist;
        needToMove = true;
        currentPage += 1;
        pim.SetIndicatorTargetPosition(currentPage);
        ResetArrows(currentPage);
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void StartSwipeLeft()
    {
        if (currentPage == 1) return;
        targetXPos += dist;
        needToMove = true;
        currentPage -= 1;
        pim.SetIndicatorTargetPosition(currentPage);
        ResetArrows(currentPage);
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    private void ActivateLevelButtons(int levelsCompleted)
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            var currentImage = levelButtons[i].GetComponent<Image>();
            var currentButton = levelButtons[i].GetComponent<Button>();
            if (i < levelsCompleted - 1)
            {
                currentImage.sprite = activatedSprite;
                SetButtonTextColor(currentButton, true);
                SetImageAlpha(currentImage, 1, true);
                SetActiveButton(currentButton, true);

            }
            else if (i == levelsCompleted - 1)
            {
                currentImage.sprite = notCompletedSprite;
                SetButtonTextColor(currentButton, false);
                SetImageAlpha(currentImage, 1, true);
                SetActiveButton(currentButton, true);
            }
            else
            {
                currentImage.sprite = notCompletedSprite;
                SetButtonTextColor(currentButton, false);
                SetImageAlpha(currentImage, deactivatedButtonAlpha, true);
                SetActiveButton(currentButton, false);
            }
        }
    }

    private void SetImageAlpha(Image currentImage, float alpha, bool hasText = false)
    {
        currentImage.color = new Color(currentImage.color.r, currentImage.color.g, currentImage.color.b, alpha);
        if (!hasText) return;
        var text = currentImage.transform.GetComponentInChildren<Text>();
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }

    private void SetButtonTextColor(Button button, bool completed)
    {
        button.transform.GetComponentInChildren<Text>().color = completed ? completedTextColor : notCompletedTextColor;
    }

    private void SetActiveButton(Button button, bool enabled)
    {
        button.enabled = enabled;
    }

    private int LevelsCompleted()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (StarSystem.GetLevelStars(i + 1) == 0)
            {
                return i + 1;
            }
        }

        return levelButtons.Length + 1;
    }

    private void ResetArrows(int currentPage)
    {
        var right = rightArrow.GetComponent<Image>();
        var left = leftArrow.GetComponent<Image>();
        
        if (currentPage == 1)
        {
            SetImageAlpha(left, deactivatedArrowAlpha);
            leftArrow.GetComponent<Button>().enabled = false;
            
            SetImageAlpha(right, 1);
            rightArrow.GetComponent<Button>().enabled = true;
        }
        else if (currentPage == PageCount)
        {
            SetImageAlpha(left, 1);
            leftArrow.GetComponent<Button>().enabled = true;

            SetImageAlpha(right, deactivatedArrowAlpha);
            rightArrow.GetComponent<Button>().enabled = false;
        }
        else
        {
            SetImageAlpha(left, 1);
            leftArrow.GetComponent<Button>().enabled = true;
            
            SetImageAlpha(right, 1);
            rightArrow.GetComponent<Button>().enabled = true;
        }
    }

    private void ResetWorldBlocks()
    {
        for (int i = 0; i < worldBlocks.Length; i++)
        {
            worldBlocks[i].GetComponent<WorldBlockStars>().SetText(StarSystem.GetAllWorldBarriers()[i]);
            if (StarSystem.GetCurrentStarsAmount() >= StarSystem.GetAllWorldBarriers()[i])
            {
                worldBlocks[i].SetActive(false);
            }
        }
    }
}
