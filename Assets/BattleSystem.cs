using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] Transform playerBattleStation;
    [SerializeField] Transform enemyBattleStation;

    unitInformation playerUnit;
    unitInformation enemyUnit;

    [SerializeField] Text dialogueText;

    [SerializeField] BattleState state;
    [SerializeField] BattleHUD playerHUD;
    [SerializeField] BattleHUD enemyHUD;

    JitsuDeck deck;
    JitsuDeck enemyDeck;

    [SerializeField] CardSprite cardSprites;
    [SerializeField] CardText cardTexts;

    System.Random rnd = new System.Random();
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }
    void getJitsuDeck(){
        deck = JitsuDeckSingleton.Instance.GetDeck();
        //deck.printHAQ();
        enemyDeck = JitsuDeckSingleton.Instance.GetEnemyDeck();
    }
    void updateCards(){
        cardSprites.setImageSprites();
        cardTexts.setCardLevels();
    }
    IEnumerator SetupBattle(){
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<unitInformation>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<unitInformation>();
        
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        dialogueText.text = "A battle ensues!";

        getJitsuDeck();

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
    IEnumerator PlayerAttack(int cardIndex){
        state = BattleState.ENEMYTURN;
        JitsuCard playerCard = deck.chooseCard(cardIndex);
        string s = "Chosen level " + playerCard.attackLevel + " ";
        switch(playerCard.element)
        {
            case 0:
                s += "snow";
                break;
            case 1:
                s += "fire";
                break;
            case 2:
                s += "water";
                break;
        }
        s+= " attack!";
        dialogueText.text = s;
        yield return new WaitForSeconds(2f);
        StartCoroutine(EnemyTurn(playerCard));
    }
    IEnumerator EnemyTurn(JitsuCard playerCard){
        string s = "Enemy counters with a ";
        int cardIndex = rnd.Next(0, 5);
        JitsuCard enemyCard = enemyDeck.chooseCard(cardIndex);
        s+= "level " + enemyCard.attackLevel + " ";
        switch(enemyCard.element)
        {
            case 0:
                s += "snow";
                break;
            case 1:
                s += "fire";
                break;
            case 2:
                s += "water";
                break;
        }
        s += " attack!";
        dialogueText.text = s;
        yield return new WaitForSeconds(2f);
        checkWinner(playerCard, enemyCard);
        yield return new WaitForSeconds(2f);
        checkGameEnd();
    }
    int checkLevelDifference(int difference, JitsuCard playerCard, JitsuCard enemyCard){
        int playerElement = playerCard.element;
        int enemyElement = enemyCard.element;
        if (difference < 0){
            enemyHUD.addAttack(enemyElement);
            dialogueText.text = "Your element was overpowered!";
            return -1; // you lost
        } else if (difference > 0) {
            playerHUD.addAttack(playerElement);
            dialogueText.text = "Your element overpowers his!";
            return 1; // you won
        } else {
            dialogueText.text = "Your attacks cancel eachother out!";
            return 0; // you tied
        }
    }
    
    void checkGameEnd(){
        if (checkGameWinner(0)){ // check if user won
            state = BattleState.WON;
            EndBattle();
        } else if (checkGameWinner(1)){ // check if enemy won
            state = BattleState.LOST;
            EndBattle();
        } else {
            state = BattleState.PLAYERTURN;
            updateCards();
            //deck.printHAQ();
            PlayerTurn();
        }
    }
    bool checkGameWinner(int specifier){
        unitInformation temp;
        switch (specifier){
            case 0:
                temp = playerUnit;
                break;
            case 1:
                temp = enemyUnit;
                break;
            default:
                temp = null;
                break;
        }
        int snowAttacks = temp.getSnowAttacks();
        int fireAttacks = temp.getFireAttacks();
        int waterAttacks = temp.getWaterAttacks();
        if (snowAttacks == 3){
            return true;
        } else if (fireAttacks == 3){
            return true;
        } else if (waterAttacks == 3){
            return true;
        } else if (fireAttacks >= 1 && snowAttacks >= 1 && waterAttacks >= 1){
            return true;
        }
        return false;
    }
    void checkWinner(JitsuCard playerCard, JitsuCard enemyCard){
        int playerElement = playerCard.element;
        int enemyElement = enemyCard.element;
        int determinator = playerElement - enemyElement;
        int determinator2 = playerCard.attackLevel - enemyCard.attackLevel;
        int outcome = 0;
        switch (determinator){
            case -2:
                playerHUD.addAttack(playerElement);
                dialogueText.text = "Your element overpowers his!";
                outcome = 1;
                break;
            case -1:
                enemyHUD.addAttack(enemyElement);
                dialogueText.text = "Your element was overpowered!";
                outcome = -1;
                break;
            case 0:
                outcome = checkLevelDifference(determinator2, playerCard, enemyCard);
                break;
            case 1:
                playerHUD.addAttack(playerElement);
                dialogueText.text = "Your element overpowers his!";
                outcome = 1;
                break;
            case 2:
                enemyHUD.addAttack(enemyElement);
                dialogueText.text = "Your element was overpowered!";
                outcome = -1;
                break;
        }
        switch (outcome){
            case -1:
                enemyUnit.increaseElementAttack(enemyElement);
                break;
            case 1:
                playerUnit.increaseElementAttack(playerElement);
                break;
        }
    }
    void EndBattle(){
        if (state == BattleState.WON){
            dialogueText.text = "Congratulations you won!";
        } else if (state == BattleState.LOST) {
            dialogueText.text = "Womp womp, you lost !";
        }
    }
    void PlayerTurn(){
        dialogueText.text = "Choose an attack!";
    }

    public void OnCard0Button(){
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack(0));
    }
    public void OnCard1Button(){
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack(1));
    }
    public void OnCard2Button(){
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack(2));
    }
    public void OnCard3Button(){
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack(3));
    }
    public void OnCard4Button(){
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack(4));
    }
}
