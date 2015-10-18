using UnityEngine;
using System.Collections;

internal class Deck : MonoBehaviour
{
    System.Collections.Generic.List<int> deck = new System.Collections.Generic.List<int>();
    /*
    Колода карт из 52 листов. 
    0 джокер
    1-13  - черви
    14-26 - буби
    27-39 - трефы
    40-52 - пики
   */

    public Deck()
    {
        NewDeck();
    }

    private void NewDeck()
    {
        deck.Clear();
        for (int i = 0; i <= 52; i++)
        {
            deck.Add(i);
        }
    }

    public int getCard()
    {
        if (deck.Count == 0)
        {
            return 0;
        }
        int cardNum = Random.Range(0, deck.Count);
        int rv = deck[cardNum];
        deck.RemoveAt(cardNum);
        return rv;
    }

    public static bool CompareCards(int _firstCard, int _secondCard)
    {
        if (_firstCard == 0)
        {
            return true; //Джокер всегда больше;
        }
        if (((_firstCard % 13 == 1) || (_firstCard % 13 == 0)) && (_firstCard % 13 < _secondCard % 13))
        {
            return true; //Туз - остаток 1, или король остаток 0
        }

        if (((_secondCard % 13 == 1) || (_secondCard % 13 == 0)) && (_secondCard % 13 < _firstCard % 13))
        {
            return false; //Туз - остаток 1, или король остаток 0
        }

        if (_firstCard % 13 > _secondCard % 13)
        {
            return true; //значение первой карты больше
        }

        if (_firstCard % 13 == _secondCard % 13)
        {
            return _firstCard < _secondCard; //масть первой карты выше
        }

        return false;
    }

    public static string CardName(int _cardNum)
    {
        string res="";
        if (_cardNum == 0)
            return "Джокер";

        res = (_cardNum % 13).ToString();
        if (res == "1")
        {
            res = "Т";
        }

        if (res == "11")
        {
            res = "В";
        }

        if (res == "12")
        {
            res = "Д";
        }

        if (res == "0")
        {
            res = "К";
        }

        if ((_cardNum >= 40) && (_cardNum <= 52))
        {
            res = res + " пик";
        }

        if ( (_cardNum >= 27) && (_cardNum <= 39))
        {
            res = res + " треф";
        }

        if ((_cardNum >= 14) && (_cardNum <= 26))
        {
            res = res + " бубей";
        }

        if ((_cardNum >= 1) && (_cardNum <= 13))
        {
            res = res + " черв";
        }
        return res;
    }
   
}