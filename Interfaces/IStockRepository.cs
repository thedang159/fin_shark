using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);

        Task<Stock?> GetByIdAsync(string id);

        Task<Stock> CreateAsync(Stock stockModel);

        Task<Stock?> UpdateAsync(string id, UpdateStockRequestDto updateDto);

        Task<Stock?> RemoveAsync(string id);

        Task<bool> StockExists(string id);
    }
}
