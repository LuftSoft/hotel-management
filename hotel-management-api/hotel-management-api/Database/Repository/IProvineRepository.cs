﻿using hotel_management_api.Database.Model;

namespace hotel_management_api.Database.Repository
{
    public interface IProvineRepository
    {
        Task<IEnumerable<Provine>> GetListProvine();
        Task<Provine?> FindByIdAsync(string? id);
        Task<IEnumerable<Provine>?> FindByNameAsync(string? key);

    }
}
