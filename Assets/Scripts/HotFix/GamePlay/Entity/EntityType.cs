namespace HotFix.GamePlay
{
    public enum EntityType
    {
        Tank = 0,
        Hero = 1,
        Monster = 2,
    }
    
    public enum EntityTeamType
    {
        Self = 1,  // 我方
        Enemy = 2, // 敌方
    }

    public enum TargetMode
    {
        Frontmost,  // 最前方
        Overpass    // 越过
    }
}