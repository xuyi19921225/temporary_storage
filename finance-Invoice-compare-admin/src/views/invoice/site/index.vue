<template>
  <div class="app-container">
    <div class="filter-container">
      <el-button v-waves class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter">
        查询
      </el-button>
      <el-button class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-plus" @click="handleCreate">
        单个导入
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
        <el-button v-waves type="primary" :loading="uploadLoading" icon="el-icon-upload">扫描数据导入</el-button>
      </el-upload>
      <el-upload
        ref="uploadManual"
        class="upload-demo filter-item"
        style="margin-left: 10px;"
        action
        :http-request="uploadManual"
        :on-change="onChangeManual"
        accept=".xlsx,application/vnd.ms-excel"
        :show-file-list="false"
      >
        <el-button v-waves type="primary" :loading="uploadManualLoading" icon="el-icon-upload">手工数据导入</el-button>
      </el-upload>
      <el-button class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-download">
        <a :href="`${path}Hardcopy record.xlsx`" download="Hardcopy record.xlsx">模板下载</a>
      </el-button>
      <el-button v-waves class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-download" :loading="downloading" @click="exportExcel">
        导出EXCEL
      </el-button>
    </div>
    <div class="filter-container">
      <el-form :model="listQuery" :inline="true" class="demo-form-inline">
        <el-form-item label="公司编码">
          <el-select
            v-model="listQuery.companyCode"
            placeholder="--请选择--"
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
        </el-form-item>
        <el-form-item label="发票号码">
          <el-input v-model="listQuery.invoiceNumber" @keyup.enter.native="handleFilter" />
        </el-form-item>
        <el-form-item label="发票日期">
          <el-date-picker
            v-model="listQuery.invoiceBeginDate"
            type="date"
            placeholder="开始日期"
            :picker-options="beginDate"
            value-format="yyyy-MM-dd"
          /><span style="padding-left:5px">~</span>
          <el-date-picker
            v-model="listQuery.invoiceEndDate"
            type="date"
            placeholder="结束日期"
            :picker-options="endDate"
            value-format="yyyy-MM-dd"
          />
        </el-form-item>
      </el-form>
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
      <el-table-column label="CompanyCode" align="center">
        <template slot-scope="{row}">
          <span>{{ row.companyCode }}</span>
        </template>
      </el-table-column>
      <el-table-column label="InvoiceNumber" align="center">
        <template slot-scope="{row}">
          <span>{{ row.invoiceNumber }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Coupa ID" align="center">
        <template slot-scope="{row}">
          <span>{{ row.coupaID }}</span>
        </template>
      </el-table-column>
      <el-table-column label="InvoiceDate" align="center">
        <template slot-scope="{row}">
          <span>{{ row.invoiceDate }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Amount" align="center">
        <template slot-scope="{row}">
          <span>{{ row.amount }}</span>
        </template>
      </el-table-column>
      <el-table-column label="数据导入时间" width="200" align="center">
        <template slot-scope="{row}">
          <span>{{ row.createAt }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Actions" align="center" width="150px" class-name="small-padding fixed-width">
        <template slot-scope="{row}">
          <el-button type="primary" size="mini" icon="el-icon-edit" @click="handleUpdate(row)">
            编辑
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.pageindex" :limit.sync="listQuery.pagesize" @pagination="getList" />

    <el-dialog :visible.sync="dialogVisible" :title="dialogType==='edit'?'编辑发票':'新增发票'">
      <el-form ref="dataForm" :model="dialogData" label-width="140px" label-position="left" :rules="rules">
        <el-form-item label="InvoiceNumber" prop="invoiceNumber">
          <el-input v-model="dialogData.invoiceNumber" :disabled="dialogType==='edit'?true:false" />
        </el-form-item>
        <el-form-item label="Coupa ID" prop="invoiceNumber">
          <el-input v-model="dialogData.coupaID" />
        </el-form-item>
        <el-form-item label="InvoiceDate" prop="invoiceDate">
          <el-date-picker
            v-model="dialogData.invoiceDate"
            align="right"
            type="date"
            placeholder="选择日期"
            :picker-options="pickerOptions"
            style="width:100%"
            value-format="yyyy-MM-dd"
          />
        </el-form-item>
        <el-form-item label="Amount" prop="amount">
          <!-- <el-input
            v-model.number="dialogData.amount"
            @input="(val) => {
              dialogData.amount = val
                .replace(/[^0-9.]/g, '')
            }"
          /> -->
          <el-input-number v-model="dialogData.amount" style="width:100%;" controls-position="right" :precision="2" :step="0.1" />
        </el-form-item>
      </el-form>
      <div style="text-align:right;">
        <el-button type="danger" @click="dialogVisible=false">取消</el-button>
        <el-button type="primary" @click="confirm">确认</el-button>
      </div>
    </el-dialog>

    <el-dialog :visible.sync="existsInvoiceVisible" title="重复发票信息">
      <el-table
        :key="tableKey"
        v-loading="existsInvoiceLoading"
        :data="existsInvoiceList"
        :header-cell-style="{background:'#eef1f6',color:'#606266'}"
        fit
        highlight-current-row
      >
        <el-table-column label="ID" prop="id" align="center" width="80">
          <template slot-scope="{row}">
            <span>{{ row.id }}</span>
          </template>
        </el-table-column>
        <el-table-column label="CompanyCode" align="center">
          <template slot-scope="{row}">
            <span>{{ row.companyCode }}</span>
          </template>
        </el-table-column>
        <el-table-column label="InvoiceNumber" align="center">
          <template slot-scope="{row}">
            <span>{{ row.invoiceNumber }}</span>
          </template>
        </el-table-column>
        <el-table-column label="InvoiceDate" align="center">
          <template slot-scope="{row}">
            <span>{{ row.invoiceDate }}</span>
          </template>
        </el-table-column>
        <el-table-column label="Amount" align="center">
          <template slot-scope="{row}">
            <span>{{ row.amount }}</span>
          </template>
        </el-table-column>
      </el-table>

      <pagination v-show="existsInvoiceTotal>0" :total="existsInvoiceTotal" :page.sync="existsInvoiceQuery.pageindex" :limit.sync="existsInvoiceQuery.pagesize" @pagination="getExistsInvoiceList" />
    </el-dialog>
  </div>
</template>

<script>
import { getSiteInvoiceList, getAllSiteInvoiceList, addSiteInvoice, saveInvoice } from '@/api/invoice'
import XLSX from 'xlsx'
import { export_json_to_excel } from '@/utils/Export2Excel'
import waves from '@/directive/waves' // waves directive
import Pagination from '@/components/Pagination' // secondary package based on el-pagination
import { parseTime } from '@/utils'

export default {
  name: 'SiteInvoiceListTable',
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
        invoiceBeginDate: '',
        invoiceEndDate: ''
      },
      companyCodeList: this.$store.getters.company,
      filename: 'Site发票信息',
      autoWidth: true,
      bookType: 'xlsx',
      downloading: false,
      uploadLoading: false,
      uploadManualLoading: false,
      dialogVisible: false,
      dialogType: '',
      dialogData: {
        invoiceNumber: '',
        invoiceDate: '',
        coupaID: '',
        amount: 0
      },
      rules: {
        invoiceNumber: [{ required: true, message: '发票号是必填项', trigger: 'blur' }],
        invoiceDate: [{ required: true, message: '发票日期是必填项', trigger: 'blur' }],
        amount: [{ required: true, message: '金额是必填项', trigger: 'blur' }]
      },
      pickerOptions: {
        disabledDate(time) {
          return time.getTime() > Date.now()
        },
        shortcuts: [{
          text: '今天',
          onClick(picker) {
            picker.$emit('pick', new Date())
          }
        }, {
          text: '昨天',
          onClick(picker) {
            const date = new Date()
            date.setTime(date.getTime() - 3600 * 1000 * 24)
            picker.$emit('pick', date)
          }
        }, {
          text: '一周前',
          onClick(picker) {
            const date = new Date()
            date.setTime(date.getTime() - 3600 * 1000 * 24 * 7)
            picker.$emit('pick', date)
          }
        }]
      },
      beginDate: {
        disabledDate: time => {
          if (this.listQuery.invoiceEndDate) {
            return time.getTime() > Date.parse(this.listQuery.invoiceEndDate)
          }
        }
      },
      endDate: {
        disabledDate: time => {
          if (this.listQuery.invoiceBeginDate) {
            return time.getTime() < Date.parse(this.listQuery.invoiceBeginDate)
          }
        }
      },
      existsInvoiceVisible: false,
      existsInvoiceList: null,
      existsInvoiceTotal: 0,
      existsInvoiceLoading: false,
      existsInvoiceQuery: {
        pageindex: 1,
        pagesize: 10
      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      this.listLoading = true
      getSiteInvoiceList(this.listQuery).then(res => {
        this.list = res.response.list
        this.total = res.response.totalCount
        this.listLoading = false
      }).catch(
        this.listLoading = false
      )
    },
    getExistsInvoiceList() {
      var list = JSON.parse(sessionStorage.getItem('existsInvoiceList'))
      this.existsInvoiceLoading = true
      this.existsInvoiceList = list.slice((this.existsInvoiceQuery.pageindex - 1) * this.existsInvoiceQuery.pagesize, this.existsInvoiceQuery.pageindex * this.existsInvoiceQuery.pagesize - 1)
      this.existsInvoiceTotal = list.length
      this.existsInvoiceLoading = false
    },
    upload(file) {
      if (this.listQuery.companyCode === '') {
        this.$message.warning({
          message: '请先选择公司编码，再进行上传或者新增操作'
        })
      } else {
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
          var sheet = wb.Sheets[this.listQuery.companyCode]

          // //获取sheet中所有有效的单元格
          // var range = XLSX.utils.decode_range(sheet['!ref'])

          // // //设置起始单元格range
          // range.s.c = 1
          // range.s.r = 9

          var header = ['ScanData']

          outdata = XLSX.utils.sheet_to_json(sheet, {
          // range: XLSX.utils.encode_range(range),
            defval: '',
            header: header,
            raw: false
          })

          if (outdata.length > 0) {
          // 检查数据是否有重复
            try {
              outdata.forEach((item, i, arr) => {
                const flag = outdata.some((e, j, arr) => {
                  if (i !== j && item === e) {
                    return true
                  }
                })

                if (flag) {
                  throw new Error('数据重复，请检查')
                }
              })
            } catch (e) {
              if (e.message !== '') {
                this.uploadLoading = false
                this.$notify.error({
                  title: 'Error',
                  message: e.message,
                  duration: 3000
                })
              }
              return
            }

            var invoiceList = []
            try {
              outdata.forEach((value, index, arr) => {
                var textDate = value.ScanData.split(',')[5]
                invoiceList.push({
                  companyCode: this.listQuery.companyCode,
                  invoiceNumber: value.ScanData.split(',')[3],
                  invoiceDate: `${textDate.substr(0, 4)}-${textDate.substr(4, 2)}-${textDate.substr(6, 2)}`,
                  amount: value.ScanData.split(',')[4],
                  dataSource: 'Scan',
                  createBy: this.$store.getters.userID
                })
              })
            } catch (e) {
              this.uploadLoading = false
              this.$notify.error({
                title: 'Error',
                message: '上传的数据格式不正确，请检查',
                duration: 3000
              })
              return
            }

            addSiteInvoice(invoiceList)
              .then(res => {
                this.uploadLoading = false
                if (res.success === true) {
                  this.$notify({
                    title: 'Success',
                    message: `上传成功,共导入${invoiceList.length}条数据`,
                    type: 'success',
                    duration: 3000
                  })
                  this.getList()
                } else {
                  this.existsInvoiceVisible = true
                  sessionStorage.setItem('existsInvoiceList', JSON.stringify(res.response))
                  this.getExistsInvoiceList()
                }
              }).catch(() => {
                this.uploadLoading = false
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
      }
    },
    uploadManual(file) {
      if (this.listQuery.companyCode === '') {
        this.$message.warning({
          message: '请先选择公司编码，再进行上传或者新增操作'
        })
      } else {
        this.uploadManualLoading = true
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
          var sheet = wb.Sheets[this.listQuery.companyCode]

          // //获取sheet中所有有效的单元格
          // var range = XLSX.utils.decode_range(sheet['!ref'])

          // // //设置起始单元格range
          // range.s.c = 1
          // range.s.r = 9

          outdata = XLSX.utils.sheet_to_json(sheet, {
          // range: XLSX.utils.encode_range(range),
            defval: '',
            raw: false
          })

          if (outdata.length > 0) {
          // 检查数据是否有重复
            try {
              outdata.forEach((item, i, arr) => {
                const flag = outdata.some((e, j, arr) => {
                  // eslint-disable-next-line no-eval
                  if (i !== j && eval('item["发票号码"]') === eval('e["发票号码"]')) {
                    return true
                  }
                })

                if (flag) {
                  throw new Error('数据重复，请检查')
                }
              })
            } catch (e) {
              if (e.message !== '') {
                this.uploadManualLoading = false
                this.$notify.error({
                  title: 'Error',
                  message: e.message,
                  duration: 3000
                })
              }
              return
            }

            var invoiceList = []
            try {
              outdata.forEach((value, index, arr) => {
                invoiceList.push({
                  companyCode: this.listQuery.companyCode,
                  // eslint-disable-next-line no-eval
                  invoiceNumber: eval('value["发票号码"]'),
                  // eslint-disable-next-line no-eval
                  coupaID: eval('value["Coupa ID"]'),
                  // eslint-disable-next-line no-eval
                  invoiceDate: eval('value["发票日期"]'),
                  // eslint-disable-next-line no-eval
                  amount: eval('value["金额"]'),
                  dataSource: 'Manual',
                  createBy: this.$store.getters.userID
                })
              })
            } catch (e) {
              this.uploadManualLoading = false
              this.$notify.error({
                title: 'Error',
                message: '上传的数据格式不正确，请检查',
                duration: 3000
              })
              return
            }

            addSiteInvoice(invoiceList)
              .then(res => {
                this.uploadManualLoading = false
                if (res.success === true) {
                  this.$notify({
                    title: 'Success',
                    message: `上传成功,共导入${invoiceList.length}条数据`,
                    type: 'success',
                    duration: 3000
                  })
                  this.getList()
                } else {
                  this.existsInvoiceVisible = true
                  sessionStorage.setItem('existsInvoiceList', JSON.stringify(res.response))
                  this.getExistsInvoiceList()
                }
              }).catch(() => {
                this.uploadLoading = false
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
      }
    },
    exportExcel() {
      this.downloading = true
      getAllSiteInvoiceList(this.listQuery)
        .then(res => {
          console.log(res.response)
          const tHeader = [
            'CompanyCode',
            'InvoiceNumber',
            'CoupaID',
            'InvoiceDate',
            'Amount',
            '数据导入时间'
          ]
          const filterVal = [
            'companyCode',
            'invoiceNumber',
            'coupaID',
            'invoiceDate',
            'amount',
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
    changeCompanyCode(value) {
      this.listQuery.companyCode = value
    },
    confirm() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          if (this.dialogType === 'add') {
            this.addSiteInvoice()
          } else {
            this.saveInvoice()
          }
        }
      })
    },
    addSiteInvoice() {
      this.dialogData.createBy = this.$store.getters.userID
      this.dialogData.dataSource = 'Manual'
      addSiteInvoice([this.dialogData]).then(res => {
        if (res.success === true) {
          this.$message({
            message: '添加成功',
            type: 'success'
          })
          this.getList()
          this.dialogVisible = false
        } else {
          this.$message.error(res.message)
        }
      })
    },
    saveInvoice() {
      this.dialogData.updatedBy = this.$store.getters.userID
      this.dialogData.dataSource = 'Manual'
      saveInvoice(this.dialogData).then(res => {
        if (res.success === true) {
          this.$message({
            message: '更新成功',
            type: 'success'
          })
          this.getList()
          this.dialogVisible = false
        } else {
          this.$message.error(res.message)
        }
      })
    },
    onChange(file, fileList) {
      this.$refs['upload'].clearFiles()
    },
    onChangeManual(file, fileList) {
      this.$refs['uploadManual'].clearFiles()
    },
    handleFilter() {
      this.listQuery.pageindex = 1
      this.getList()
    },
    handleCreate() {
      if (this.listQuery.companyCode === '') {
        this.$message.warning({
          message: '请先选择公司编码，再进行上传或者新增操作'
        })
      } else {
        this.dialogType = 'add'
        this.dialogVisible = true
        Object.assign(this.dialogData, this.defaultValue())
        this.dialogData.companyCode = this.listQuery.companyCode
        this.$nextTick(() => {
          this.$refs['dataForm'].resetFields()
        })
      }
    },
    handleUpdate(row) {
      this.dialogType = 'edit'
      this.dialogVisible = true
      Object.assign(this.dialogData, this.defaultValue())
      this.$nextTick(() => {
        this.$refs['dataForm'].resetFields()
        this.dialogData = {
          companyCode: row.companyCode,
          id: row.id,
          invoiceNumber: row.invoiceNumber,
          coupaID: row.coupaID,
          invoiceDate: row.invoiceDate,
          amount: row.amount
        }
      })
    },
    defaultValue() {
      this.dialogData = {
        invoiceNumber: '',
        invoiceDate: '',
        coupaID: '',
        amount: 0
      }
    }
  }
}
</script>

<style lang="scss" >
.el-input-number.is-controls-right .el-input__inner{
  text-align: left;
}
</style>
