namespace ShadcnBlazor.Extras.Steppers;

public enum StepperState
{
    Inactive = 0,
    Active = 1,
    Completed = 2
}

public enum StepperOrientation
{
    Horizontal = 0,
    Vertical = 1
}

public static class Extensions
{
    public static string ToDataString(this StepperState state)
        => state.ToString().ToLowerInvariant();

    public static string ToDataString(this StepperOrientation orientation)
        => orientation.ToString().ToLowerInvariant();
}