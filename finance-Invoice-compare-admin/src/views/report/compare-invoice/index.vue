<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.invoiceNumber" style="width: 150px;" placeholder="发票号码" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-select
        v-model="listQuery.compare"
        class="filter-item"
        placeholder="请选择条件"
        style="width: 150px;"
        clearable
        @change="selectInit"
      >
        <el-option
          v-for="item in isMatchList"
          :key="item.id"
          :label="item.value"
          :value="item.value"
        />
      </el-select>
      <el-select
        v-model="listQuery.companyCode"
        class="filter-item"
        placeholder="--请选择公司编码--"
        clearable
        @change="handleFilter"
      >
        <el-option
          v-for="item in companyCodeList"
          :key="item.Id"
          :label="item.code"
          :value="item.code"
        />
      </el-select>
      <el-button v-waves class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-search" @click="handleFilter">
        查询
      </el-button>
      <el-button v-waves class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-download" :loading="downloading" @click="exportExcel">
        导出EXCEL
      </el-button>
      <el-button v-waves class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-refresh" :loading="matching" @click="match">
        手动匹配
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
      <el-table-column label="Coupa ID" width="120" align="center">
        <template slot-scope="{row}">
          <span>{{ row.coupaID }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Invoice Date" width="150" align="center" show-overflow-tooltip>
        <template slot-scope="{row}">
          <span>{{ row.invoiceDate }}</span>
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
        <template slot-scope="{row}">
          <span>{{ row.vendor }}</span>
        </template>
      </el-table-column>
      <el-table-column label="VendorChName" width="200" align="center" show-overflow-tooltip fixed>
        <template slot-scope="{row}">
          <span>{{ row.vendorChName }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Reference" width="120" align="center">
        <template slot-scope="{row}">
          <span>{{ row.reference }}</span>
        </template>
      </el-table-column>
      <el-table-column label="CompanyCode" width="120" align="center">
        <template slot-scope="{row}">
          <span>{{ row.cocd }}</span>
        </template>
      </el-table-column>
      <el-table-column label="DocumentNo" width="120" align="center">
        <template slot-scope="{row}">
          <span>{{ row.documentNo }}</span>
        </template>
      </el-table-column>
      <el-table-column label="DocType" width="120" align="center">
        <template slot-scope="{row}">
          <span>{{ row.type }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Net Due Date" width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.netDueDT }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Pstng Date" width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.pstngDate }}</span>
        </template>
      </el-table-column>
      <el-table-column label="DocDate" width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.docDate }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Amount" width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.amountInDC }}</span>
        </template>
      </el-table-column>
      <el-table-column label="PBK" width="120" align="center">
        <template slot-scope="{row}">
          <span>{{ row.pbk }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Text" width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.text }}</span>
        </template>
      </el-table-column>
      <el-table-column label="BlineDate" width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.blineDate }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Amt. LC2" width="120" align="center">
        <template slot-scope="{row}">
          <span>{{ row.amtLC2 }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Assignment" width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.assign }}</span>
        </template>
      </el-table-column>
      <el-table-column label="G/L" width="120" align="center">
        <template slot-scope="{row}">
          <span>{{ row.gL }}</span>
        </template>
      </el-table-column>
      <el-table-column label="ClrngDoc" width="120" align="center">
        <template slot-scope="{row}">
          <span>{{ row.clrngDoc }}</span>
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
      <el-table-column label="MatchDate" width="160" align="center">
        <template slot-scope="{row}">
          <span>{{ row.matchDate }}</span>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.pageindex" :limit.sync="listQuery.pagesize" @pagination="getList" />

  </div>
</template>

<script>
import { getCompareMatchInvoiceReport, getAllCompareMatchInvoiceReport } from '@/api/report'
import { matchInvoice } from '@/api/invoice'
import waves from '@/directive/waves' // waves directive
import { export_json_to_excel } from '@/utils/Export2Excel'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import { parseTime } from '@/utils'

export default {
  name: 'CompareInvoiceReportListTable',
  components: { Pagination },
  directives: { waves },
  data() {
    return {
      isMatchList: [{
        id: 1,
        value: 'Match'
      }, {
        id: 2,
        value: 'Unmatch'
      }],
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: false,
      matching: false,
      listQuery: {
        pageindex: 1,
        pagesize: 20,
        invoiceNumber: '',
        companyCode: '',
        compare: '',
        multipleCompany: this.$store.getters.company.map(item => {
          return item.code
        }).join(',')
      },
      companyCodeList: this.$store.getters.company,
      uploadLoading: false,
      downloading: false,
      filename: '发票比对信息',
      autoWidth: true,
      bookType: 'xlsx'
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      if (this.$store.getters.company && this.$store.getters.company.length > 0) {
        this.listLoading = true
        getCompareMatchInvoiceReport(this.listQuery).then(res => {
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
    match() {
      this.matching = true
      matchInvoice().then(res => {
        this.matching = false
        if (res.success === true) {
          this.$message({
            message: '已重新匹配',
            type: 'success'
          })
          this.getList()
        } else {
          this.$message.error(res.message)
        }
      }).catch(
        this.matching = false
      )
    },
    exportExcel() {
      this.downloading = true
      getAllCompareMatchInvoiceReport(this.listQuery)
        .then(res => {
          const tHeader = [
            '发票号码',
            '厂别',
            'VendorCode',
            'VendorChName',
            'Coupa ID',
            'Invoice Date',
            '发票金额',
            '是否一致',

            'Reference',
            'CompanyCode',
            'DocumentNo',
            'DocType',
            'Net Due Date',
            'Pstng Date',
            'Doc Date',
            'Amount',
            'PBk',
            'Text',
            'BlineDate',
            'Amt. LC2',
            'Assignment',
            'G/L',
            'ClrngDoc',
            'Check',
            'Data Source',
            'Match Date'
          ]
          const filterVal = [
            'invoiceNumber',
            'companyCode',
            'vendor',
            'vendorChName',
            'coupaID',
            'invoiceDate',
            'amount',
            'isMatch',
            'reference',
            'cocd',
            'documentNo',
            'docType',
            'netDueDate',
            'pstngDate',
            'docDate',
            'amountInDC',
            'pBk',
            'text',
            'blineDate',
            'amtLC2',
            'assignment',
            'gl',
            'clrngDoc',
            'check',
            'dataSource',
            'matchDate'
          ]
          const list = res.response
          const data = this.formatJson(filterVal, list)
          export_json_to_excel({
            header: tHeader,
            data,
            filename: this.filename,
            autoWidth: this.autoWidth,
            bookType: this.bookType
          })
          this.downloading = false
        }
        ).catch(
          this.downloading = false
        )
    },
    handleFilter() {
      this.listQuery.pageindex = 1
      this.getList()
    },
    formatJson(filterVal, jsonData) {
      return jsonData.map(v =>
        filterVal.map(j => {
          if (j === 'timestamp') {
            return parseTime(v[j])
          } else {
            return v[j]
          }
        })
      )
    },
    selectInit() {
      this.handleFilter()
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
