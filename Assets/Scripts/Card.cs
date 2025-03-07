using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Card_data data;

    public string value;
    public string value2;
    public string suit;
    public int rank;
    public int cost;
    public int damage;
    public Sprite sprite;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI damageText;
    public Image spriteImage;
        
    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();        value = data.value;
        suit = data.suit;
        rank = data.rank;
        value2 = data.value2;
        sprite = data.sprite;
        nameText.text = value;
        descriptionText.text = suit;
        healthText.text = rank.ToString();
        costText.text = cost.ToString();
        damageText.text = value2.ToString();
        spriteImage.sprite = sprite;

        SetAceRank(gm.score);

    }

    // Update is called once per frame
    
    
        public void SetAceRank(int score)
        {
            if (data.rank == 11) // Assuming rank 11 is Ace
            {
                if (score + 11 <= 21)
                {
                    rank = 11;
                }
                else
                {
                    rank = 1;
                }
                
            }
        }
    
}
