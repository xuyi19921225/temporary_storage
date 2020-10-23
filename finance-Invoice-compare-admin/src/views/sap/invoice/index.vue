<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.value" style="width: 150px;" class="filter-item" @keyup.enter.native="handleFilter" />
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
        :http-request="uploadVendor"
        :on-change="onChange"
        accept=".xlsx,application/vnd.ms-excel"
        :show-file-list="false"
      >
        <el-button type="primary" :loading="uploadLoading" icon="el-icon-upload">批量导入</el-button>
      </el-upload>
      <el-button class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-download">
        <a :href="`${path}vendor_template.xlsx`" download="vendor_template.xlsx">模板下载</a>
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
          <span>{{ row.vendorCode }}</span>
        </template>
      </el-table-column>
      <el-table-column label="VendorChName" align="center">
        <template slot-scope="{row}">
          <span>{{ row.vendorChName }}</span>
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

    <el-dialog :visible.sync="dialogVisible" :title="dialogType==='edit'?'编辑供应商':'新增供应商'">
      <el-form ref="dataForm" :model="dialogData" label-width="140px" label-position="left" :rules="rules">
        <el-form-item label="VendorCode" prop="vendorCode">
          <el-input v-model="dialogData.vendorCode" :disabled="dialogType==='edit'?true:false" />
        </el-form-item>
        <el-form-item label="VendorChName" prop="vendorChName">
          <el-input v-model="dialogData.vendorChName" />
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
import { addVendor, saveVendor, getVendorList } from '@/api/vendor'
import XLSX from 'xlsx'
import waves from '@/directive/waves' // waves directive
import Pagination from '@/components/Pagination' // secondary package based on el-pagination

export default {
  name: 'MenuListTable',
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
        value: ''
      },
      uploadLoading: false,
      dialogVisible: false,
      dialogType: '',
      dialogData: {
        vendorCode: '',
        vendorChName: ''
      },
      rules: {
        vendorCode: [{ required: true, message: '供应商编码是必填项', trigger: 'blur' }],
        vendorChName: [{ required: true, message: '供应商名称是必填项', trigger: 'blur' }]
      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      this.listLoading = true
      getVendorList(this.listQuery).then(res => {
        this.list = res.response.list
        this.total = res.response.totalCount
        this.listLoading = false
      }).catch(
        this.listLoading = false
      )
    },
    uploadVendor(file) {
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

        outdata = XLSX.utils.sheet_to_json(wb.Sheets[wb.SheetNames[0]], {
          range: 'A9',
          defval: ''
        })

        if (outdata.length > 0) {
          const vendors = []
          outdata.forEach((value, index, arr) => {
            vendors.push({
              vendorCode: value.VendorCode,
              vendorChName: value.VendorChName,
              createBy: this.$store.getters.userID
            })
          })
          addVendor(vendors)
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
    addVendor() {
      this.dialogData.createBy = this.$store.getters.userID
      addVendor([this.dialogData]).then(res => {
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
    saveVendor() {
      this.dialogData.updatedBy = this.$store.getters.userID
      saveVendor(this.dialogData).then(res => {
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
    confirm() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          if (this.dialogType === 'add') {
            this.addVendor()
          } else {
            this.saveVendor()
          }
        }
      })
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
    handleUpdate(row) {
      this.dialogType = 'edit'
      this.dialogVisible = true
      Object.assign(this.dialogData, this.defaultValue())
      this.$nextTick(() => {
        this.$refs['dataForm'].resetFields()
        this.dialogData = {
          id: row.id,
          vendorCode: row.vendorCode,
          vendorChName: row.vendorChName
        }
      })
    },
    defaultValue() {
      this.dialogData = {
        vendorCode: '',
        vendorChName: ''
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
