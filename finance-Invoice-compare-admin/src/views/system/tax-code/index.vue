<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input
        v-model="listQuery.code"
        style="width: 150px;"
        class="filter-item"
        placeholder="税码"
        @keyup.enter.native="handleFilter"
      />
      <el-button
        v-waves
        class="filter-item"
        style="margin-left: 10px;"
        type="primary"
        icon="el-icon-search"
        @click="handleFilter"
      >
        查询
      </el-button>
      <el-button
        class="filter-item"
        style="margin-left: 10px;"
        type="primary"
        icon="el-icon-plus"
        @click="handleCreate"
      >
        新增
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
      <el-table-column
        label="ID"
        prop="id"
        align="center"
        width="80"
      >
        <template slot-scope="{row}">
          <span>{{ row.id }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="税码"
        align="center"
      >
        <template slot-scope="{row}">
          <span>{{ row.code }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="税率"
        align="center"
      >
        <template slot-scope="{row}">
          <span>{{ row.rate }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="备注"
        align="center"
      >
        <template slot-scope="{row}">
          <span>{{ row.remark }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="Actions"
        align="center"
        width="250px"
        class-name="small-padding fixed-width"
      >
        <template slot-scope="{row}">
          <el-button
            type="primary"
            size="mini"
            icon="el-icon-edit"
            @click="handleUpdate(row)"
          >
            编辑
          </el-button>
          <el-button
            v-if="row.status!='deleted'"
            size="mini"
            icon="el-icon-delete"
            type="danger"
            @click="handleDelete(row)"
          >
            删除
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination
      v-show="total>0"
      :total="total"
      :page.sync="listQuery.pageindex"
      :limit.sync="listQuery.pagesize"
      @pagination="getList"
    />

    <el-dialog
      :visible.sync="dialogVisible"
      :title="dialogType==='edit'?'编辑税码':'新增税码'"
    >
      <el-form
        ref="dataForm"
        :model="dialogData"
        label-width="140px"
        label-position="left"
        :rules="rules"
      >
        <el-form-item
          label="税码"
          prop="code"
        >
          <el-input
            v-model="dialogData.code"
            :disabled="dialogType==='edit'?true:false"
          />
        </el-form-item>
        <el-form-item
          label="税率"
          prop="rate"
        >
          <!-- <el-input
            v-model="dialogData.rate"
            :disabled="dialogType==='edit'?true:false"
          /> -->

          <el-input-number v-model="dialogData.rate" style="width:100%;" controls-position="right" :precision="2" :step="0.1" />
        </el-form-item>
        <el-form-item
          label="备注"
          prop="remark"
        >
          <el-input v-model="dialogData.remark" />
        </el-form-item>
      </el-form>
      <div style="text-align:right;">
        <el-button
          type="danger"
          @click="dialogVisible=false"
        >取消</el-button>
        <el-button
          type="primary"
          @click="confirm"
        >确认</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { getTaxCodeList, addTaxCode, saveTaxCode, deleteTaxCode } from '@/api/taxcode'
import waves from '@/directive/waves' // waves directive
import Pagination from '@/components/Pagination' // secondary package based on el-pagination

export default {
  name: 'TaxCodeListTable',
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
        code: ''
      },
      dialogVisible: false,
      dialogType: '',
      dialogData: {
        code: '',
        rate: '',
        remark: ''
      },
      rules: {
        code: [{ required: true, message: '税码是必填项', trigger: 'blur' }],
        rate: [{ required: true, message: '税率是必填项', trigger: 'blur' }]
      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      this.listLoading = true
      getTaxCodeList(this.listQuery).then(res => {
        this.list = res.response.list
        this.total = res.response.totalCount
        this.listLoading = false
      }).catch(
        this.listLoading = false
      )
    },
    addVendor() {
      this.dialogData.createBy = this.$store.getters.userID
      addTaxCode(this.dialogData).then(res => {
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
      saveTaxCode(this.dialogData).then(res => {
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
    deleteVendor(id) {
      deleteTaxCode({ id: id }).then(res => {
        if (res.success === true) {
          this.$notify({
            title: 'Success',
            message: '删除成功',
            type: 'success',
            duration: 2000
          })
          this.getList()
        } else {
          this.$notify.error({
            title: 'Error',
            message: '删除失败',
            duration: 2000
          })
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
          code: row.code,
          rate: row.rate,
          remark: row.remark
        }
      })
    },
    handleDelete(row, index) {
      this.deleteVendor(row.id)
    },
    defaultValue() {
      this.dialogData = {
        code: '',
        rate: '',
        remark: ''
      }
    }
  }
}
</script>

<style lang="scss" scoped>
// .el-input-number.is-controls-right .el-input__inner{
//   text-align: left;
// }
</style>
