using FinanceInvoiceCompare.WebApi.IRepository;
using FinanceInvoiceCompare.WebApi.Model;
using FinanceInvoiceCompare.WebApi.Repository.Base;
using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceInvoiceCompare.WebApi.Repository
{
    public class SAPRepository : BaseRepository<SAPInvoiceData>, ISAPRepository
    {
        public SAPRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<PageModel<SAPInvoiceData>> GetSAPInvoiceList(SAPRequestModel model)
        {
            var allSapInvoice = Db.Queryable<SAPInvoiceData, Vendor, Invoice>((a1, b2, c3) => new object[]
               {
                        JoinType.Left,a1.Vendor==b2.VendorCode&&a1.Cocd==b2.CompanyCode&&b2.IsDelete==false,
                        JoinType.Left,a1.Reference==c3.InvoiceNumber&&a1.Cocd==c3.CompanyCode&&c3.IsDelete==false

               })
            .Where((a1) => a1.IsDelete == false)
            .Select((a1, b2, c3) => new SAPInvoiceData 
            {
                Id=a1.Id,
                Vendor=a1.Vendor,
                Cocd=a1.Cocd,
                Reference=a1.Reference,
                IsMatch = SqlFunc.IIF(SqlFunc.IsNull(c3.InvoiceNumber, string.Empty)!=string.Empty, "Y", "N") ,
                VendorChName=b2.VendorChName,
                DocumentNo=a1.DocumentNo,
                Type=a1.Type,
                NetDueDT=a1.NetDueDT,
                PstngDate=a1.PstngDate,
                DocDate=a1.DocDate,
                Curr=a1.Curr,
                AmountInDC=a1.AmountInDC,
                Remark=a1.Remark,
                PBk=a1.PBk,
                Text=a1.Text,
                BlineDate=a1.BlineDate,
                AmtLC2=a1.AmtLC2,
                Assign=a1.Assign,
                GL=a1.GL,
                ClrngDoc=a1.ClrngDoc,
                CreateAt = a1.CreateAt,
                CreateBy=a1.CreateBy,
                UpdatedAt=a1.UpdatedAt,
                UpdatedBy=a1.UpdatedBy
            });


            var groupSapInvoice = Db.Queryable<SAPInvoiceData>()
              .Where(it => it.IsDelete == false)
              .GroupBy(it => new { it.Reference, it.Cocd })
              .Select(it => new SAPInvoiceData { Cocd = it.Cocd, Reference = it.Reference, Check = SqlFunc.AggregateCount(it.Id) });

            RefAsync<int> totalCount = 0;


            return new PageModel<SAPInvoiceData>()
            {
                List = await Db.Queryable(allSapInvoice, groupSapInvoice, (p1, p2) => p1.Cocd == p2.Cocd && p1.Reference == p2.Reference)
                .WhereIF(!string.IsNullOrEmpty(model.InvoiceNumber), (p1)=> p1.Reference.Contains(model.InvoiceNumber))
                .Select((p1,p2)=>new SAPInvoiceData 
                {
                    Id=p1.Id,
                    Vendor = p1.Vendor,
                    Cocd = p1.Cocd,
                    Reference = p1.Reference,
                    IsMatch = p1.IsMatch,
                    VendorChName = p1.VendorChName,
                    DocumentNo = p1.DocumentNo,
                    Type = p1.Type,
                    NetDueDT = p1.NetDueDT,
                    PstngDate = p1.PstngDate,
                    DocDate = p1.DocDate,
                    Curr = p1.Curr,
                    AmountInDC = p1.AmountInDC,
                    Remark = p1.Remark,
                    PBk = p1.PBk,
                    Text = p1.Text,
                    BlineDate = p1.BlineDate,
                    AmtLC2 = p1.AmtLC2,
                    Assign = p1.Assign,
                    GL = p1.GL,
                    ClrngDoc = p1.ClrngDoc,
                    Check=p2.Check,
                    CreateAt = p1.CreateAt,
                    CreateBy = p1.CreateBy,
                    UpdatedAt = p1.UpdatedAt,
                    UpdatedBy = p1.UpdatedBy
                })
                .OrderBy((p1)=>p1.Id,OrderByType.Desc)
                .ToPageListAsync(model.PageIndex, model.PageSize, totalCount),
                TotalCount = totalCount
            };

        }
    }
}
