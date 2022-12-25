namespace FleetManager
{
    public static class Constants
    {
        public static class Fleet
        {
            public const string Group = "agones.dev";
            public const string Version = "v1";
            public const string NamePlural = "fleets";
            public const string Kind = "Fleet";
        }

        public static class GameServerAllocation
        {
            public const string Group = "allocation.agones.dev";
            public const string Version = "v1";
            public const string NamePlural = "gameserverallocations";
            public const string Kind = "GameServerAllocation";
        }
    }
}
