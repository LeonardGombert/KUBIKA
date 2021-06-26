namespace Gameplay.Scripts.Cubes.Managers
{
    public class BehaviorManager_Gravity : AbstractBehaviorManager<CubeBehavior_Movement>
    {
        // target = (moving cube's index - down)
        // OPTION 1 : check a dictionary sorted by index for target
        // OPTION 2 : get and check the grid for contents of target

        public void CheckCubeGravity()
        {
            
        }
    }
}