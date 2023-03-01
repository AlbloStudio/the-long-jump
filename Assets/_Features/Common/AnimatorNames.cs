using UnityEngine;

public static class CharAnimationNames
{
    public static readonly int Grounded = Animator.StringToHash("grounded");
    public static readonly int Jump = Animator.StringToHash("jump");
    public static readonly int VelocityX = Animator.StringToHash("velocityX");
    public static readonly int VelocityY = Animator.StringToHash("velocityY");
}

public static class SpringAnimationNames
{
    public static readonly int Activate = Animator.StringToHash("activate");
}
