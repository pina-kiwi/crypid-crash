namespace ItsAdventureTime;

public static class Conditions
{
    public static Dictionary<ConditionTypes, Condition> conditions = new Dictionary<ConditionTypes, Condition>();
    
    public static void Initialize()
    {
        Condition isDrunk = new Condition(ConditionTypes.IsDrunk);
        isDrunk.AddToActivateList(ConditionActions.WriteOutput("**HIC**"));
        Add(isDrunk);
        
        Condition beerMe = new Condition(ConditionTypes.IsBeerMed);
        beerMe.AddToActivateList(ConditionActions.AddItemToInventory("beer"));
        beerMe.AddToDeactivateList(ConditionActions.RemoveItemFromInventory("beer"));
        Add(beerMe);

        Condition isHungOver = new Condition(ConditionTypes.IsHungOver);
        isHungOver.AddToActivateList(ConditionActions.AddItemToLocation("puke", "Entrance"));
        Add(isHungOver);
        
        Condition isTidiedUp = new Condition(ConditionTypes.IsTidiedUp);
        isTidiedUp.AddToActivateList(ConditionActions.RemoveItemFromLocation("puke", "Entrance"));
        Add(isTidiedUp);
        
        Condition isTeleported = new Condition(ConditionTypes.IsTeleported);
        isTeleported.AddToActivateList(ConditionActions.MovePlayerToLocation("Throne"));
        Add(isTeleported);

        Condition isConnectedRooms = new Condition(ConditionTypes.IsCreatedConnection);
        isConnectedRooms.AddToActivateList(ConditionActions.WriteOutput("a pile of rocks as been lifted from somewhere"));
        isConnectedRooms.AddToActivateList(ConditionActions.AddMapConnection("Entrance", "south", "Cave"));
        isConnectedRooms.AddToActivateList(ConditionActions.AddMapConnection("Cave", "north", "Entrance"));
        Add(isConnectedRooms);
            
        Condition isDisconnectedRooms = new Condition(ConditionTypes.IsDisconnected);
        isDisconnectedRooms.AddToActivateList(ConditionActions.WriteOutput("A convenient pile of rocks blocks the north exit"));
        isDisconnectedRooms.AddToActivateList(ConditionActions.RemoveMapConnection("Entrance", "north"));
        Add(isDisconnectedRooms);
        
        Condition isPlaying = new Condition(ConditionTypes.IsPlaying);
        Add(isPlaying);
        
        Condition isScrollUsed = new Condition(ConditionTypes.IsScrollUsed);
        Add(isScrollUsed);
        
        Condition isSafeUnlocked = new Condition(ConditionTypes.IsSafeUnlocked);
        Add(isSafeUnlocked);
        
    }

    public static void ChangeCondition(ConditionTypes conditionType, bool isSettingToTrue)
    {
        if (NotInDictionary(conditionType))
        {
            return;
        }

        Condition condition = conditions[conditionType];

        if (isSettingToTrue && IsFalse(conditionType))
        {
            condition.Activate();
        }
        else if (!isSettingToTrue && IsTrue(conditionType))
        {
            condition.Deactivate();
        }
    }


    public static void Add(Condition condition)
    {
        conditions.Add(condition.Type, condition);
    }

    public static bool IsTrue(ConditionTypes conditionType)
    {
        if (NotInDictionary(conditionType))
        {
            return false;
        }

        return conditions[conditionType].IsActive;
    }

    public static bool IsFalse(ConditionTypes conditionType)
    {
        if (NotInDictionary(conditionType))
        {
            return true;
        }

        return !conditions[conditionType].IsActive;
    }

    private static bool NotInDictionary(ConditionTypes conditionType)
    {
        return !conditions.ContainsKey(conditionType);
    }

    public static bool CheckCondition(ConditionTypes conditionType)
    {
        if (conditions.ContainsKey(conditionType))
        {
            return conditions[conditionType].IsActive;
        }
        else
        {
            return false;
        }
    }
}