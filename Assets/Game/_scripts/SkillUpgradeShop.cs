using UnityEngine;
using UnityEngine.UI;

public class SkillUpgradeShop : MonoBehaviour
{
    public int controllabilityCost = 25; // Стоимость прокачки Controllability
    public int immunityCost = 50;       // Стоимость прокачки Immunity

    public Button controllabilityButton; // Кнопка прокачки Controllability
    public Button immunityButton;        // Кнопка прокачки Immunity

    public GameObject controllabilityBar; // Бар для Controllability
    public GameObject immunityBar;        // Бар для Immunity

    public Text coinsText;                // Текст для отображения количества монет

    private int coins;                    // Количество монет
    private float controllability;        // Текущее значение Controllability
    private float immunity;               // Текущее значение Immunity

    private const float skillDecrease = 0.1f; // Уменьшение значения скилла при прокачке
    private const int maxSkillLevel = 13;     // Максимальный уровень для каждого скилла

    private void Start()
    {
        // Загружаем начальные данные из PlayerPrefs
        coins = PlayerPrefs.GetInt("Coins", 0);
        controllability = PlayerPrefs.GetFloat("Controllability", 3.0f);
        immunity = PlayerPrefs.GetFloat("Immunity", 3.0f);

        // Обновляем отображение монет
        UpdateCoinsText();

        // Настраиваем бары и кнопки
        SetupSkillBar(controllabilityBar, "ControllabilityLevel", controllabilityButton, controllabilityCost);
        SetupSkillBar(immunityBar, "ImmunityLevel", immunityButton, immunityCost);
    }

    private void SetupSkillBar(GameObject bar, string skillLevelKey, Button button, int cost)
    {
        int skillLevel = PlayerPrefs.GetInt(skillLevelKey, 1); // Уровень по умолчанию — 1
        UpdateBar(bar, skillLevel);

        if (coins < cost)
        {
            // Недостаточно монет
            button.interactable = false;
            button.transform.GetChild(0).gameObject.SetActive(true); // Включаем индикатор "недостаточно"
            button.transform.GetChild(1).gameObject.SetActive(false); // Отключаем индикатор "максимум"
        }
        else if (skillLevel >= maxSkillLevel)
        {
            // Достигнут максимальный уровень
            button.interactable = false;
            button.transform.GetChild(1).gameObject.SetActive(true); // Включаем индикатор "максимум"
            button.transform.GetChild(0).gameObject.SetActive(false); // Отключаем индикатор "недостаточно"
        }
        else
        {
            button.interactable = true;
            button.transform.GetChild(0).gameObject.SetActive(false); // Отключаем индикаторы
            button.transform.GetChild(1).gameObject.SetActive(false);
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
            // Обновляем уровень скилла
            skillLevel++;
            PlayerPrefs.SetInt(skillLevelKey, skillLevel);

            // Уменьшаем значение скилла
            skillValue -= skillDecrease;
            PlayerPrefs.SetFloat(skillLevelKey.Replace("Level", ""), skillValue);

            // Обновляем бар
            UpdateBar(bar, skillLevel);

            // Вычитаем монеты
            coins -= cost;
            PlayerPrefs.SetInt("Coins", coins);
            UpdateCoinsText();

            // Проверяем, достигнут ли максимальный уровень
            if (skillLevel >= maxSkillLevel)
            {
                button.interactable = false;
                button.transform.GetChild(1).gameObject.SetActive(true); // Включаем индикатор "максимум"
            }
        }

        // Проверяем состояние кнопок для всех скиллов
        SetupSkillBar(controllabilityBar, "ControllabilityLevel", controllabilityButton, controllabilityCost);
        SetupSkillBar(immunityBar, "ImmunityLevel", immunityButton, immunityCost);
    }

    private void UpdateBar(GameObject bar, int skillLevel)
    {
        for (int i = 0; i < bar.transform.childCount; i++)
        {
            var child = bar.transform.GetChild(i);
            var color = child.GetComponent<Image>().color;
            color.a = i < skillLevel ? 1 : 0; // Открытые уровни делаем видимыми
            child.GetComponent<Image>().color = color;
        }
    }

    private void UpdateCoinsText()
    {
        coinsText.text = $"{coins}";
    }
}
