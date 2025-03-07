using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;
using System.ComponentModel;
public class GameManager : MonoBehaviour
{
    public bool gamefinished = false;
    public GameObject cardPrefab;
    public Sprite cardBackSprite;
    public TextMeshProUGUI WinText;
    public TextMeshProUGUI AiwinText;
    public TextMeshProUGUI AiBustText;
    public TextMeshProUGUI BustText;
    public int score = 0;
    public int ai_score = 0;
    public TextMeshProUGUI aiScoreText;
    public TextMeshProUGUI ScoreText;
    public GameObject card;
    public static GameManager gm;
    public Transform canvas;
    public List<Card> deck = new List<Card>();
    public List<Card> player_deck = new List<Card>();
    public List<Card> ai_deck = new List<Card>();
    public List<Card> player_hand = new List<Card>();
    public List<Card> ai_hand = new List<Card>();
    public List<Card> discard_pile = new List<Card>();

    public Card_data blankCard;

    [SerializeField]private int starting_hand_size = 2;
    [SerializeField]private int ai_hand_size = 1;

    void Awake()
    {
            if (gm != null && gm != this)
            {
                Destroy(gameObject);
            }
            else
            {
                gm = this;
                DontDestroyOnLoad(this.gameObject);
            }

    }
        // Start is called before the first frame update
        void Start()
        {
            //HitText.text="Hit";
            //StandText.text="Stand";
            //Shuffle();
            Deal();
            //aiScoreText = GameObject.FindGameObjectsWithTag("AI Score").GetComponent<TextMeshProUGUI>();
            
        }
    
        // Update is called once per frame
        void Update()
        {
             ai_score = 0;
           score = 0;
            foreach (Card card in player_hand)
            {
                score += card.data.rank;
            }
            
           ScoreText.text = "Score: " + score;
            
            foreach (Card card in ai_hand)
            {
                ai_score += card.data.rank;
            }
            aiScoreText.text = "AI Score: " + ai_score;
            
            if (player_hand.Count > 1)
            {  
                if (score > 17&&score<21&& gamefinished==true)
                {
                    if(ai_score < score)
                    {
                        WinText.transform.position = new Vector3(900,450,0);
                    }
                
                }
                 if ( score > 21)
                  {
                      BustText.transform.position = new Vector3(900,450,0);
                        AiwinText.transform.position = new Vector3(900,600,0);
                }
                if (score == 21)
                {
                    WinText.transform.position = new Vector3(900,450,0);
                }
             }
            if (ai_hand.Count > 1)
            {
                if (ai_score > 17&&ai_score<21)
                
                {
                    if(score < ai_score)
                    {
                        AiwinText.transform.position = new Vector3(900,600,0);
                    }
                }
                if (ai_score > 21)
                {
                    AiBustText.transform.position = new Vector3(900,600,0);
                    WinText.transform.position = new Vector3(900,450,0);
                }
                if (ai_score == 21)
                {
                    AiwinText.transform.position = new Vector3(900,600,0);
                }
            }
                 
               
               
               
        }
    
        void Deal()
        {
            print("Dealing cards");

            
            for (int i = 0; i < starting_hand_size; i++)
            {
                makeACard( -919+(200*player_hand.Count), -456, "player");

            }
            
            for (int i = 0; i < ai_hand_size; i +=1)
            {
                makeACard(-808+(200*ai_hand.Count), 276, "ai");
                //makeACard(-808+(200*ai_hand.Count), 276, "ai");
                //makebackCard(-808+(200*ai_hand.Count), 276, "ai");
            }
            if (ai_score == 21)
                     {
                        AiwinText.transform.position = new Vector3(900,450,0);
                     }
        }
    
