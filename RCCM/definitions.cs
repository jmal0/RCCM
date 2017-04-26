namespace RCCM
{
    /// <summary>
    /// Enum representing the two sets of fine axes
    /// </summary>
    public enum RCCMStage
    {
        RCCM1,
        RCCM2,
        Coarse,
        None
    }

    /// <summary>
    /// Enum representing different methods for calculating crack length
    /// </summary>
    public enum MeasurementMode
    {
        Projection,
        Tip,
        Total
    }

    /// <summary>
    /// Enum representing different global (FASTER facility) vs local (pabel) coordinate system
    /// </summary>
    public enum CoordinateSystem
    {
        Global,
        Local
    }
}