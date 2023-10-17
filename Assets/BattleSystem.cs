using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    void getJitsuDeck()
    {
        deck = JitsuDeckSingleton.Instance.GetDeck();
        enemyDeck = JitsuDeckSingleton.Instance.GetEnemyDeck();
    }
    void updateCards()
    {
        cardSprites.setImageSprites();
        cardTexts.setCardLevels();
    }
    void UpdateDialogueText(string s) { dialogueText.text = s; }
    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<unitInformation>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<unitInformation>();
        
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        UpdateDialogueText("A battle ensues!");

        getJitsuDeck();

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
    string CreateAttackString(JitsuCard card, bool isPlayer)
    {
        string s = isPlayer ? "Chosen level " : "Enemy counters with a ";
        s += card.attackLevel + " ";
        switch(card.element)
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
        return s;
    }
    IEnumerator PlayerAttack(int cardIndex)
    {
        state = BattleState.ENEMYTURN;
        JitsuCard playerCard = deck.chooseCard(cardIndex);
        string s = CreateAttackString(playerCard, true);
        UpdateDialogueText(s);
        yield return new WaitForSeconds(2f);
        StartCoroutine(EnemyTurn(playerCard));
    }
    IEnumerator EnemyTurn(JitsuCard playerCard)
    {
        JitsuCard enemyCard = AIChooseRandomCard();
        string s = CreateAttackString(enemyCard, false);
        UpdateDialogueText(s);
        yield return new WaitForSeconds(2f);
        checkWinner(playerCard, enemyCard);
        yield return new WaitForSeconds(2f);
        checkGameEnd();
    }
    JitsuCard AIChooseRandomCard()
    {
        int cardIndex = rnd.Next(0, 5);
        JitsuCard enemyCard = enemyDeck.chooseCard(cardIndex);
        return enemyCard;
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
    void checkGameEnd() 
    {
        switch(checkGameWinner()) {
            case 0: // user won
                state = BattleState.WON;
                StartCoroutine(EndBattle());
                break;
            case 1: // enemy won
                state = BattleState.LOST;
                StartCoroutine(EndBattle());
                break;
            default:
                state = BattleState.PLAYERTURN;
                updateCards();
                PlayerTurn();
                break;
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
        if (snowAttacks == 3 || fireAttacks == 3 || waterAttacks == 3
            || (fireAttacks >= 1 && snowAttacks >= 1 && waterAttacks >= 1)) {
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
        switch (determinator)
        {
            case -2:
            case 1:
                playerHUD.addAttack(playerElement);
                dialogueText.text = "Your element overpowers his!";
                outcome = 1;
                break;
            case -1:
            case 2:
                enemyHUD.addAttack(enemyElement);
                dialogueText.text = "Your element was overpowered!";
                outcome = -1;
                break;
            case 0:
                outcome = checkLevelDifference(determinator2, playerCard, enemyCard);
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
    IEnumerator EndBattle(){
        if (state == BattleState.WON){
            UpdateDialogueText("Congratulations you won!");
        } else if (state == BattleState.LOST) {
            UpdateDialogueText("Womp womp, you lost !");
        }
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("MainMenuScene");
    }
    void PlayerTurn() { UpdateDialogueText("Choose an attack!"); }
    public void OnCardButton(int cardIndex)
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack(cardIndex));
    }
    public void Card0() { OnCardButton(0); }
    public void Card1() { OnCardButton(1); }
    public void Card2() { OnCardButton(2); }
    public void Card3() { OnCardButton(3); }
    public void Card4() { OnCardButton(4); }
}
