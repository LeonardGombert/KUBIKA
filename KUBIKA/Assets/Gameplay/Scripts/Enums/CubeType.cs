[System.Flags] public enum CubeType
{
    None = 0,
    Static = 1 << 0,
    Moveable = 1 << 1,
    Victory = 1 << 2,
    Delivery = 1 << 3,
    Elevator = 1 << 4,
    Heavy = 1 << 5,
    Mine = 1 << 6,
    Counter = 1 << 7,
    Switcher = 1 << 8,
    Rotator  = 1 << 9,
}