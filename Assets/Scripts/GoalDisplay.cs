using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GoalDisplay : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _seasonsSprites;
    [SerializeField]
    private Sprite[] _menuSprites;
    [SerializeField]
    private Sprite[] _windowSprites;

    [SerializeField]
    private Image _seasonDisplay;
    [SerializeField]
    private Image _menuDisplay;
    [SerializeField]
    private Image _windowDisplay;

    [SerializeField]
    private TextMeshProUGUI _seasonText;
    private string[] seasonsText = { "Printemps", "�t�", "Automne", "Hiver" };

    private int newSeasonGoal;
    private int seasonGoalBase = 50;
    private float seasonGoal;

    public int CurrentIndex;
    public int PlayerGoalAmount;

    [SerializeField]
    private TextMeshProUGUI _goalText;
    private bool goalCompleted = false;

    void Start()
    {
        GameManager.Instance.gestionResource = FindObjectOfType<ResourceGestion>();

        PlayerGoalAmount = 0;
        _seasonText.text = seasonsText[CurrentIndex];
        newSeasonGoal = seasonGoalBase;

        GameManager.Instance.gestionResource.ChangeRandomResourcesList();
    }
    void Update()
    {
        if (goalCompleted == false)
        {
            if (newSeasonGoal >= 1000)
            {
                _goalText.text = PlayerGoalAmount.ToString("") + " / " + (newSeasonGoal / 1000) + " k";
            }
            else if (newSeasonGoal >= 1000 && PlayerGoalAmount >= 1000)
            {
                _goalText.text = (PlayerGoalAmount / 1000) + " k / " + (newSeasonGoal/1000) + " k";
            }
            else
            {
                _goalText.text = PlayerGoalAmount.ToString("") + " / " + newSeasonGoal.ToString("");
            }

            if (PlayerGoalAmount >= newSeasonGoal)
            {
                goalCompleted = true;
                DisplaySeason();
            }
        }
    }

    private void DisplaySeason()
    {
        GameManager.Instance.gestionResource.ChangeRandomResourcesList();

        PlayerGoalAmount = 0;

        CurrentIndex += 1;
        seasonGoal = seasonGoalBase * (Mathf.Pow(1.5f, (CurrentIndex)));
        newSeasonGoal = (int)seasonGoal;

        _seasonText.text = seasonsText[CurrentIndex];
        _seasonDisplay.sprite = _seasonsSprites[CurrentIndex];
        _menuDisplay.sprite = _menuSprites[CurrentIndex];
        _windowDisplay.sprite = _windowSprites[CurrentIndex];

        ClickableObject[] allClickers = FindObjectsOfType<ClickableObject>();

        foreach (ClickableObject clicker in allClickers)
        {
            clicker.ResetStats();
        }

        GameManager.Instance.LouisCompetence += 0.4f;
        if (GameManager.Instance.JulesV2)
        {
            GameManager.Instance.JulesCompetence += 0.4f;
        }

        goalCompleted = false;
    }
}