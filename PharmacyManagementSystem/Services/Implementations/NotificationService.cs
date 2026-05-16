using PharmacyManagementSystem.Models;
using PharmacyManagementSystem.Repositories.Interfaces;

namespace PharmacyManagementSystem.Services.Implementations
{
    public class NotificationService
    {
        private readonly IGenericRepository<Notification> _repository;

        public NotificationService(IGenericRepository<Notification> repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(Notification notification)
        {
            await _repository.AddAsync(notification);
            await _repository.SaveAsync();
        }
    }
}
