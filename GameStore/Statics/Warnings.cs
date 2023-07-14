namespace GameStore.Statics
{
    public static class Warnings
    {
        public static string AccountAlreadyExists<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity).Name;
            return $"{type} is already exists!";
        }
    }
}
