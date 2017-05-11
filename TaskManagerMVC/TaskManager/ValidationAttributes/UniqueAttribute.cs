namespace TaskManager.ValidationAttributes
{
    using DataAccess.Entities;
    using DataAccess.Repositories;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    public class UniqueAttribute : ValidationAttribute
    {
        private string entityTypeName;
        private string memberName;

        public UniqueAttribute(string entityTypeName)
        {
            this.entityTypeName = entityTypeName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            this.memberName = validationContext.MemberName;

            return base.IsValid(value, validationContext);
        }

        public override bool IsValid(object value)
        {
            Type entityType =
                Assembly.GetAssembly(typeof(BaseEntity)).GetType(entityTypeName);
            var repo = CreateRepo(entityType);

            MethodInfo mi = repo.GetType().GetMethod("GetAll");
            PropertyInfo pi = entityType.GetProperties().FirstOrDefault(p => p.Name == memberName);
            
            IEnumerable<object> items = (IEnumerable<object>)mi.Invoke(repo, new object[] { null, 0, 0 });

            if (value == null)
                value = "";

            foreach (var item in items)
            {
                if (pi.GetValue(item).ToString() == value.ToString())
                {
                    this.ErrorMessage = "This " + pi.Name + " already exists";
                    return false;
                }
            }
            return true;
        }

        private object CreateRepo(Type entityType)
        {
            if (entityType.Name == typeof(User).Name)
            {
                return RepositoryFactory.GetUsersRepository();
            }
            else if (entityType.Name == typeof(Task).Name)
            {
                return RepositoryFactory.GetTasksRepository();
            }
            else if (entityType.Name == typeof(Comment).Name)
            {
                return RepositoryFactory.GetCommentsRepository();
            }
            else if (entityType.Name == typeof(LoggedWork).Name)
            {
                return RepositoryFactory.GetLoggedWorksRepository();
            }
            return null;
        }
    }
}