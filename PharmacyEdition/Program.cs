// See https://aka.ms/new-console-template for more information

using PharmacyEdition.Data.IRepositories;
using PharmacyEdition.Data.Repositories;
using PharmacyEdition.Domain.Enums;
using PharmacyEdition.Models;
using PharmacyEdition.Service.DTOs;
using PharmacyEdition.Service.Interfaces;
using PharmacyEdition.Service.Services;

GenericRepository<Medicine> medicineRepository = new GenericRepository<Medicine>();

IMedicineService medicineService = new MedicineService();
IOrderService orderService = new OrderService();

MedicineCreationDto medicineDto = new MedicineCreationDto()
{
    Name = "Parestamol",
    Count = 10,
    Description = "Bosh",
    Price = 1000
};

var paymentDto = new PaymentCreationDto()
{
    IsPaid = true,
    OrderId = 2,
    Type = PaymentType.Humo
};

var medicine = await medicineService.GetByIdAsync(1);
var medicine2 = await medicineService.GetByIdAsync(2);

OrderCreationDto orderCreationDto = new OrderCreationDto()
{
    Medicines = new List<Medicine>() { medicine.Value, medicine2.Value },
    Payment = paymentDto
};

//await medicineService.CreateAsync(medicineDto);
await orderService.CreateAsync(orderCreationDto);
