using System;

[Flags]
public enum ComplexCubeType
{
    None = 0,
    Static = CubeBehaviors.Static,
    Moveable  = CubeBehaviors.Moveable,
    MoveableVictory = CubeBehaviors.Moveable | CubeBehaviors.Victory,
    StaticDelivery = CubeBehaviors.Static | CubeBehaviors.Delivery,
    StaticElevator = CubeBehaviors.Static | CubeBehaviors.Elevator,
    Heavy = CubeBehaviors.Moveable | CubeBehaviors.Heavy,
    HeavyVictory = CubeBehaviors.Moveable | CubeBehaviors.Heavy | CubeBehaviors.Victory,
    Mine = CubeBehaviors.Moveable | CubeBehaviors.Mine,
    MineVictory = CubeBehaviors.Moveable | CubeBehaviors.Victory | CubeBehaviors.Mine,
    Counter = CubeBehaviors.Static | CubeBehaviors.Counter,
    Switcher = CubeBehaviors.Moveable | CubeBehaviors.Switcher,
    SwitcherVictory = CubeBehaviors.Moveable | CubeBehaviors.Switcher | CubeBehaviors.Victory,
    Rotator = CubeBehaviors.Static | CubeBehaviors.Rotator
}