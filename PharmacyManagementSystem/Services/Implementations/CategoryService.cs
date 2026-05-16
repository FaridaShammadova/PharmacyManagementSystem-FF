using AutoMapper;
using PharmacyManagementSystem.Models;
using PharmacyManagementSystem.ViewModels.Category;
using PharmacyManagementSystem.Repositories.Interfaces;

namespace PharmacyManagementSystem.Services.Implementations
{
    public class CategoryService
    {
        private readonly IGenericRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CategoryViewModel>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync();
            return _mapper.Map<List<CategoryViewModel>>(data);
        }

        public async Task<CategoryViewModel?> GetByIdAsync(int id)
        {
            var data = await _repository.GetByIdAsync(id);
            return _mapper.Map<CategoryViewModel>(data);
        }

        public async Task CreateAsync(CategoryCreateViewModel model)
        {
            var entity = _mapper.Map<Category>(model);
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(CategoryViewModel model)
        {
            var entity = await _repository.GetByIdAsync(model.Id);
            if (entity is null) return;

            _mapper.Map(model, entity);

            _repository.Update(entity);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
            {
                _repository.Delete(entity);
                await _repository.SaveAsync();
            }
        }

        public async Task<List<Category>> GetAllForAdminAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
