<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.invoiceNumber" style="width: 150px;" placeholder="发票号码" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-input v-model="listQuery.version" style="width: 150px;" placeholder="版本" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-select
        v-model="listQuery.companyCode"
        class="filter-item"
        placeholder="请选择公司编码"
        style="width: 150px;"
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
      <el-select
        v-model="listQuery.blockStatus"
        class="filter-item"
        placeholder="BlockStatus"
        style="width: 150px;"
        clearable
        @change="handleFilter"
      >
        <el-option
          v-for="item in blockStatusList"
          :key="item.Id"
          :label="item.blockStatus"
          :value="item.blockStatus"
        />
      </el-select>
      <el-button v-waves class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-search" @click="handleFilter">
        查询
      </el-button>
      <el-upload
        ref="upload"
        class="upload-demo filter-item"
        style="margin-left: 10px;"
        action
        :http-request="upload"
        :on-change="onChange"
        accept=".xlsx,application/vnd.ms-excel"
        :show-file-list="false"
      >
        <el-button type="primary" :loading="uploadLoading" icon="el-icon-upload">批量导入</el-button>
      </el-upload>
      <el-button v-waves class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-download" :loading="downloading" @click="exportExcel">
        导出EXCEL
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
      <el-table-column label="Version" width="160" align="center" fixed>
        <template slot-scope="{row}">
          <span>{{ row.version }}</span>
        </template>
      </el-table-column>
      <el-table-column label="VendorCode" width="160" align="center" fixed>
        <template slot-scope="{row}">
          <span>{{ row.vendor }}</span>
        </template>
      </el-table-column>
      <el-table-column label="VendorChName" width="200" show-overflow-tooltip align="center" fixed>
        <template slot-scope="{row}">
          <span>{{ row.vendorChName }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Reference" width="120" align="center">
        <template slot-scope="{row}">
          <span>{{ row.reference }}</span>
        </template>
      </el-table-column>
      <el-table-column label="CompanyCode" width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.cocd }}</span>
        </template>
      </el-table-column>
      <el-table-column label="DocumentNo" width="100" align="center">
        <template slot-scope="{row}">
          <span>{{ row.documentNo }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="DocType"
        width="120"
        align="center"
      >
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
      <el-table-column label="Currency" width="120" align="center">
        <template slot-scope="{row}">
          <span>{{ row.curr }}</span>
        </template>
      </el-table-column>
      <el-table-column label="PBK" width="120" align="center">
        <template slot-scope="{row}">
          <span>{{ row.pbk }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Amount" width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.dcAmount }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Match Date" width="200" align="center">
        <template slot-scope="{row}">
          <span>{{ row.matchDate }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="Received Status"
        width="150"
        align="center"
      >
        <template slot-scope="{row}">
          <span>{{ row.recivedStatus }}</span>
        </template>
      </el-table-column>
      <el-table-column label="日期判断" width="160" align="center">
        <template slot-scope="{row}">
          <span>{{ row.day }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Block status" width="160" align="center">
        <template slot-scope="{row}">
          <span>{{ row.blockStatus }}</span>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.pageindex" :limit.sync="listQuery.pagesize" @pagination="getList" />

  </div>
</template>

<script>
import { addPayment, getPaymentInvoiceReport, getAllPaymentInvoiceReport } from '@/api/report'
import XLSX from 'xlsx'
import waves from '@/directive/waves' // waves directive
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import { export_json_to_excel } from '@/utils/Export2Excel'
import { parseTime } from '@/utils'

export default {
  name: 'PatmentInvoiceReportListTable',
  components: { Pagination },
  directives: { waves },
  data() {
    return {
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: false,
      companyCodeList: this.$store.getters.company,
      listQuery: {
        pageindex: 1,
        pagesize: 20,
        invoiceNumber: '',
        version: '',
        companyCode: '',
        blockStatus: '',
        multipleCompany: this.$store.getters.company.map(item => {
          return item.code
        }).join(',')
      },
      uploadLoading: false,
      downloading: false,
      filename: '付款发票信息',
      autoWidth: true,
      bookType: 'xlsx',
      blockStatusList: [
        {
          id: 0,
          blockStatus: 'Block'
        }, {
          id: 1,
          blockStatus: 'Payment'
        }
      ]
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      if (this.$store.getters.company && this.$store.getters.company.length > 0) {
        this.listLoading = true
        getPaymentInvoiceReport(this.listQuery).then(res => {
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
    upload(file) {
      this.uploadLoading = true
      var binary = ''
      var wb
      var outdata
      var reader = new FileReader()

      // //readAsArrayBuffer异步按字节读取文件内容，结果用ArrayBuffer对象表示,执行结束调用
      reader.onload = e => {
        var bytes = new Uint8Array(e.target.result)
        var length = bytes.byteLength

        for (var i = 0; i < length; i++) {
          binary += String.fromCharCode(bytes[i])
        }

        wb = XLSX.read(binary, {
          type: 'binary'
        })

        // //获取Sheet
        var sheet = wb.Sheets[wb.SheetNames[0]]

        // //获取sheet中所有有效的单元格
        var range = XLSX.utils.decode_range(sheet['!ref'])

        // //设置起始单元格range
        range.s.c = 1
        range.s.r = 5

        var header = ['Vendor', 'Reference', 'CoCd', 'DocumentNo', 'Type', 'NetDueDt', 'PstngDate', 'DocDate', 'Curr', 'PBk', 'DCAmount']

        outdata = XLSX.utils.sheet_to_json(sheet, {
          range: XLSX.utils.encode_range(range),
          defval: '',
          header: header,
          raw: false
        })

        if (outdata.length > 0) {
          outdata.forEach((value, index, arr) => {
            arr[index].createBy = this.$store.getters.userID
          })

          addPayment(outdata)
            .then(res => {
              this.uploadLoading = false
              if (res.success === true) {
                this.$notify({
                  title: 'Success',
                  message: '上传成功',
                  type: 'success',
                  duration: 3000
                })
                this.getList()
              } else {
                this.$notify.error({
                  title: 'Error',
                  message: res.message,
                  duration: 3000
                })
              }
            }).catch(
              this.uploadLoading = false
            )
        } else {
          this.$notify.warning({
            title: 'warning',
            message: '数据为空,请核准',
            duration: 3000
          })
          this.uploadLoading = false
        }
      }
      reader.readAsArrayBuffer(file.file)
    },
    exportExcel() {
      this.downloadLoading = true
      getAllPaymentInvoiceReport(this.listQuery)
        .then(res => {
          const tHeader = [
            'Version',
            'VendorCode',
            'VendorChName',
            'Reference',
            'CompantCode',
            'DocumentNo',
            'DocType',
            'Net Due Date',
            'Pstng Date',
            'Doc Date',
            'Currency',
            'PBk',
            'Amount',
            'Match Date',
            'Recived Status',
            '日期判断',
            'Block Status'
          ]
          const filterVal = [
            'version',
            'vendor',
            'vendorChName',
            'reference',
            'cocd',
            'documentNo',
            'type',
            'netDueDT',
            'pstngDate',
            'docDate',
            'curr',
            'pBk',
            'dCAmount',
            'matchDate',
            'recivedStatus',
            'day',
            'blockStatus'
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
          this.downloadLoading = false
        }
        ).catch(
          this.downloadLoading = false
        )
    },
    onChange(file, fileList) {
      this.$refs['upload'].clearFiles()
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
    handleFilter() {
      this.listQuery.pageindex = 1
      this.getList()
    },
    selectInit() {
      this.handleFilter()
    },
    setCellColor({ row, column, rowIndex, columnIndex }) {
      if (['VendorCode', 'VendorChName'].includes(column.label)) {
        return 'background-color:#98c3e4;'
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
