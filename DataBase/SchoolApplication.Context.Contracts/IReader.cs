﻿namespace SchoolApplication.Context.Contracts
{
    /// <summary>
    /// Интерфейс получения записей из контекста
    /// </summary>
    public interface IReader
    {
        /// <summary>
        /// Предоставляет функциональные возможности для выполнения запросов
        /// </summary>
        IQueryable<TEntity> Read<TEntity>() where TEntity : class;
    }
}
