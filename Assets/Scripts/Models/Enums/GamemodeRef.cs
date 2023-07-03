
public enum GamemodeRef
{
    Versus, //Baskets are dropped on death. Owners can pick up their basket
    Bases, //Baskets are positioned on map. Owners can store eggs in basket
    KOTH, //Baskets are in hill. Stand in hill to earn eggs for basket. Cannot earn if multiple teams are in hill.
    Survival, //Baskets are positioned on map. Start with 50 eggs, cannot add eggs to your own base but can steal eggs. When team runs out of eggs they are eliminated.
    Elimination, //No baskets. One life. Best of 5
    Freeze, //Two teams only. No baskets. (Maybe baskets so seekers have a target)
            //One team are "seekers", can shoot and freeze players when shot. They win if they freeze all players
            //Other team are "hiders", cannot shoot and unfreeze players by interacting. They win if they survive the time limit
}
