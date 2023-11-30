using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    [SerializeField] Animator transition;
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

    [SerializeField] SoundManager soundSystem;

    [SerializeField] CardsSelected cardSelectedSystem;

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
    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<unitInformation>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<unitInformation>();
        
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
        soundSystem.PlayEntranceMusic();
        UpdateDialogueText("A battle ensues!");
        getJitsuDeck();
        yield return new WaitForSeconds(6f);

        PlayerTurn();
        yield return new WaitForSeconds(1f);
        state = BattleState.PLAYERTURN;
        updateCards();
        soundSystem.PlayCardRevealSound();
    }
    void PlayerTurn() { UpdateDialogueText("Choose an attack!"); }
    public void Card0() { OnCardButton(0); }
    public void Card1() { OnCardButton(1); }
    public void Card2() { OnCardButton(2); }
    public void Card3() { OnCardButton(3); }
    public void Card4() { OnCardButton(4); }
    public void OnCardButton(int cardIndex)
    {
        if (state != BattleState.PLAYERTURN)
            return;
        soundSystem.PlayCardPickedSound();
        StartCoroutine(PlayerAttack(cardIndex));
    }
    IEnumerator PlayerAttack(int cardIndex)
    {
        state = BattleState.ENEMYTURN;
        JitsuCard playerCard = deck.chooseCard(cardIndex);
        string s = CreateAttackString(playerCard, true);
        UpdateDialogueText(s);
        cardSelectedSystem.SpawnPlayerCard(playerCard);
        yield return new WaitForSeconds(2f);
        StartCoroutine(EnemyTurn(playerCard));
    }
    IEnumerator EnemyTurn(JitsuCard playerCard)
    {
        JitsuCard enemyCard = AIChooseRandomCard();
        string s = CreateAttackString(enemyCard, false);
        UpdateDialogueText(s);
        cardSelectedSystem.SpawnEnemyCard(enemyCard);
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
                soundSystem.PlayElementSound(playerElement);
                dialogueText.text = "Your element overpowers his!";
                outcome = 1;
                break;
            case -1:
            case 2:
                enemyHUD.addAttack(enemyElement);
                soundSystem.PlayElementSound(enemyElement);
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
    int checkLevelDifference(int difference, JitsuCard playerCard, JitsuCard enemyCard){
        int playerElement = playerCard.element;
        int enemyElement = enemyCard.element;
        if (difference < 0){
            enemyHUD.addAttack(enemyElement);
            soundSystem.PlayElementSound(enemyElement);
            dialogueText.text = "Your element was overpowered!";
            return -1; // you lost
        } else if (difference > 0) {
            playerHUD.addAttack(playerElement);
            soundSystem.PlayElementSound(playerElement);
            dialogueText.text = "Your element overpowers his!";
            return 1; // you won
        } else {
            soundSystem.PlayElementsCancelSound();
            dialogueText.text = "Your attacks cancel eachother out!";
            return 0; // you tied
        }
    }
    void checkGameEnd() 
    {
        if (checkGameWinner(0)){ // check if user won
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        } else if (checkGameWinner(1)){ // check if enemy won
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        } else {
            state = BattleState.PLAYERTURN;
            updateCards();
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
        if (snowAttacks == 3 || fireAttacks == 3 || waterAttacks == 3
            || (fireAttacks >= 1 && snowAttacks >= 1 && waterAttacks >= 1)) {
            return true;
        }
        return false;
    }
    IEnumerator EndBattle(){
        if (state == BattleState.WON){
            soundSystem.PlayGameWinnerSound();
            UpdateDialogueText("Congratulations you won!");
        } else if (state == BattleState.LOST) {
            soundSystem.PlayGameLoserSound();
            UpdateDialogueText("Womp womp, you lost !");
        }
        yield return new WaitForSeconds(2f);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f); // 1f for transition time
        SceneManager.LoadScene("MainMenuScene");
    }
}
