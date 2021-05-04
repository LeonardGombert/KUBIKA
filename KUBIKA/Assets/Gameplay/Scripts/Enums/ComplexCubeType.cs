public enum ComplexCubeType
{
    None = 0,
    Static = CubeBehaviors.Static,
    Moveable  = CubeBehaviors.Moveable,
    MoveableVictory = CubeBehaviors.Moveable | CubeBehaviors.Victory,
    StaticDelivery = CubeBehaviors.Static | CubeBehaviors.Delivery,
    StaticElevator = CubeBehaviors.Static | CubeBehaviors.Elevator,
    Heavy = CubeBehaviors.Moveable | CubeBehaviors.Heavy,
    VictoryHeavy = CubeBehaviors.Moveable | CubeBehaviors.Heavy | CubeBehaviors.Victory,
    Mine = CubeBehaviors.Moveable | CubeBehaviors.Mine,
    VictoryMine = CubeBehaviors.Moveable | CubeBehaviors.Mine | CubeBehaviors.Victory,
    Counter = CubeBehaviors.Static | CubeBehaviors.Counter,
    Switcher = CubeBehaviors.Moveable | CubeBehaviors.Switcher,
    VictorySwitcher = CubeBehaviors.Moveable | CubeBehaviors.Switcher | CubeBehaviors.Victory,
    Rotator = CubeBehaviors.Static | CubeBehaviors.Rotator
}