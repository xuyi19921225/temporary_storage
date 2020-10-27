<template>
  <div class="app-container">
    <div class="filter-container">
      <el-select
        v-model="listQuery.companyCode"
        class="filter-item"
        placeholder="请选择公司编码"
        style="width: 150px;"
        @change="changeCompanyCode"
      >
        <el-option
          v-for="item in companyCodeList"
          :key="item.Id"
          :label="item.code"
          :value="item.code"
        />
      </el-select>
      <el-input v-model="listQuery.invoiceNumber" style="width: 150px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-button v-waves class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-search" @click="handleFilter">
        查询
      </el-button>
      <el-button class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-plus" @click="handleCreate">
        新增
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
        <el-button v-waves type="primary" :loading="uploadLoading" icon="el-icon-upload">批量导入</el-button>
      </el-upload>
      <el-button class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-download">
        <a :href="`${path}SAP scan_template.xlsx`" download="SAP scan_template.xlsx">模板下载</a>
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
        <el-form-item label="InvoiceDate" prop="invoiceDate">
          <el-date-picker
            v-model="dialogData.invoiceDate"
            align="right"
            type="date"
            placeholder="选择日期"
            :picker-options="pickerOptions"
            style="width:100%"
          />
        </el-form-item>
        <el-form-item label="Amount" prop="amount">
          <el-input v-model.number="dialogData.amount" />
        </el-form-item>
      </el-form>
      <div style="text-align:right;">
        <el-button type="danger" @click="dialogVisible=false">取消</el-button>
        <el-button type="primary" @click="confirm">确认</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { getSiteInvoiceList, addSiteInvoice, saveInvoice } from '@/api/invoice'
import XLSX from 'xlsx'
import waves from '@/directive/waves' // waves directive
import Pagination from '@/components/Pagination' // secondary package based on el-pagination

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
        companyCode: ''
      },
      companyCodeList: this.$store.getters.company,
      uploadLoading: false,
      dialogVisible: false,
      dialogType: '',
      dialogData: {
        invoiceNumber: '',
        invoiceDate: '',
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
      }
    }
  },
  // computed: {
  //   'dialogData.companyCode': {
  //     set: function() {
  //       this.dialogData.companyCode = this.listQuery.companyCode
  //     }
  //   }
  // },
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
          var sheet = wb.Sheets[wb.SheetNames[0]]

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
      }
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
    handleFilter() {
      this.listQuery.pageindex = 1
      this.getList()
    },
    handleCreate() {
      console.log(this.listQuery)
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
          invoiceDate: row.invoiceDate,
          amount: row.amount
        }
      })
    },
    defaultValue() {
      this.dialogData = {
        invoiceNumber: '',
        invoiceDate: '',
        amount: 0
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
