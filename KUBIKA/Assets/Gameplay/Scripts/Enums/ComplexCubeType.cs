public enum ComplexCubeType
{
    None = 0,
    Static = CubeType.Static,
    Moveable  = CubeType.Moveable,
    MoveableVictory = CubeType.Moveable | CubeType.Victory,
    StaticDelivery = CubeType.Static | CubeType.Delivery,
    StaticElevator = CubeType.Static | CubeType.Elevator,
    Heavy = CubeType.Moveable | CubeType.Heavy,
    VictoryHeavy = CubeType.Moveable | CubeType.Heavy | CubeType.Victory,
    Mine = CubeType.Moveable | CubeType.Mine,
    VictoryMine = CubeType.Moveable | CubeType.Mine | CubeType.Victory,
    Counter = CubeType.Static | CubeType.Counter,
    Switcher = CubeType.Moveable | CubeType.Switcher,
    VictorySwitcher = CubeType.Moveable | CubeType.Switcher | CubeType.Victory,
    Rotator = CubeType.Static | CubeType.Rotator
}