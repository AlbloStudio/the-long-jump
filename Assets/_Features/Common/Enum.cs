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
        Ready = 0,
        Starting = 1,
        Started = 2,
    }
}