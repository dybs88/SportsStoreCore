using Microsoft.EntityFrameworkCore;
using SportsStore.DAL.Contexts;
using SportsStore.Models.DAL.Repos.SalesSchema;
using SportsStore.Models.DictionaryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.DAL.Repos.DictionarySchema
{
    public class VatRateRepository : IVatRateRepository
    {
        private DictionaryDbContext _context;
        private IProductRepository _productRepository;
        private IOrderRepository _orderRepository;

        public VatRateRepository(DictionaryDbContext context, IProductRepository productRepo, IOrderRepository orderRepo)
        {
            _context = context;
            _productRepository = productRepo;
            _orderRepository = orderRepo;
        }

        public IEnumerable<VatRate> VatRates => _context.VatRates;

        public VatRate GetVatRate(int vatRateId)
        {
            return VatRates.FirstOrDefault(vr => vr.VatRateId == vatRateId);
        }

        public dynamic DeleteVatRate(int vatRateId)
        {
            if (vatRateId == 0)
                return new { Message = "" };

            if (_productRepository.Products.Any(p => p.VatRateId == vatRateId) && _orderRepository.Orders.Any(p => p.VatRateId == vatRateId))
                return new { Result = false, Message = "Stawka VAT została już wykorzystana w aplikacji" };

            var vatRate = GetVatRate(vatRateId);

            _context.VatRates.Remove(vatRate);

            _context.SaveChanges();
            return new { Result = true, Message = "Stawka VAT została usunięta" };
        }

        public dynamic SaveVatRate(VatRate vatRate)
        {
            if(vatRate.VatRateId == 0)
            {
                _context.VatRates.Add(vatRate);
            }
            else
            {
                VatRate dbEntry = GetVatRate(vatRate.VatRateId);
                
                if(dbEntry != null)
                {
                    dbEntry.Symbol = vatRate.Symbol;
                    dbEntry.Value = vatRate.Value;
                }

            }

            try
            {
                _context.SaveChanges();
            }
            catch(DbUpdateException e)
            {
                return new { Result = false, Message = "Nie można zapisać stawki VAT z symbolem, który został już wykorzystany" };
            }

            return new { Result = true, Message = $"Zapisano stawkę VAT {vatRate.Symbol} {vatRate.Value}%", vatRate.VatRateId };
        }
    }
}
