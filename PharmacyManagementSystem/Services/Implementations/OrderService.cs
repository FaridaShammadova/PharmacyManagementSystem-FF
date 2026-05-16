using AutoMapper;
using PharmacyManagementSystem.Enums;
using PharmacyManagementSystem.Models;
using PharmacyManagementSystem.ViewModels.Order;
using PharmacyManagementSystem.Repositories.Interfaces;

namespace PharmacyManagementSystem.Services.Implementations
{
    public class OrderService
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<CartItem> _cartItemRepository;

        private readonly CartService _cartService;

        private readonly IMapper _mapper;

        public OrderService(IGenericRepository<Order> orderRepository,
            IGenericRepository<CartItem> cartItemRepository,
            CartService cartService,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _cartItemRepository = cartItemRepository;
            _cartService = cartService;
            _mapper = mapper;
        }

        public async Task CreateAsync(OrderCreateViewModel model, string userId)
        {
            var cart = await _cartService.GetCartAsync(userId);

            var order = new Order
            {
                UserId = userId,
                DeliveryAddress = model.DeliveryAddress,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                PaymentMethod = model.PaymentMethod,
                Status = OrderStatus.Pending,
                OrderItems = new List<OrderItem>(),

                TotalItemsCount = model.OrderItems.Sum(x => x.Quantity)

            };

            decimal total = 0;

            foreach (var item in cart.Items)
            {
                order.OrderItems.Add(new OrderItem
                {
                    MedicineId = item.MedicineId,
                    Quantity = item.Quantity,
                    Price = item.Price
                });

                total += item.Price * item.Quantity;
            }

            order.TotalPrice = total;

            await _orderRepository.AddAsync(order);
            await _cartService.ClearCartAsync(userId);
        }

        public async Task<OrderViewModel> GetByIdAsync(int id)
        {
            var data = (await _orderRepository.GetAllAsync())
                .FirstOrDefault(x => x.Id == id);

            return _mapper.Map<OrderViewModel>(data);
        }

        public async Task<List<OrderViewModel>> GetByUserIdAsync(string userId)
        {
            var data = await _orderRepository.GetAllAsync();
            return _mapper.Map<List<OrderViewModel>>(
                data.Where(x => x.UserId == userId)
            );
        }

        public async Task<List<OrderViewModel>> GetAllAsync()
        {
            var data = await _orderRepository.GetAllAsync();
            return _mapper.Map<List<OrderViewModel>>(data);
        }

        public async Task UpdateStatusAsync(int orderId, Enums.OrderStatus status)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null) return;

            order.Status = status;

            _orderRepository.Update(order);
            await _orderRepository.SaveAsync();
        }

        public async Task DeleteAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null) return;

            _orderRepository.Delete(order);
            await _orderRepository.SaveAsync();
        }
    }
}
