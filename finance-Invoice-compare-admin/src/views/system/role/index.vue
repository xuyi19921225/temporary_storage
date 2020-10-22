<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.roleCode" placeholder="RoleCode" style="width: 150px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-button v-waves class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-search" @click="handleFilter">
        查询
      </el-button>
      <el-button class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-edit" @click="handleCreate">
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
      <el-table-column label="RoleCode" align="center">
        <template slot-scope="{row}">
          <span>{{ row.roleCode }}</span>
        </template>
      </el-table-column>
      <el-table-column label="RoleName" align="center">
        <template slot-scope="{row}">
          <span>{{ row.roleName }}</span>
        </template>
      </el-table-column>

      <el-table-column label="Actions" align="center" width="250px" class-name="small-padding fixed-width">
        <template slot-scope="{row,$index}">
          <el-button type="primary" size="mini" @click="handleUpdate(row)">
            编辑
          </el-button>
          <el-button type="primary" size="mini" @click="handleAuthorize(row)">
            授权菜单
          </el-button>
          <el-button v-if="row.status!='deleted'" size="mini" type="danger" @click="handleDelete(row,$index)">
            删除
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.pageindex" :limit.sync="listQuery.pagesize" @pagination="getList" />

    <el-dialog :visible.sync="dialogVisible" :title="dialogType==='edit'?'编辑角色':'新增角色'">
      <el-form ref="dataForm" :model="dialogData" label-width="100px" label-position="left" :rules="rules">
        <el-form-item label="角色编码" prop="roleCode">
          <el-input v-model="dialogData.roleCode" :disabled="dialogType==='edit'?true:false" />
        </el-form-item>
        <el-form-item label="角色名称" prop="roleName">
          <el-input v-model="dialogData.roleName" />
        </el-form-item>
      </el-form>
      <div style="text-align:right;">
        <el-button type="danger" @click="dialogVisible=false">取消</el-button>
        <el-button type="primary" @click="confirm">确认</el-button>
      </div>
    </el-dialog>

    <el-dialog :visible.sync="dialog2Visible" title="授权菜单">
      <el-form ref="menuForm" :model="dialog2Data" label-width="80px" label-position="right">
        <el-form-item label="菜单列表">
          <el-tree
            ref="tree"
            :data="menuList"
            show-checkbox
            node-key="id"
            :default-expanded-keys="[2, 3]"
            :default-checked-keys="[5]"
            :props="defaultProps"
            check-strictly="true"
          />
        </el-form-item>
      </el-form>
      <div style="text-align:right;">
        <el-button type="danger" @click="dialog2Visible=false">取消</el-button>
        <el-button type="primary" @click="confirmRoleMenu">确认</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { getRoleList, addRole, saveRole, deleteRole } from '@/api/role'
import { getTreeMenus, saveRoleMenu, getRMenuByRoleId } from '@/api/menu'
import waves from '@/directive/waves' // waves directive
import Pagination from '@/components/Pagination' // secondary package based on el-pagination

export default {
  name: 'RoleListTable',
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
        roleCode: ''
      },
      dialogVisible: false,
      dialogType: '',
      dialogData: {
        roleCode: '',
        roleName: ''
      },
      rules: {
        roleCode: [{ required: true, message: '角色Code是必填项', trigger: 'blur' }],
        roleName: [{ required: true, message: '角色名是必填项', trigger: 'blur' }]
      },
      dialog2Visible: false,
      dialog2Data: {
        userId: ''
      },
      menuList: [],
      defaultProps: {
        children: 'children',
        label: 'name'
      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      this.listLoading = true
      getRoleList(this.listQuery).then(res => {
        this.list = res.response.list
        this.total = res.response.totalCount
        this.listLoading = false
      }).catch(
        this.listLoading = false
      )
    },
    getTreeMenus(roleId) {
      getTreeMenus().then(res => {
        this.menuList = res.response
        getRMenuByRoleId({ roleId: roleId }).then(res => {
          this.$refs.tree.setCheckedKeys(res.response)
        })
      })
    },
    deleteRole(id) {
      deleteRole({ id: id }).then(res => {
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
    addRole() {
      this.dialogData.createBy = this.$store.getters.userID
      addRole(this.dialogData).then(res => {
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
    saveRole() {
      this.dialogData.updatedBy = this.$store.getters.userID
      saveRole(this.dialogData).then(res => {
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
            this.addRole()
          } else {
            this.saveRole()
          }
        }
      })
    },
    confirmRoleMenu() {
      this.dialog2Data.list = this.$refs.tree.getCheckedNodes()
      saveRoleMenu(this.dialog2Data).then(res => {
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
        this.dialogData = {
          id: row.id,
          roleCode: row.roleCode,
          roleName: row.roleName
        }
      })
    },
    handleAuthorize(row) {
      this.dialog2Visible = true
      this.dialog2Data.roleId = row.id
      this.$nextTick(() => {
        this.getTreeMenus(this.dialog2Data.roleId)
      })
    },
    handleDelete(row, index) {
      this.deleteRole(row.id)
    },
    defaultValue() {
      this.dialogData = {
        roleCode: '',
        roleName: ''
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
