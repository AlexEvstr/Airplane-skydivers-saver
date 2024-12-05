using UnityEngine;
using UnityEngine.UI;

public class SkillUpgradeShop : MonoBehaviour
{
    public int controllabilityCost = 25;
    public int immunityCost = 50;

    public Button controllabilityButton;
    public Button immunityButton;

    public GameObject controllabilityBar;
    public GameObject immunityBar;

    public Text coinsText;

    private int coins;
    private float controllability;
    private float immunity;

    private const float skillDecrease = 0.1f;
    private const int maxSkillLevel = 13;

    private void Start()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
        controllability = PlayerPrefs.GetFloat("Controllability", 3.0f);
        immunity = PlayerPrefs.GetFloat("Immunity", 3.0f);

        UpdateCoinsText();

        SetupSkillBar(controllabilityBar, "ControllabilityLevel", controllabilityButton, controllabilityCost);
        SetupSkillBar(immunityBar, "ImmunityLevel", immunityButton, immunityCost);
    }

    private void SetupSkillBar(GameObject bar, string skillLevelKey, Button button, int cost)
    {
        int skillLevel = PlayerPrefs.GetInt(skillLevelKey, 1);
        UpdateBar(bar, skillLevel);

        if (coins < cost)
        {
            button.interactable = false;
            button.transform.GetChild(0).gameObject.SetActive(true);
            button.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (skillLevel >= maxSkillLevel)
        {
            button.interactable = false;
            button.transform.GetChild(1).gameObject.SetActive(true);
            button.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            button.interactable = true;
            button.transform.GetChild(0).gameObject.SetActive(false);
            button.transform.GetChild(1).gameObject.SetActive(false);
        }

        if (skillLevel >= maxSkillLevel)
        {
            button.interactable = false;
            button.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void UpgradeControllability()
    {
        UpgradeSkill(controllabilityBar, "ControllabilityLevel", ref controllability, controllabilityButton, controllabilityCost);
    }

    public void UpgradeImmunity()
    {
        UpgradeSkill(immunityBar, "ImmunityLevel", ref immunity, immunityButton, immunityCost);
    }

    private void UpgradeSkill(GameObject bar, string skillLevelKey, ref float skillValue, Button button, int cost)
    {
        int skillLevel = PlayerPrefs.GetInt(skillLevelKey, 1);

        if (coins >= cost && skillLevel < maxSkillLevel)
        {
            skillLevel++;
            PlayerPrefs.SetInt(skillLevelKey, skillLevel);

            skillValue -= skillDecrease;
            PlayerPrefs.SetFloat(skillLevelKey.Replace("Level", ""), skillValue);
            UpdateBar(bar, skillLevel);

            coins -= cost;
            PlayerPrefs.SetInt("Coins", coins);
            UpdateCoinsText();
        }

        SetupSkillBar(controllabilityBar, "ControllabilityLevel", controllabilityButton, controllabilityCost);
        SetupSkillBar(immunityBar, "ImmunityLevel", immunityButton, immunityCost);

        if (skillLevel >= maxSkillLevel)
        {
            button.interactable = false;
            button.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void UpdateBar(GameObject bar, int skillLevel)
    {
        for (int i = 0; i < bar.transform.childCount; i++)
        {
            var child = bar.transform.GetChild(i);
            var color = child.GetComponent<Image>().color;
            color.a = i < skillLevel ? 1 : 0;
            child.GetComponent<Image>().color = color;
        }
    }

    private void UpdateCoinsText()
    {
        coinsText.text = $"{coins}";
    }
}