// The Game Events used across the Game.
// Anytime there is a need for a new event, it should be added here.

using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameEventsHandler
{
    public static readonly GameStartEvent GameStartEvent = new GameStartEvent();
    public static readonly GameOverEvent GameOverEvent = new GameOverEvent();
    public static readonly MoneyCollectEvent MoneyCollectEvent = new MoneyCollectEvent();
    public static readonly PlayerFinishReachedEvent PlayerFinishReachedEvent = new PlayerFinishReachedEvent();
    public static readonly PlayerScoreChangeEvent PlayerScoreChangeEvent = new PlayerScoreChangeEvent();
    public static readonly PlayerMoneyUpdateEvent PlayerMoneyUpdateEvent = new PlayerMoneyUpdateEvent();
    public static readonly AmbianceInfoEvent AmbianceInfoEvent = new AmbianceInfoEvent();
    public static readonly DebugCallEvent DebugCallEvent = new DebugCallEvent();
    public static readonly PlayerObstacleHitEvent PlayerObstacleHitEvent = new PlayerObstacleHitEvent();
    public static readonly UpgradeButtonPressEvent UpgradeButtonPressEvent = new UpgradeButtonPressEvent();
    public static readonly PlayerFireHitEvent PlayerFireHitEvent = new PlayerFireHitEvent ();
    public static readonly FinisherEndEvent FinisherEndEvent = new FinisherEndEvent();
    public static readonly GateEvent GateEvent = new GateEvent();
    public static readonly FinisherPlayerInPosition FinisherPlayerInPosition = new FinisherPlayerInPosition();
    public static readonly PlayerProgressEvent PlayerProgressEvent = new PlayerProgressEvent();
    public static readonly PlayerDNACollectEvent PlayerDNACollectEvent = new PlayerDNACollectEvent();
    public static readonly ShowcaseSwitchEvent ShowcaseSwitchEvent = new ShowcaseSwitchEvent();
    public static readonly ShowcaseSelectEvent ShowcaseSelectEvent = new ShowcaseSelectEvent();
    public static readonly ShowcaseSkinInfoEvent ShowcaseSkinInfoEvent = new ShowcaseSkinInfoEvent();
    public static readonly ShowcaseSkinShowEvent ShowcaseSkinShowEvent = new ShowcaseSkinShowEvent();

    public static readonly FinisherPlayerProgressInfoEvent FinisherPlayerProgressInfoEvent =
        new FinisherPlayerProgressInfoEvent();
    
    public static readonly CoinUiCollectEvent CoinUiCollectEvent = new CoinUiCollectEvent();
}

public class GameEvent {}
public class GameStartEvent : GameEvent
{
    public float PlayerHeight;
}

public class GameOverEvent : GameEvent
{
    public bool IsWin;
}

public class MoneyCollectEvent : GameEvent
{
    public Vector3 PlayerPosition;
}

public class PlayerFinishReachedEvent : GameEvent
{
    public Transform PlayerTransform;
}

public class PlayerFireHitEvent : GameEvent{}

public class FinisherEndEvent : GameEvent
{
}

public class PlayerScoreChangeEvent : GameEvent
{
    public float Value;
    public bool IsImmediate;
    public int Stage;
}

public class PlayerMoneyUpdateEvent : GameEvent
{
    public int TotalMoney;
    public int CurrentMoney;
}

public class AmbianceInfoEvent : GameEvent
{
    public Action<int> Callback;
}

public class PlayerObstacleHitEvent : GameEvent
{
    public Vector3 Contact;
}

public class UpgradeButtonPressEvent: GameEvent
{
    public int UpgradeLevel;
    public int MoneyTotal;
}
public class GateEvent : GameEvent
{
    public int ChangeValue;
}
public class FinisherPlayerInPosition : GameEvent
{
}
public class PlayerProgressEvent : GameEvent
{
    public float Height;
}
public class PlayerDNACollectEvent : GameEvent
{
    public Vector3 PlayerPosition;
}
public class DebugCallEvent : GameEvent
{
    public Dictionary<int, int> IntValues;
    public Dictionary<int, float> FloatValues;
}

public class ShowcaseSwitchEvent : GameEvent
{
    public bool IsNext;
    public Action<string> Callback;
}
public class ShowcaseSelectEvent : GameEvent
{
    
}

public class ShowcaseSkinInfoEvent : GameEvent
{
    public bool AllowSelect;
}

public class FinisherPlayerProgressInfoEvent : GameEvent
{
    public int SkinNum;
}

public class ShowcaseSkinShowEvent : GameEvent
{
    public string Name;
    public int Number;
}

public class CoinUiCollectEvent : GameEvent
{
}





