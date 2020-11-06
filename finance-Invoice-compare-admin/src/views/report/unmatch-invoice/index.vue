<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.invoiceNumber" style="width: 150px;" placeholder="发票号码" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-button v-waves class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-search" @click="handleFilter">
        查询
      </el-button>
    </div>

    <el-table
      :key="tableKey"
      v-loading="listLoading"
      :data="list"
      :header-cell-style="{background:'#eef1f6',color:'#606266'}"
      fit
      highlight-current-row
      :cell-style="setCellColor"
    >
      <el-table-column label="发票号码" width="160" align="center" fixed>
        <template slot-scope="{row}">
          <span>{{ row.invoiceNumber }}</span>
        </template>
      </el-table-column>
      <el-table-column label="厂别" width="120" align="center" fixed>
        <template slot-scope="{row}">
          <span>{{ row.companyCode }}</span>
        </template>
      </el-table-column>
      <el-table-column label="发票金额" width="120" align="center">
        <template slot-scope="{row}">
          <span>{{ row.amount }}</span>
        </template>
      </el-table-column>
      <el-table-column label="是否一致" width="120" align="center">
        <template slot-scope="{row}">
          <span>{{ row.isMatch }}</span>
        </template>
      </el-table-column>
      <el-table-column label="VendorCode" width="100" align="center" fixed>
        <template>
          <span>#N/A</span>
        </template>
      </el-table-column>
      <el-table-column label="VendorChName" width="200" align="center" fixed>
        <template>
          <span>#N/A</span>
        </template>
      </el-table-column>
      <el-table-column label="Reference" width="120" align="center">
        <template>
          <span>#N/A</span>
        </template>
      </el-table-column>
      <el-table-column label="CompanyCode" width="120" align="center">
        <template>
          <span>#N/A</span>
        </template>
      </el-table-column>
      <el-table-column label="DocumentNo" width="120" align="center">
        <template>
          <span>#N/A</span>
        </template>
      </el-table-column>
      <el-table-column label="DocType" width="120" align="center">
        <template>
          <span>#N/A</span>
        </template>
      </el-table-column>
      <el-table-column label="Net Due Date" width="150" align="center">
        <template>
          <span>#N/A</span>
        </template>
      </el-table-column>
      <el-table-column label="Pstng Date" width="150" align="center">
        <template>
          <span>#N/A</span>
        </template>
      </el-table-column>
      <el-table-column label="DocDate" width="150" align="center">
        <template>
          <span>#N/A</span>
        </template>
      </el-table-column>
      <el-table-column label="Amount" width="120" align="center">
        <template>
          <span>#N/A</span>
        </template>
      </el-table-column>
      <el-table-column label="PBK" width="120" align="center">
        <template>
          <span>#N/A</span>
        </template>
      </el-table-column>
      <el-table-column label="Text" width="120" align="center">
        <template>
          <span>#N/A</span>
        </template>
      </el-table-column>
      <el-table-column label="BlineDate" width="150" align="center">
        <template>
          <span>#N/A</span>
        </template>
      </el-table-column>
      <el-table-column label="Amt. LC2" width="120" align="center">
        <template>
          <span>#N/A</span>
        </template>
      </el-table-column>
      <el-table-column label="Assignment" width="150" align="center">
        <template>
          <span>#N/A</span>
        </template>
      </el-table-column>
      <el-table-column label="G/L" width="120" align="center">
        <template>
          <span>#N/A</span>
        </template>
      </el-table-column>
      <el-table-column label="ClrngDoc" width="120" align="center">
        <template>
          <span>#N/A</span>
        </template>
      </el-table-column>
      <el-table-column label="Check" width="120" align="center">
        <template slot-scope="{row}">
          <span>{{ row.check }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Data Source" width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.dataSource }}</span>
        </template>
      </el-table-column>
      <el-table-column label="MatchDate" width="150" align="center">
        <template>
          <span>#N/A</span>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.pageindex" :limit.sync="listQuery.pagesize" @pagination="getList" />

  </div>
</template>

<script>
import { getUnMatchInvoiceReport } from '@/api/report'
import waves from '@/directive/waves' // waves directive
import Pagination from '@/components/Pagination' // secondary package based on el-pagination

export default {
  name: 'UnMatchInvoiceReportListTable',
  components: { Pagination },
  directives: { waves },
  data() {
    return {
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: false,
      listQuery: {
        pageindex: 1,
        pagesize: 20,
        invoiceNumber: '',
        multipleCompany: this.$store.getters.company.map(item => {
          return item.code
        }).join(',')

        // this.$store.getters.company.map(item => {
        //   return item.code
        // })
      },
      uploadLoading: false
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      if (this.$store.getters.company && this.$store.getters.company.length > 0) {
        this.listLoading = true
        getUnMatchInvoiceReport(this.listQuery).then(res => {
          this.list = res.response.list
          this.total = res.response.totalCount
          this.listLoading = false
        }).catch(
          this.listLoading = false
        )
      } else {
        this.$message.warning('未授权公司，无法查看相应公司的报表信息')
      }
    },
    handleFilter() {
      this.listQuery.pageindex = 1
      this.getList()
    },
    setCellColor({ row, column, rowIndex, columnIndex }) {
      if (['发票号码', '厂别', 'VendorCode', 'VendorChName'].includes(column.label)) {
        return 'background-color:#98c3e4;'
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
