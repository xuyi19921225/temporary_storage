<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.invoiceNumber" style="width: 150px;" placeholder="发票号码" class="filter-item" @keyup.enter.native="handleFilter" />
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
    </div>

    <el-table
      :key="tableKey"
      v-loading="listLoading"
      :data="list"
      :header-cell-style="{background:'#eef1f6',color:'#606266'}"
      fit
      highlight-current-row
    >
      <el-table-column label="发票号码" width="160" align="center" fixed>
        <template slot-scope="{row}">
          <span>{{ row.invoiceNumber }}</span>
        </template>
      </el-table-column>
      <el-table-column label="厂别" align="center">
        <template slot-scope="{row}">
          <span>{{ row.companyCode }}</span>
        </template>
      </el-table-column>
      <el-table-column label="发票金额" align="center">
        <template slot-scope="{row}">
          <span>{{ row.amount }}</span>
        </template>
      </el-table-column>
      <el-table-column label="是否一致" align="center">
        <template slot-scope="{row}">
          <span>{{ row.isMatch }}</span>
        </template>
      </el-table-column>
      <el-table-column label="VendorCode" width="100" align="center">
        <template slot-scope="{row}">
          <span>{{ row.vendor }}</span>
        </template>
      </el-table-column>
      <el-table-column label="VendorChName" width="120" align="center">
        <template slot-scope="{row}">
          <span>{{ row.vendorChName }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Reference" align="center">
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
      <el-table-column label="DocType" align="center">
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
      <el-table-column label="PBK" align="center">
        <template slot-scope="{row}">
          <span>{{ row.pbk }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Text" width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.text }}</span>
        </template>
      </el-table-column>
      <el-table-column label="PBK" align="center">
        <template slot-scope="{row}">
          <span>{{ row.pbk }}</span>
        </template>
      </el-table-column>
      <el-table-column label="BlineDate" width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.blineDate }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Amt. LC2" align="center">
        <template slot-scope="{row}">
          <span>{{ row.amtLC2 }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Assignment" width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.assign }}</span>
        </template>
      </el-table-column>
      <el-table-column label="G/L" align="center">
        <template slot-scope="{row}">
          <span>{{ row.gL }}</span>
        </template>
      </el-table-column>
      <el-table-column label="ClrngDoc" align="center">
        <template slot-scope="{row}">
          <span>{{ row.clrngDoc }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Check" align="center" fixed>
        <template slot-scope="{row}">
          <span>{{ row.check }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Data Source" width="150" align="center" fixed>
        <template slot-scope="{row}">
          <span>{{ row.dataSource }}</span>
        </template>
      </el-table-column>
      <el-table-column label="MatchDate" width="160" align="center" fixed>
        <template slot-scope="{row}">
          <span>{{ row.matchDate }}</span>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.pageindex" :limit.sync="listQuery.pagesize" @pagination="getList" />

  </div>
</template>

<script>
import { addPayment, getCompareMatchInvoiceReport } from '@/api/report'
import XLSX from 'xlsx'
import waves from '@/directive/waves' // waves directive
import Pagination from '@/components/Pagination' // secondary package based on el-pagination

export default {
  name: 'PatmentInvoiceReportListTable',
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
      console.log(this.$store.getters.company)
      this.listQuery.list = this.$store.getters.company
      getCompareMatchInvoiceReport(this.listQuery).then(res => {
        this.list = res.response.list
        this.total = res.response.totalCount
        this.listLoading = false
      }).catch(
        this.listLoading = false
      )
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
    onChange(file, fileList) {
      this.$refs['upload'].clearFiles()
    },
    handleFilter() {
      this.listQuery.pageindex = 1
      this.getList()
    },
    selectInit() {
      this.handleFilter()
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
