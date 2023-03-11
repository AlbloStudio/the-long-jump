public static class Enum
{
    public enum CharState
    {
        Grounded,
        Coyoting,
        Airing,
        Jumping,
        Impulsing,
    }

    public enum Facing
    {
        Right,
        Left
    }

    public enum PlanningMode
    {
        Waiting = 0,
        Playing = 1,
        Planning = 2,
    }

    public enum StartState
    {
        Awaking = 0,
        Ready = 1,
        Starting = 2,
        Started = 3,
    }
}