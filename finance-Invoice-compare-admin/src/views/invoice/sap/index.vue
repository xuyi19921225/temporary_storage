<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.invoiceNumber" style="width: 150px;" placeholder="发票号码" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-input v-model="listQuery.companyCode" style="width: 150px;" placeholder="公司编码" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-select
        v-model="listQuery.isMatch"
        class="filter-item"
        style="width: 150px;"
        placeholder="是否匹配"
        clearable
        @change="handleFilter"
      >
        <el-option
          v-for="item in isMatchList"
          :key="item.id"
          :label="item.match"
          :value="item.match"
        />
      </el-select>
      <el-input v-model.number="listQuery.check" style="width: 150px;" placeholder="check" class="filter-item" @keyup.enter.native="handleFilter" />
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
      <el-button class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-download">
        <a :href="`${path}SAP scan file1.xls`" download="SAP scan file1.xls">模板下载</a>
      </el-button>
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
    >
      <el-table-column label="ID" prop="id" align="center" width="80">
        <template slot-scope="{row}">
          <span>{{ row.id }}</span>
        </template>
      </el-table-column>
      <el-table-column label="VendorCode" width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.vendor }}</span>
        </template>
      </el-table-column>
      <el-table-column label="VendorChName" width="250" align="center">
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
      <el-table-column label="Currency" width="120" align="center">
        <template slot-scope="{row}">
          <span>{{ row.curr }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Amount" width="150" align="center">
        <template slot-scope="{row}">
          <span>{{ row.amountInDC }}</span>
        </template>
      </el-table-column>
      <el-table-column label="IsMatch" width="120" align="center">
        <template slot-scope="{row}">
          <span>{{ row.isMatch }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Check" width="120" align="center">
        <template slot-scope="{row}">
          <span>{{ row.check }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Remark" width="120" align="center">
        <template slot-scope="{row}">
          <span>{{ row.remark }}</span>
        </template>
      </el-table-column>
      <el-table-column label="数据导入时间" width="200" align="center">
        <template slot-scope="{row}">
          <span>{{ row.createAt }}</span>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.pageindex" :limit.sync="listQuery.pagesize" @pagination="getList" />

  </div>
</template>

<script>
import { getSAPInvoiceList, getAllSAPInvoiceList, addSAPInvoice } from '@/api/invoice'
import XLSX from 'xlsx'
import { export_json_to_excel } from '@/utils/Export2Excel'
import waves from '@/directive/waves' // waves directive
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import { parseTime } from '@/utils'

export default {
  name: 'SAPInvoiceListTable',
  components: { Pagination },
  directives: { waves },
  data() {
    return {
      path: process.env.BASE_URL,
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: {
        pageindex: 1,
        pagesize: 20,
        invoiceNumber: '',
        companyCode: '',
        isMatch: '',
        check: ''
      },
      filename: 'SAP发票信息',
      autoWidth: true,
      bookType: 'xlsx',
      downloading: false,
      uploadLoading: false,
      isMatchList: [
        {
          id: 0,
          match: 'Y'
        }, {
          id: 1,
          match: 'N'
        }
      ]

    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      this.listLoading = true
      getSAPInvoiceList(this.listQuery).then(res => {
        this.list = res.response.list
        this.total = res.response.totalCount
        this.listLoading = false
      }).catch(
        this.listLoading = false
      )
    },
    exportExcel() {
      this.downloading = true
      getAllSAPInvoiceList(this.listQuery)
        .then(res => {
          console.log(res.response)
          const tHeader = [
            'VendorCode',
            'VendorChName',
            'Reference',
            'CompanyCode',
            'DocumentNo',
            'DocType',
            'Net Due Date',
            'Pstng Date',
            'Doc Date',
            'Currency',
            'Amount',
            'IsMatch',
            'Check',
            'Remark',
            '数据导入时间'
          ]
          const filterVal = [
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
            'amountInDC',
            'isMatch',
            'check',
            'remark',
            'createAt'
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
        range.s.r = 9

        var header = ['Vendor', 'Reference', '', 'CoCd', 'DocumentNo', 'Type', 'NetDueDt', 'PstngDate', 'DocDate', 'Curr', 'AmountInDC', 'PBk', 'Text', 'BlineDate', 'AmtLC2', 'Assign', 'GL', 'ClrngDoc']

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

          addSAPInvoice(outdata)
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
            })
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
    handleCreate() {
      this.dialogType = 'add'
      this.dialogVisible = true
      Object.assign(this.dialogData, this.defaultValue())
      this.$nextTick(() => {
        this.$refs['dataForm'].resetFields()
      })
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
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
