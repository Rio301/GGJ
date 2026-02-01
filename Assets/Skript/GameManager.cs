using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI (scene-dependent)")]
    public TextMeshProUGUI difficultyText;

    [Header("Game State")]
    public int difficultyLevel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        difficultyLevel = 0;
        ChangeText();
    }

    void ChangeText()
    {
        if (difficultyText == null) return; // prevents crash across scenes

        switch (difficultyLevel)
        {
            case 0: difficultyText.text = "easy"; break;
            case 1: difficultyText.text = "medium"; break;
            case 2: difficultyText.text = "hard"; break;
            case 3: difficultyText.text = "extreme"; break;
        }

        Data.difficultyLevel = difficultyLevel;
    }

    public void addDifficultyLevel()
    {
        difficultyLevel = (difficultyLevel + 1) % 4;
        ChangeText();
    }

    public void minDifficultyLevel()
    {
        difficultyLevel = (difficultyLevel - 1 + 4) % 4;
        ChangeText();
    }
}
