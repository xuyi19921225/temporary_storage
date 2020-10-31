using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceInvoiceCompare.WebApi.Repository
{
    public class PaymentRepository:BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(IUnitOfWork unitOfWork):base(unitOfWork)
        {

        }
    }
}
