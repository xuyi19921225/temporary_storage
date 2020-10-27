using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Repository.Base;
using SqlSugar;
using System;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.Repository
{
    public class InvoiceRepository:BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(IUnitOfWork unitOfWork):base(unitOfWork)
        {
         
        }

        public async Task<PageModel<UMatchInvoiceReportViewModel>> GetMatchInvoiceRepost(MatchInvoiceReportRequestModel model) 
        {
            var allInvoice = Db.Queryable<Invoice>()
            .Where((a1) => a1.IsDelete == false)
            .WhereIF(model.CompanyCodeList.Length>0, (a1) => SqlFunc.ContainsArray(model.CompanyCodeList, a1.CompanyCode))
            .WhereIF(!string.IsNullOrEmpty(model.InvoiceNumber), (a1) => a1.InvoiceNumber.Contains(model.InvoiceNumber))
            .Select<Invoice>();


            var groupInvoice = Db.Queryable<SAPInvoiceData,Vendor>((a1, b2) => new object[]
               {
                        JoinType.Left,a1.Vendor==b2.VendorCode&&b2.IsDelete==false

               })
              .GroupBy(it => new { it.Reference, it.Cocd })
              .Where((a1) => a1.IsDelete == false)
              .WhereIF(model.CompanyCodeList.Length > 0, (a1) => SqlFunc.ContainsArray(model.CompanyCodeList, a1.Cocd))
              .WhereIF(!string.IsNullOrEmpty(model.InvoiceNumber), (a1) => a1.Reference.Contains(model.InvoiceNumber))
              .Select((a1,b2) => new SAPInvoiceData { 
                  Cocd = a1.Cocd, 
                  Reference = a1.Reference,
                  AmountInDC=SqlFunc.AggregateSum(a1.AmountInDC),
                  VendorChName = a1.VendorChName,
                  DocumentNo = a1.DocumentNo,
                  Type = a1.Type,
                  NetDueDT = a1.NetDueDT,
                  PstngDate = a1.PstngDate,
                  DocDate = a1.DocDate,
                  Curr = a1.Curr,
                  Remark = a1.Remark,
                  PBk = a1.PBk,
                  Text = a1.Text,
                  BlineDate = a1.BlineDate,
                  AmtLC2 = a1.AmtLC2,
                  Assign = a1.Assign,
                  GL = a1.GL,
                  ClrngDoc = a1.ClrngDoc
              });

            RefAsync<int> totalCount = 0;


            return new PageModel<UMatchInvoiceReportViewModel>()
            {
                List = await Db.Queryable(allInvoice, groupInvoice, (p1, p2) => p1.CompanyCode == p2.Cocd && p1.InvoiceNumber == p2.Reference).Select((p1, p2) => new UMatchInvoiceReportViewModel
                {
                    Vendor = p2.Vendor,
                    Cocd = p2.Cocd,
                    Reference = p2.Reference,
                    IsMatch = "Y",
                    VendorChName = p2.VendorChName,
                    DocumentNo = p2.DocumentNo,
                    Type = p2.Type,
                    NetDueDT = p2.NetDueDT,
                    PstngDate = p2.PstngDate,
                    DocDate = p2.DocDate,
                    Curr = p2.Curr,
                    AmountInDC = p2.AmountInDC,
                    PBk = p2.PBk,
                    Text = p2.Text,
                    BlineDate = p2.BlineDate,
                    AmtLC2 = p2.AmtLC2,
                    Assign = p2.Assign,
                    GL = p2.GL,
                    ClrngDoc = p2.ClrngDoc,
                    MatchDate=DateTime.UtcNow,
                    Check=p1.Amount-p2.AmountInDC,
                    DataSource=p1.DataSource
                })
                .ToPageListAsync(model.PageIndex, model.PageSize, totalCount),
                TotalCount = totalCount
            };
        }
    }
}
