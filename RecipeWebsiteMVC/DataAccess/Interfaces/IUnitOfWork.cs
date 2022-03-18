namespace RecipeWebsiteMVC.DataAccess.Interfaces
{
    /// <summary>
    /// Wrapper till alla Repos som kommer användas
    /// Viktigt att komma ihåg att Ingecera alla som används i UnitOfWork :)
    /// </summary>
    public interface IUnitOfWork
    {
        ICategoryRepo Category { get; }
        IRecipeRepository Recipe { get; }
        IAppUserRepo User { get; }


        public Task SaveAsync();
        public void Save();
    }
}
