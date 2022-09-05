using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvents : HexaEventCallback
{
    [ComponentInject] private ButtonUpdater buttonUpdater;
    [ComponentInject] private EndTurnButtonScript endTurnButtonScript;
    
    private void Start()
    {
        UpdateAllAbilities(setToUnselected: true, interactable: false);
        UpdateEndTurnButton(visible: false, interactable: false, waitForSeconds: 0);
    }

    private void UpdateEndTurnButton(bool? visible = null, bool ? interactable = null, bool onlyInteractableOnMyNetwork = true, float waitForSeconds = 0.1f)
    {
        StartCoroutine(CR_UpdateEndTurnButton(visible, interactable, onlyInteractableOnMyNetwork, waitForSeconds));
    }

    private IEnumerator CR_UpdateEndTurnButton(bool? visible, bool? interactable, bool onlyInteractableOnMyNetwork, float waitForSeconds)
    {
        yield return Wait4Seconds.Get(waitForSeconds);
        if (visible.HasValue)
        {
            endTurnButtonScript.GetComponent<CanvasGroup>().alpha = visible.Value ? 1 : 0;
        }
        if (onlyInteractableOnMyNetwork && !Netw.CurrPlayer().IsOnMyNetwork())
        {
            endTurnButtonScript.Button.interactable = false;
        }
        else if (interactable.HasValue)
        {
            endTurnButtonScript.Button.interactable = interactable.Value;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            UpdateAllAbilities(setToUnselected: true);
        }
    }

    protected override void OnEnemyFaseStarted()
    {
        UpdateAllAbilities(setToUnselected: true, interactable: false);
        UpdateEndTurnButton(interactable: false);
    }

    protected override void OnEndRound(bool reachedMiddle, PlayerScript pWinner)
    {
        UpdateAllAbilities(setToUnselected: true, interactable: false);
        UpdateEndTurnButton(interactable: false);
    }

    protected override void OnEndGame()
    {
        UpdateAllAbilities(setToUnselected: true, interactable: false);
        UpdateEndTurnButton(interactable: false);
    }

    protected override void OnEndPlayerTurn(PlayerScript player, List<AbilityQueueItem> abilityQueue)
    {
        // hier klopt deze check wel --> ook bij sim. turns
        if (player == Netw.CurrPlayer())
        {
            UpdateAllAbilities(setToUnselected: true, interactable: false);
            UpdateEndTurnButton(interactable: false);
        }
    }

    protected override void OnPlayerAbility(PlayerScript player, Hex hex, AbilityType type, int queueId)
    {
        if (GameHandler.instance.GameStatus != GameStatus.PlayerFase) { return; }
        if (Settings.UseQueueAbilities) { return; }
                  
        if (!type.GetEventImmediatelyFinished())
        {
            // niet interactable totdat het event voorbij is
            UpdateAllAbilities(setToUnselected: true, interactable: false);
            UpdateEndTurnButton(interactable: false);
        }
        else
        {
            StartCoroutine(UpdatePlayerAbilityButtons());
        }        
    }

    protected override void OnPlayerAbilityQueue(PlayerScript player, Hex hex, AbilityType type)
    {
        if (GameHandler.instance.GameStatus != GameStatus.PlayerFase) { return; }
        if (!Settings.UseQueueAbilities) { return; }

        StartCoroutine(UpdatePlayerAbilityButtons());
    }

    protected override void OnRemoveQueueItem(AbilityQueueItem queueItem)
    {
        if (GameHandler.instance.GameStatus != GameStatus.PlayerFase) { return; }
        if (!Settings.UseQueueAbilities) { return; }

        StartCoroutine(UpdatePlayerAbilityButtons());
    }

    protected override void OnUnitAttackHit(IUnit unit, Hex hexWithTargetHit, int damage)
    {        
        if (GameHandler.instance.GameStatus != GameStatus.PlayerFase) { return; }
        StartCoroutine(UpdatePlayerAbilityButtons());
        UpdateEndTurnButton(interactable: true);
    }

    protected override void OnPlayerDamageObjectHitTile(PlayerScript player, Hex hexHit, DamageObjectType doType)
    {
        if (GameHandler.instance.GameStatus != GameStatus.PlayerFase) { return; }
        StartCoroutine(UpdatePlayerAbilityButtons());
        UpdateEndTurnButton(interactable: true);
    }

    protected override void OnUnitMovingFinished(IUnit unit)
    {
        if (GameHandler.instance.GameStatus != GameStatus.PlayerFase) { return; }
        StartCoroutine(UpdatePlayerAbilityButtons());
        UpdateEndTurnButton(interactable: true);
    }

    private IEnumerator UpdatePlayerAbilityButtons()
    {
        yield return Wait4Seconds.Get(0.1f); // wijziging moet verwerkt worden....
        if (Netw.CurrPlayer().IsOnMyNetwork())
        {
            UpdateAllAbilities(setToUnselected: true, checkAvailableInTurn: true);
        }       
    }

    private void UpdateAllAbilities(bool setToUnselected, bool checkAvailableInTurn = true, bool checkEnoughPoints = true, bool? interactable = null)
    {
        foreach (AbilityType abilityType in Utils.GetValues<AbilityType>().Where(x => x.IsAvailableInGame()))
        {
            if (setToUnselected)
            {
                buttonUpdater.SetToUnselected(abilityType);
            }
            if (interactable.HasValue)
            {
                buttonUpdater.SetAbilityInteractable(abilityType, interactable.Value);
                if (!interactable.Value)
                {
                    continue;
                }
            }
            if (Netw.CurrPlayer() == null)
            {
                buttonUpdater.SetAbilityInteractable(abilityType, false);
        }
            if (checkAvailableInTurn || checkEnoughPoints)
            {
                var availableInTurnResult = checkAvailableInTurn ? abilityType.IsAvailableThisTurn() : true;
                var enoughPointsResult = checkEnoughPoints ? abilityType.HaveEnoughPoints() : true;

                // todo: dat moet anders -> doel Ability zelf weet of hij extra redenen heeft om wel/niet getoond te worden. Bv max aantal keren een raket per keer
                var canDoAbilityThisTurn = buttonUpdater.abilityScripts.Single(x => x.Type == abilityType).GetComponent<IAbilityAction>().CanDoAbility(Netw.CurrPlayer());

                if(Settings.UseQueueAbilities && abilityType.IsPickup())
                {
                    // heeft pickup abil al gezet in queue
                    if(Netw.CurrPlayer() != null && Netw.CurrPlayer().GetComponent<PlayerAbilityQueueSelection>().AbilityQueueItems.Any(x => x.AbilityType == abilityType))
                    {
                        canDoAbilityThisTurn = false;
                    }

                }

                buttonUpdater.SetAbilityInteractable(abilityType, availableInTurnResult && enoughPointsResult && canDoAbilityThisTurn);
            }
        }
    }

    protected override void OnNewRoundStarted(List<PlayerScript> arg1, PlayerScript currentPlayer) => StartCoroutine(CheckEnableButtonsNewTurn(currentPlayer));
    protected override void OnNewPlayerTurn(PlayerScript currentPlayer) => StartCoroutine(CheckEnableButtonsNewTurn(currentPlayer));

    private IEnumerator CheckEnableButtonsNewTurn(PlayerScript currentPlayer)
    {
        yield return Wait4Seconds.Get(0.1f);// wacht tot wijziging is verwerkt
        if(Settings.UseSimultaniousTurns && !currentPlayer.IsOnMyNetwork()) { yield break; } // AI beurt verandert niet bij andere spelers

        if (GameHandler.instance.GameStatus == GameStatus.PlayerFase) 
        {
            UpdateEndTurnButton(visible: true, interactable: true, waitForSeconds: 0f);
            if(Settings.UseSimultaniousTurns)
            {
                UpdateAllAbilities(interactable: true, setToUnselected: true);
            }
            else
            {
                UpdateAllAbilities(interactable: currentPlayer.IsOnMyNetwork(), setToUnselected: true);
            }
        }
    }
}