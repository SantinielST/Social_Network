﻿using SocialNetwork.DLL.Interfaces;

namespace SocialNetwork.DLL.UoW;

public interface IUnitOfWork : IDisposable
{
    int SaveChanges(bool ensureAutoHistory = false); //Первый метод — сохранение всех изменений в базу данных (по всем репозиториям).

    IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = true) where TEntity : class; //GetRepository, возвращающий объект
                                                                                                        //IRepository, — это все репозитории для наших самописных моделей.
}
