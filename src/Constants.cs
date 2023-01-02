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

        public static class Regex
        {
            public const string ResourceName = "[a-z-]+";
            public const string Cpu = "[0-9]+m";
            public const string Memory = "[0-9]+Mi";
        }

        public static class MinimumResourceCount
        {
            public const int FleetReplicas = 0;
        }

        public static class MaximumResourceCount
        {
            public const int FleetReplicas = 999;
        }
    }
}
