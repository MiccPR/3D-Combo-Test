public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    public PlayerStateReusableData ReusableData { get; }

    public PlayerMovementStateMachine MovementStateMachine { get; }

    public PlayerCombatStateMachine CombatStateMachine { get; }

    public PlayerStateMachine(Player player)
    {
        Player = player;
        MovementStateMachine = new PlayerMovementStateMachine(player, this);
        // CombatStateMachine = new PlayerCombatStateMachine();

        ReusableData = new PlayerStateReusableData();
    }
}
