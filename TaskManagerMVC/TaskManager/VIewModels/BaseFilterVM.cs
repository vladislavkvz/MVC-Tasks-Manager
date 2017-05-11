namespace TaskManager.VIewModels
{
    using DataAccess.Entities;
    using System;

    public abstract class BaseFilterVM<T>
        where T : BaseEntity
    {
        public string Prefix { get; set; }
        public Pager ParentPager { get; set; }
        public abstract Predicate<T> BuildFilter();
    }
}