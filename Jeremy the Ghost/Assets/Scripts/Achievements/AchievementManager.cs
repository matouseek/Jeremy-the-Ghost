using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }
    [SerializeField] private List<Achievement> _achievements;
    public List<Achievement> Achievements => _achievements;
    [SerializeField] public List<GameObject> AchievementBoxes; // Boxes in achievement menu

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this;
        } 
    }

    public void LoadAchievements()
    {
        for (int i = 0; i < AchievementBoxes.Count; ++i)
        {
            var achievementBoxTransform = AchievementBoxes[i].transform;
            var achievementScriptableObject = _achievements[i];
            
            // Insert the corresponding name
            achievementBoxTransform.Find("Name")
                .GetComponent<TextMeshProUGUI>().text = achievementScriptableObject.Name;
            
            // Insert the corresponding description
            achievementBoxTransform.Find("Description")
                .GetComponent<TextMeshProUGUI>().text = achievementScriptableObject.Description;
            
            // Set whether achievement is completed
            achievementBoxTransform.Find("CompletedText")
                .gameObject.SetActive(achievementScriptableObject.Completed);

            var rewardTransform = achievementBoxTransform.Find("Reward");
            if (achievementScriptableObject.EyeRewardSprite == null)
            {
                rewardTransform.gameObject.SetActive(false);
                continue;
            }
            achievementBoxTransform.Find("Reward").Find("RewardImage")
                .GetComponent<Image>().sprite = achievementScriptableObject.EyeRewardSprite;
        }
    }
}