        /*void Shuffle()
        {
            for (int i = 0; i < deck.Count; i++)
            {
                Card temp = deck[i];
                int randomIndex = Random.Range(i, deck.Count);
                deck[i] = deck[randomIndex];
                deck[randomIndex] = temp;
            }
        }
        */
     public void Hit()
     {
      
        print("Hit");  
        if (score < 21)
        {
           makeACard(-919+(200*player_hand.Count), -456,"player");
           //makeACard(-808+(200*ai_hand.Count), 276, "ai");
           int aceCount = 0;
            foreach (Card card in ai_hand)
            {
                ai_score += card.data.rank;
                if (card.data.rank == 1) // Assuming rank 1 is Ace
                {
                    aceCount++;
                }
            }
    
            // Adjust for Aces
            while (ai_score > 21 && aceCount > 0)
            {
                ai_score -= 10;
                aceCount--;
            }
    
            if (ai_score > 21)
            {
                return;
            }
        }
       
        
            
           
           
            
            
     }
    public void Stand()
    {
        
        while (ai_score < 21)
        {
            if (ai_score > 17&&ai_score<21)
            {
                gamefinished=true;
                break;
               if(score < ai_score) 
               {
                   
               }
            }
            
               
            
            makeACard(-808 + (200 * ai_hand.Count), 276, "ai");
            ai_score = 0;
    
            int aceCount = 0;
            foreach (Card card in ai_hand)
            {
                ai_score += card.data.rank;
                if (card.data.rank == 1) // Assuming rank 1 is Ace
                {
                    aceCount++;
                }
            }
    
            // Adjust for Aces
            while (ai_score > 21 && aceCount > 0)
            {
                ai_score -= 10;
                aceCount--;
            }
    
            if (ai_score > 21)
            {
                return;
            }
        }
    }

     void makeACard(int x,int y, string player)
    {
       // print("player_hand.Count: " + player_hand.Count);
        int randomNumber = Random.Range(0, deck.Count);
        Card cardData = deck[randomNumber];
        if (player == "player")
        {
             player_hand.Add(cardData);
        
        
        
                 GameObject makeCard = Instantiate(card, new Vector3(x, y, 0), Quaternion.identity);
                 Card cardComponent = makeCard.GetComponent<Card>();
                 cardComponent.data=cardData.data;
                 cardComponent.SetAceRank(score);
                 makeCard.transform.SetParent(canvas, false); // Set the parent to the canvas
                 score+= cardComponent.rank; // Update the player's score
                 deck.RemoveAt(randomNumber);
                      //print("rank for card is "+makeCard.GetComponent<Card>().rank);
                 score += makeCard.GetComponent<Card>().rank;

                 int aceCount = 0;
                   foreach (Card card in player_hand)
                     {
                           if (card.rank == 1) // Assuming rank 1 is Ace
                          {
                                aceCount++;
                          }
                      }
                        while (score > 21 && aceCount > 0)
                         {
                           score -= 10;
                              aceCount--;
                        }
        }
    else
    {
         ai_hand.Add(cardData);
        GameObject makeCard = Instantiate(card, new Vector3(x, y, 0), Quaternion.identity);
        Card cardComponent = makeCard.GetComponent<Card>();
        cardComponent.data = cardData.data;
        makeCard.transform.SetParent(canvas, false); // Set the parent to the canvas
        ai_score += cardComponent.rank; // Update the AI's score
        deck.RemoveAt(randomNumber);

        // Adjust for Aces
        int aceCount = 0;
        foreach (Card card in ai_hand)
        {
            if (card.rank == 1) // Assuming rank 1 is Ace
            {
                aceCount++;
            }
        }
        while (ai_score > 21 && aceCount > 0)
        {
            ai_score -= 10;
            aceCount--;
        }
    }
    }
         void makebackCard(int x, int y, string player)
         {
                 GameObject makethebackCard = Instantiate(cardPrefab, new Vector3(x, y, 0), Quaternion.identity);
                 makethebackCard.transform.SetParent(canvas, false); // Set the parent to the canvas
                 Image cardImage = makethebackCard.GetComponent<Image>();
                 if (cardImage != null)
                     {
                        cardImage.sprite = cardBackSprite; // Set the card back sprite
                     }
                    int randomNumber = Random.Range(0, deck.Count);
                      ai_hand.Add(deck[randomNumber]);
                     GameObject makeCard = Instantiate(cardPrefab, new Vector3(x, y, 0), Quaternion.identity);
                      makeCard.GetComponent<Card>().data = deck[randomNumber].data;
                    makeCard.transform.SetParent(canvas, false); // Set the parent to the canvas
                  deck.RemoveAt(randomNumber);
                     //print("rank for card is "+makeCard.GetComponent<Card>().rank);
                     ai_score += makeCard.GetComponent<Card>().rank;
                  makeCard.SetActive(false);
        }
}
    
