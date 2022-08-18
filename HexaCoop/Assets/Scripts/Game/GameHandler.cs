using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameHandler : HexaEventCallback
{
    private HexGrid HexGrid;
    public static GameHandler instance;
    public GameStatus GameStatus;
    public int CurrentTurn;

    private new void Awake()
    {
        base.Awake();
        instance = this;
        GameStatus = GameStatus.NotStarted;
    }

    private new void Start()
    {
        base.Start();
        HexGrid = GameObject.FindObjectOfType<HexGrid>();        
    }     
}