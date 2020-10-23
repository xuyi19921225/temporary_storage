<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.NTID" placeholder="NTID" style="width: 150px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-button v-waves class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-search" @click="handleFilter">
        查询
      </el-button>
      <el-button v-waves class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-plus" @click="handleCreate">
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
      <el-table-column label="ID" prop="id" align="center" width="80">
        <template slot-scope="{row}">
          <span>{{ row.id }}</span>
        </template>
      </el-table-column>
      <el-table-column label="NTID" align="center">
        <template slot-scope="{row}">
          <span>{{ row.ntid }}</span>
        </template>
      </el-table-column>
      <el-table-column label="用户名" align="center">
        <template slot-scope="{row}">
          <span>{{ row.userName }}</span>
        </template>
      </el-table-column>
      <el-table-column label="邮箱" align="center" show-overflow-tooltip>
        <template slot-scope="{row}">
          <span>{{ row.email }}</span>
        </template>
      </el-table-column>
      <el-table-column label="是否启用" align="center" show-overflow-tooltip>
        <template slot-scope="scope">
          <el-switch
            v-model="scope.row.isActive"
            active-color="#13ce66"
            inactive-color="#ff4949"
            @change="changeIsActive(scope.row)"
          />
        </template>
      </el-table-column>
      <el-table-column label="Actions" align="center" width="350px" class-name="small-padding fixed-width">
        <template slot-scope="{row,$index}">
          <el-button type="primary" size="mini" icon="el-icon-edit" @click="handleUpdate(row)">
            分配角色
          </el-button>
          <el-button type="primary" size="mini" icon="el-icon-edit" @click="handleAuthorize(row)">
            授权公司
          </el-button>
          <el-button v-if="row.status!='deleted'" size="mini" icon="el-icon-delete" type="danger" @click="handleDelete(row,$index)">
            删除
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.pageindex" :limit.sync="listQuery.pagesize" @pagination="getList" />

    <el-dialog :visible.sync="dialogVisible" :title="dialogType==='edit'?'编辑用户':'新增用户'">
      <el-form ref="dataForm" :model="dialogData" label-width="80px" label-position="left" :rules="rules">
        <el-form-item label="NTID" prop="ntid">
          <el-input v-model="dialogData.ntid" :disabled="dialogType==='edit'?true:false" />
        </el-form-item>
        <el-form-item label="用户名" prop="userName">
          <el-input v-model="dialogData.userName" />
        </el-form-item>
        <el-form-item label="邮箱" prop="email">
          <el-input v-model="dialogData.email" />
        </el-form-item>
        <el-form-item v-if="dialogType==='edit'" label="角色" prop="rid">
          <el-select
            v-model="dialogData.rid"
            class="filter-item"
            placeholder="请选择角色"
            style="width:100%"
          >
            <el-option
              v-for="item in roleList"
              :key="item.id"
              :label="item.roleName"
              :value="item.id"
            />
          </el-select>
        </el-form-item>
        <el-form-item label="是否启用" prop="isActive">
          <el-switch
            v-model="dialogData.isActive"
            active-color="#13ce66"
            inactive-color="#ff4949"
          />
        </el-form-item>
      </el-form>
      <div style="text-align:right;">
        <el-button type="danger" @click="dialogVisible=false">取消</el-button>
        <el-button type="primary" @click="confirm">确认</el-button>
      </div>
    </el-dialog>

    <el-dialog :visible.sync="dialog2Visible" title="授权公司">
      <el-form ref="companyForm" :model="dialog2Data" label-width="80px" label-position="right">
        <el-form-item label="公司列表">
          <el-tree
            ref="tree"
            :data="companyList"
            show-checkbox
            node-key="id"
            :default-expanded-keys="[2, 3]"
            :default-checked-keys="[5]"
            :props="defaultProps"
          />
        </el-form-item>
      </el-form>
      <div style="text-align:right;">
        <el-button type="danger" @click="dialog2Visible=false">取消</el-button>
        <el-button type="primary" @click="confirmCompany">确认</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { getUserList, addUser, saveUser, deleteUser } from '@/api/user'
import { getAllRoleList } from '@/api/role'
import { getAllCompanyList, saveUCompany, getUCompanyByUid } from '@/api/company'
import waves from '@/directive/waves' // waves directive
import Pagination from '@/components/Pagination' // secondary package based on el-pagination

export default {
  name: 'UserListTable',
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
        NTID: ''
      },
      roleList: [],
      dialogVisible: false,
      dialogType: '',
      dialogData: {
        ntid: '',
        userName: '',
        email: '',
        isActive: true,
        rid: ''
      },
      rules: {
        ntid: [{ required: true, message: '工号是必填项', trigger: 'blur' }],
        userName: [{ required: true, message: '用户名是必填项', trigger: 'blur' }],
        email: [
          {
            required: true,
            type: 'email',
            message: '请填写正确的邮箱',
            trigger: ['blur']
          }
        ]
      },
      companyList: [],
      dialog2Visible: false,
      dialog2Data: {
        userId: ''
      },
      defaultProps: {
        children: 'children',
        label: 'companyName'
      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      this.listLoading = true
      getUserList(this.listQuery).then(res => {
        this.list = res.response.list
        this.total = res.response.totalCount
        this.listLoading = false
      }).catch(
        this.listLoading = false
      )
    },
    getAllRoleList() {
      getAllRoleList().then(res => {
        this.roleList = res.response
      })
    },
    getAllCompanyList(userId) {
      this.$refs.tree.setCheckedKeys([])
      getAllCompanyList().then(res => {
        this.companyList = res.response
        getUCompanyByUid({ userId: userId }).then(res => {
          this.$refs.tree.setCheckedKeys(res.response)
        })
      })
    },
    deleteUser(id) {
      deleteUser({ id: id }).then(res => {
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
    addUser() {
      this.dialogData.createBy = this.$store.getters.userID
      this.dialogData.rid = this.dialogData.rid > 0 ? this.dialogData.rid : 0
      addUser(this.dialogData).then(res => {
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
    saveUser(data) {
      this.dialogData.updatedBy = this.$store.getters.userID
      saveUser(data || this.dialogData).then(res => {
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
    changeIsActive(row) {
      this.saveUser(row)
    },
    confirm() {
      this.$refs['dataForm'].validate(valid => {
        if (valid) {
          if (this.dialogType === 'add') {
            this.addUser()
          } else {
            this.saveUser()
          }
        }
      })
    },
    confirmCompany() {
      this.dialog2Data.list = this.$refs.tree.getCheckedNodes()
      console.log(this.$refs.tree.getCheckedNodes())
      saveUCompany(this.dialog2Data).then(res => {
        if (res.success === true) {
          this.$message({
            message: '保存成功',
            type: 'success'
          })
          this.getList()
          this.dialog2Visible = false
        } else {
          this.$message.error(res.message)
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
        this.getAllRoleList()
        this.dialogData = {
          ntid: row.ntid,
          userName: row.ntid,
          email: row.email,
          isActive: row.isActive,
          id: row.id,
          rid: row.roleID
        }
      })
    },
    handleAuthorize(row) {
      this.dialog2Visible = true
      this.dialog2Data.userId = row.id
      this.$nextTick(() => {
        this.getAllCompanyList(row.id)
      })
    },
    handleDelete(row, index) {
      this.deleteUser(row.id)
    },
    defaultValue() {
      this.dialogData = {
        ntid: '',
        userName: '',
        email: '',
        isActive: true,
        rid: ''
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
