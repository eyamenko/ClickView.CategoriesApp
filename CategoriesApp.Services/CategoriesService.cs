﻿namespace CategoriesApp.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts.Data;
    using Contracts.Services;
    using Models;

    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<IEnumerable<Category>> Get()
        {
            return await _categoriesRepository.Get();
        }

        public async Task<Category> Get(int id)
        {
            return await _categoriesRepository.Get(id);
        }

        public async Task<bool> Create(Category category)
        {
            return Category.IsValid(category) && await _categoriesRepository.Create(category);
        }

        public async Task<bool> Update(Category category)
        {
            return Category.IsValid(category) && await _categoriesRepository.Update(category);
        }

        public async Task<bool> Delete(int id)
        {
            foreach (var child in await this.GetChildren(id))
                await this.Delete(child.Id);
            return await _categoriesRepository.Delete(id);
        }

        public async Task<IEnumerable<Category>> GetRoot()
        {
            return await _categoriesRepository.GetRoot();
        }

        public async Task<IEnumerable<Category>> GetChildren(int id)
        {
            return await _categoriesRepository.GetChildren(id);
        }
    }
}