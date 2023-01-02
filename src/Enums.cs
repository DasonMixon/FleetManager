namespace FleetManager
{
    // TODO: See if predefined modes like this makes sense in this service, or if an upstream service handles translating the desired mode
    // and making the corresponding requests to this service
    public enum FleetDeploymentMode
    {
        OnDemand = 0, // Fleet will start at 0 replicas and only increase when a server is needed
        Dyanmic = 1, // Fleet will start at n warmed-up replicas and increase when necessary
        Static = 2 // Fleet will start at n servers and never increase or decrease
    }
}
