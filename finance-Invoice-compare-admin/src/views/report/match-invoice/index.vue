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
    >
      <el-table-column label="ID" prop="id" align="center" width="80">
        <template slot-scope="{row}">
          <span>{{ row.id }}</span>
        </template>
      </el-table-column>
      <el-table-column label="VendorCode" align="center">
        <template slot-scope="{row}">
          <span>{{ row.vendor }}</span>
        </template>
      </el-table-column>
      <el-table-column label="VendorChName" align="center">
        <template slot-scope="{row}">
          <span>{{ row.vendorChName }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Reference" align="center">
        <template slot-scope="{row}">
          <span>{{ row.reference }}</span>
        </template>
      </el-table-column>
      <el-table-column label="CompanyCode" align="center">
        <template slot-scope="{row}">
          <span>{{ row.cocd }}</span>
        </template>
      </el-table-column>
      <el-table-column label="DocumentNo" align="center">
        <template slot-scope="{row}">
          <span>{{ row.documentNo }}</span>
        </template>
      </el-table-column>
      <el-table-column label="DocType" align="center">
        <template slot-scope="{row}">
          <span>{{ row.type }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Net Due Date" align="center">
        <template slot-scope="{row}">
          <span>{{ row.netDueDT }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Pstng Date" align="center">
        <template slot-scope="{row}">
          <span>{{ row.pstngDate }}</span>
        </template>
      </el-table-column>
      <el-table-column label="DocDate" align="center">
        <template slot-scope="{row}">
          <span>{{ row.docDate }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Currency" align="center">
        <template slot-scope="{row}">
          <span>{{ row.curr }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Amount" align="center">
        <template slot-scope="{row}">
          <span>{{ row.amountInDC }}</span>
        </template>
      </el-table-column>
      <el-table-column label="IsMatch" align="center">
        <template slot-scope="{row}">
          <span>{{ row.isMatch }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Check" align="center">
        <template slot-scope="{row}">
          <span>{{ row.check }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Remark" align="center">
        <template slot-scope="{row}">
          <span>{{ row.remark }}</span>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.pageindex" :limit.sync="listQuery.pagesize" @pagination="getList" />

  </div>
</template>

<script>
import { getMatchInvoiceRepost } from '@/api/report'
import waves from '@/directive/waves' // waves directive
import Pagination from '@/components/Pagination' // secondary package based on el-pagination

export default {
  name: 'MatchInvoiceReportListTable',
  components: { Pagination },
  directives: { waves },
  data() {
    return {
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: {
        pageindex: 1,
        pagesize: 20,
        invoiceNumber: ''
      },
      uploadLoading: false
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      this.listLoading = true
      getMatchInvoiceRepost(this.listQuery).then(res => {
        this.list = res.response.list
        this.total = res.response.totalCount
        this.listLoading = false
      }).catch(
        this.listLoading = false
      )
    },
    handleFilter() {
      this.listQuery.pageindex = 1
      this.getList()
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
