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
        FadeIn = 0,
        Ready = 1,
        FadeOut = 2,
        Done = 3,
    }

    public enum DeathType
    {
        Fall = 0,
        Abism = 1,
        Drown = 2,
        Spikes = 3,
        Reset = 4,
    }

    public enum CollisionType
    {
        Collision = 0,
        Trigger = 1
    }

    public enum PlayerSounds
    {
        Jump = 0,
        Grounded = 1,
        Drown = 3,
        Death = 4,
    }
}