using System;

[Flags]
public enum CubeBehaviorsEnum
{
    Static = 1 << 0,
    Moveable = 2 << 1,
    Victory = 4 << 2,
    Delivery = 8 << 3
}
