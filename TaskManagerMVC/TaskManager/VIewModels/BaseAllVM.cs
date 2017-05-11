namespace TaskManager.VIewModels
{
    using DataAccess.Entities;
    using System.Collections.Generic;

    public class BaseAllVM<T, F>
        where T : BaseEntity
        where F : BaseFilterVM<T>
    {
        public IList<T> Items { get; set; }

        public Pager Pager { get; set; }

        public F Filter { get; set; }
    }
}