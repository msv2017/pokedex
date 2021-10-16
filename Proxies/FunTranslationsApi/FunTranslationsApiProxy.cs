namespace pokedex.Proxies
{
    // Our translation API is public.
    // To maintain our service level we ratelimit the number of API calls.
    // For public API calls this is 60 API calls a day with distribution of 5 calls an hour.
    // For paid plans this limit is increased according to the service level described in the plan.
    public interface IFunTranslationsApiProxy
    {

    }

    public class FunTranslationsApiProxy: IFunTranslationsApiProxy
    {
        public FunTranslationsApiProxy()
        {
        }
    }
}
