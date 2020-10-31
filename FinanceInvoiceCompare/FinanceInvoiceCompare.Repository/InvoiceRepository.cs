using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Repository.Base;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.Repository
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<PageModel<UMatchInvoiceReportViewModel>> GetMatchInvoiceReport(MatchInvoiceReportRequestModel model)
        {
            var allInvoice = Db.Queryable<Invoice>()
        .Where((c1) => c1.IsDelete == false)
        .Select<Invoice>();


            var groupInvoice = Db.Queryable<SAPInvoiceData, Vendor>((a1, b2) => new object[]
                {
                        JoinType.Left,a1.Vendor==b2.VendorCode&&b2.IsDelete==false

                })
              .GroupBy((a1, b2) => new
              {
                  a1.Reference,
                  a1.Cocd,
                  a1.Vendor,
                  b2.VendorChName,
                  a1.DocumentNo,
                  a1.Type,
                  a1.NetDueDT,
                  a1.PstngDate,
                  a1.DocDate,
                  a1.Curr,
                  a1.PBk,
                  a1.Text,
                  a1.BlineDate,
                  a1.AmtLC2,
                  a1.Assign,
                  a1.GL,
                  a1.ClrngDoc
              })
              .Where((a1) => a1.IsDelete == false)
              .Select((a1, b2) => new SAPInvoiceData
              {
                  Cocd = a1.Cocd,
                  Reference = a1.Reference,
                  AmountInDC = SqlFunc.AggregateSum(a1.AmountInDC),
                  Vendor = a1.Vendor,
                  VendorChName = b2.VendorChName,
                  DocumentNo = a1.DocumentNo,
                  Type = a1.Type,
                  NetDueDT = a1.NetDueDT,
                  PstngDate = a1.PstngDate,
                  DocDate = a1.DocDate,
                  Curr = a1.Curr,
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
                List = await Db.Queryable(allInvoice, groupInvoice, JoinType.Left, (p1, p2) => p1.CompanyCode == p2.Cocd && p1.InvoiceNumber == p2.Reference)
                .Where((p1, p2) => p2.Reference != null)
                .WhereIF(model.CompanyCodeList.Length > 0, (p1) => SqlFunc.ContainsArray(model.CompanyCodeList, p1.CompanyCode))
                .WhereIF(!string.IsNullOrEmpty(model.InvoiceNumber), (p1) => p1.InvoiceNumber == model.InvoiceNumber)
                .Select((p1, p2) => new UMatchInvoiceReportViewModel
                {
                    InvoiceNumber = p1.InvoiceNumber,
                    InvoiceDate = p1.InvoiceDate,
                    Amount = p1.Amount,
                    CompanyCode = p1.CompanyCode,
                    MatchDate = p1.MatchDate,
                    Check = p1.Amount,
                    DataSource = p1.DataSource,
                    Cocd = p2.Cocd,
                    Reference = p2.Reference,
                    IsMatch = "Y",
                    Vendor = p2.Vendor,
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
                    ClrngDoc = p2.ClrngDoc
                })
                .ToPageListAsync(model.PageIndex, model.PageSize, totalCount),
                TotalCount = totalCount
            };
        }

        public async Task<PageModel<UMatchInvoiceReportViewModel>> GetUnMatchInvoiceReport(MatchInvoiceReportRequestModel model)
        {
            var allInvoice = Db.Queryable<Invoice>()
          .Where((c1) => c1.IsDelete == false)
          .Select<Invoice>();


            var groupInvoice = Db.Queryable<SAPInvoiceData, Vendor>((a1, b2) => new object[]
                {
                        JoinType.Left,a1.Vendor==b2.VendorCode&&b2.IsDelete==false

                })
              .GroupBy((a1, b2) => new
              {
                  a1.Reference,
                  a1.Cocd,
                  a1.Vendor,
                  b2.VendorChName,
                  a1.DocumentNo,
                  a1.Type,
                  a1.NetDueDT,
                  a1.PstngDate,
                  a1.DocDate,
                  a1.Curr,
                  a1.PBk,
                  a1.Text,
                  a1.BlineDate,
                  a1.AmtLC2,
                  a1.Assign,
                  a1.GL,
                  a1.ClrngDoc
              })
              .Where((a1) => a1.IsDelete == false)
              .Select((a1, b2) => new SAPInvoiceData
              {
                  Cocd = a1.Cocd,
                  Reference = a1.Reference,
                  AmountInDC = SqlFunc.AggregateSum(a1.AmountInDC),
                  Vendor = a1.Vendor,
                  VendorChName = b2.VendorChName,
                  DocumentNo = a1.DocumentNo,
                  Type = a1.Type,
                  NetDueDT = a1.NetDueDT,
                  PstngDate = a1.PstngDate,
                  DocDate = a1.DocDate,
                  Curr = a1.Curr,
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
                List = await Db.Queryable(allInvoice, groupInvoice, JoinType.Left, (p1, p2) => p1.CompanyCode == p2.Cocd && p1.InvoiceNumber == p2.Reference)
                .Where((p1, p2) => p2.Reference == null)
                .WhereIF(model.CompanyCodeList.Length > 0, (p1) => SqlFunc.ContainsArray(model.CompanyCodeList, p1.CompanyCode))
                .WhereIF(!string.IsNullOrEmpty(model.InvoiceNumber), (p1) => p1.InvoiceNumber == model.InvoiceNumber)
                .Select((p1, p2) => new UMatchInvoiceReportViewModel
                {
                    InvoiceNumber = p1.InvoiceNumber,
                    InvoiceDate = p1.InvoiceDate,
                    Amount = p1.Amount,
                    CompanyCode = p1.CompanyCode,
                    MatchDate = p1.MatchDate,
                    Check = p1.Amount,
                    DataSource = p1.DataSource,
                    Cocd = p2.Cocd,
                    Reference = p2.Reference,
                    IsMatch = "N",
                    Vendor = p2.Vendor,
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
                    ClrngDoc = p2.ClrngDoc
                })
                .ToPageListAsync(model.PageIndex, model.PageSize, totalCount),
                TotalCount = totalCount
            };
        }


        public async Task<PageModel<UMatchInvoiceReportViewModel>> GetCompareMatchInvoiceReport(MatchInvoiceReportRequestModel model)
        {
            var allInvoice = Db.Queryable<Invoice>()
            .Where((c1) => c1.IsDelete == false)
            .Select<Invoice>();


            var groupInvoice = Db.Queryable<SAPInvoiceData, Vendor>((a1, b2) => new object[]
                {
                        JoinType.Left,a1.Vendor==b2.VendorCode&&b2.IsDelete==false

                })
              .GroupBy((a1, b2) => new
              {
                  a1.Reference,
                  a1.Cocd,
                  a1.Vendor,
                  b2.VendorChName,
                  a1.DocumentNo,
                  a1.Type,
                  a1.NetDueDT,
                  a1.PstngDate,
                  a1.DocDate,
                  a1.Curr,
                  a1.PBk,
                  a1.Text,
                  a1.BlineDate,
                  a1.AmtLC2,
                  a1.Assign,
                  a1.GL,
                  a1.ClrngDoc
              })
              .Where((a1) => a1.IsDelete == false)
              .Select((a1, b2) => new SAPInvoiceData
              {
                  Cocd = a1.Cocd,
                  Reference = a1.Reference,
                  AmountInDC = SqlFunc.AggregateSum(a1.AmountInDC),
                  Vendor = a1.Vendor,
                  VendorChName = b2.VendorChName,
                  DocumentNo = a1.DocumentNo,
                  Type = a1.Type,
                  NetDueDT = a1.NetDueDT,
                  PstngDate = a1.PstngDate,
                  DocDate = a1.DocDate,
                  Curr = a1.Curr,
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
                List = await Db.Queryable(allInvoice, groupInvoice, JoinType.Left, (p1, p2) => p1.CompanyCode == p2.Cocd && p1.InvoiceNumber == p2.Reference)
                .WhereIF(model.Compare?.ToUpper() == "MATCH", (p1, p2) => p2.Reference != null)
                .WhereIF(model.Compare?.ToUpper() == "UNMATCH", (p1, p2) => p2.Reference == null)
                .WhereIF(model.CompanyCodeList.Length > 0, (p1) => SqlFunc.ContainsArray(model.CompanyCodeList, p1.CompanyCode))
                .WhereIF(!string.IsNullOrEmpty(model.InvoiceNumber), (p1) => p1.InvoiceNumber.Contains(model.InvoiceNumber))
                .Select((p1, p2) => new UMatchInvoiceReportViewModel
                {
                    InvoiceNumber = p1.InvoiceNumber,
                    InvoiceDate = p1.InvoiceDate,
                    Amount = p1.Amount,
                    CompanyCode = p1.CompanyCode,
                    MatchDate = p1.MatchDate,
                    Check = SqlFunc.IIF(p2.AmountInDC == null, p1.Amount, p1.Amount - p2.AmountInDC),
                    DataSource = p1.DataSource,
                    Cocd = p2.Cocd,
                    Reference = p2.Reference,
                    IsMatch = SqlFunc.IIF(p2.Reference == null, "N", "Y"),
                    Vendor = p2.Vendor,
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
                    ClrngDoc = p2.ClrngDoc
                })
                .ToPageListAsync(model.PageIndex, model.PageSize, totalCount),
                TotalCount = totalCount
            };
        }


        public async Task<List<UMatchInvoiceReportViewModel>> GetAllCompareMatchInvoiceReport(MatchInvoiceReportRequestModel model)
        {
            var allInvoice = Db.Queryable<Invoice>()
            .Where((c1) => c1.IsDelete == false)
            .Select<Invoice>();


            var groupInvoice = Db.Queryable<SAPInvoiceData, Vendor>((a1, b2) => new object[]
                {
                        JoinType.Left,a1.Vendor==b2.VendorCode&&b2.IsDelete==false

                })
              .GroupBy((a1, b2) => new
              {
                  a1.Reference,
                  a1.Cocd,
                  a1.Vendor,
                  b2.VendorChName,
                  a1.DocumentNo,
                  a1.Type,
                  a1.NetDueDT,
                  a1.PstngDate,
                  a1.DocDate,
                  a1.Curr,
                  a1.PBk,
                  a1.Text,
                  a1.BlineDate,
                  a1.AmtLC2,
                  a1.Assign,
                  a1.GL,
                  a1.ClrngDoc
              })
              .Where((a1) => a1.IsDelete == false)
              .Select((a1, b2) => new SAPInvoiceData
              {
                  Cocd = a1.Cocd,
                  Reference = a1.Reference,
                  AmountInDC = SqlFunc.AggregateSum(a1.AmountInDC),
                  Vendor = a1.Vendor,
                  VendorChName = b2.VendorChName,
                  DocumentNo = a1.DocumentNo,
                  Type = a1.Type,
                  NetDueDT = a1.NetDueDT,
                  PstngDate = a1.PstngDate,
                  DocDate = a1.DocDate,
                  Curr = a1.Curr,
                  PBk = a1.PBk,
                  Text = a1.Text,
                  BlineDate = a1.BlineDate,
                  AmtLC2 = a1.AmtLC2,
                  Assign = a1.Assign,
                  GL = a1.GL,
                  ClrngDoc = a1.ClrngDoc
              });

            RefAsync<int> totalCount = 0;


            return await Db.Queryable(allInvoice, groupInvoice, JoinType.Left, (p1, p2) => p1.CompanyCode == p2.Cocd && p1.InvoiceNumber == p2.Reference)
                  .WhereIF(model.Compare?.ToUpper() == "MATCH", (p1, p2) => p2.Reference != null)
                  .WhereIF(model.Compare?.ToUpper() == "UNMATCH", (p1, p2) => p2.Reference == null)
                  .WhereIF(model.CompanyCodeList.Length > 0, (p1) => SqlFunc.ContainsArray(model.CompanyCodeList, p1.CompanyCode))
                  .WhereIF(!string.IsNullOrEmpty(model.InvoiceNumber), (p1) => p1.InvoiceNumber.Contains(model.InvoiceNumber))
                  .Select((p1, p2) => new UMatchInvoiceReportViewModel
                  {
                      InvoiceNumber = p1.InvoiceNumber,
                      InvoiceDate = p1.InvoiceDate,
                      Amount = p1.Amount,
                      CompanyCode = p1.CompanyCode,
                      MatchDate = p1.MatchDate,
                      Check = SqlFunc.IIF(p2.AmountInDC == null, p1.Amount, p1.Amount - p2.AmountInDC),
                      DataSource = p1.DataSource,
                      Cocd = p2.Cocd,
                      Reference = p2.Reference,
                      IsMatch = SqlFunc.IIF(p2.Reference == null, "N", "Y"),
                      Vendor = p2.Vendor,
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
                      ClrngDoc = p2.ClrngDoc
                  }).ToListAsync();

        }


        /// <summary>
        /// 查询支付发票的报表信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PageModel<Payment>> GetInvoicePaymentReport(MatchInvoiceReportRequestModel model)
        {


            var mainInvoice = Db.Queryable<Payment, Vendor>((a1, b2) => new object[]
                {
                        JoinType.Left,a1.Vendor==b2.VendorCode&&b2.IsDelete==false

                })
              .Where((a1) => a1.IsDelete == false)
              .Select((a1, b2) => new Payment
              {
                  Cocd = a1.Cocd,
                  Reference = a1.Reference,
                  Vendor = a1.Vendor,
                  VendorChName = b2.VendorChName,
                  DocumentNo = a1.DocumentNo,
                  Type = a1.Type,
                  NetDueDT = a1.NetDueDT,
                  PstngDate = a1.PstngDate,
                  DocDate = a1.DocDate,
                  Curr = a1.Curr,
                  PBk = a1.PBk
              });

            var allInvoice = Db.Queryable<Invoice>()
            .Where((c1) => c1.IsDelete == false)
            .Select<Invoice>();


            RefAsync<int> totalCount = 0;


            return new PageModel<UMatchInvoiceReportViewModel>()
            {
                List = await Db.Queryable(mainInvoice, allInvoice, JoinType.Left, (p1, p2) => p1.Cocd == p2.CompanyCode && p1.Reference == p2.InvoiceNumber)
                  .WhereIF(model.CompanyCodeList.Length > 0, (p1) => SqlFunc.ContainsArray(model.CompanyCodeList, p1.Cocd))
                  .WhereIF(!string.IsNullOrEmpty(model.InvoiceNumber), (p1) => p1.Cocd.Contains(model.InvoiceNumber))
                  .Select((p1, p2) => new Payment
                  {
                      Cocd = p1.Cocd,
                      Reference = p1.Reference,
                      Vendor = p1.Vendor,
                      VendorChName = p1.VendorChName,
                      DocumentNo = p1.DocumentNo,
                      Type = p1.Type,
                      NetDueDT = p1.NetDueDT,
                      PstngDate = p1.PstngDate,
                      DocDate = p1.DocDate,
                      Curr = p1.Curr,
                      PBk = p1.PBk,
                      MatchDate = p2.MatchDate,
                      RecivedStatus = "Y",
                      Day = SqlFunc.IIF(p2.MatchDate >= p1.PstngDate, p2.MatchDate - p1.DocDate, p1.PstngDate - p1.DocDate),
                      BlockStatus = string.Empty
                  })
                  .ToPageListAsync(model.PageIndex, model.PageSize, totalCount),
                  TotalCount = totalCount
            };
        }

    }
}
