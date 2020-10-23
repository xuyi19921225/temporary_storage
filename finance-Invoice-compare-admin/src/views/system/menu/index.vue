<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.name" placeholder="name" style="width: 150px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-button v-waves class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-search" @click="handleFilter">
        查询
      </el-button>
      <el-button class="filter-item" style="margin-left: 10px;" type="primary" icon="el-icon-plus" @click="handleCreate">
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
      <el-table-column label="ParentID" align="center">
        <template slot-scope="{row}">
          <span>{{ row.parentID }}</span>
        </template>
      </el-table-column>
      <el-table-column label="name" align="center">
        <template slot-scope="{row}">
          <span>{{ row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Icon" align="center">
        <template slot-scope="{row}">
          <span>{{ row.icon }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Component" align="center">
        <template slot-scope="{row}">
          <span>{{ row.component }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Path" align="center">
        <template slot-scope="{row}">
          <span>{{ row.path }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Actions" align="center" width="200px" class-name="small-padding fixed-width">
        <template slot-scope="{row,$index}">
          <el-button type="primary" size="mini" icon="el-icon-edit" @click="handleUpdate(row)">
            编辑
          </el-button>
          <el-button v-if="row.status!='deleted'" icon="el-icon-delete" size="mini" type="danger" @click="handleDelete(row,$index)">
            删除
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.pageindex" :limit.sync="listQuery.pagesize" @pagination="getList" />

    <el-dialog :visible.sync="dialogVisible" :title="dialogType==='edit'?'编辑菜单':'新增菜单'">
      <el-form ref="dataForm" :model="dialogData" label-width="100px" label-position="right" :rules="rules">
        <el-form-item label="父级菜单">
          <el-select
            v-model="dialogData.parentID"
            class="filter-item"
            placeholder="请选择菜单"
            style="width:100%"
          >
            <el-option
              v-for="item in menuList"
              :key="item.ID"
              :label="item.name"
              :value="item.id"
            />
          </el-select>
        </el-form-item>
        <el-form-item label="菜单名称" prop="name">
          <el-input v-model="dialogData.name" :disabled="dialogType==='edit'?true:false" />
        </el-form-item>
        <el-form-item label="图标" prop="icon">
          <el-input v-model="dialogData.icon" />
        </el-form-item>
        <el-form-item label="路径" prop="path">
          <el-input v-model="dialogData.path" />
        </el-form-item>
        <el-form-item label="重定向" prop="redirect">
          <el-input v-model="dialogData.redirect" />
        </el-form-item>
        <el-form-item label="组件" prop="component">
          <el-input v-model="dialogData.component" />
        </el-form-item>
        <el-form-item label="标题" prop="title">
          <el-input v-model="dialogData.title" />
        </el-form-item>
        <el-form-item label="路由菜单" prop="alwaysShow">
          <el-switch
            v-model="dialogData.alwaysShow"
            active-color="#13ce66"
            inactive-color="#ff4949"
          />
        </el-form-item>
        <el-form-item label="是否隐藏" prop="hidden">
          <el-switch
            v-model="dialogData.hidden"
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
  </div>
</template>

<script>
import { getMenus, addMenu, saveMenu, deleteMenu, getAllParentMenus } from '@/api/menu'
import waves from '@/directive/waves' // waves directive
import Pagination from '@/components/Pagination' // secondary package based on el-pagination

export default {
  name: 'MenuListTable',
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
        name: ''
      },
      menuList: [],
      dialogVisible: false,
      dialogType: '',
      dialogData: {
        parentID: '',
        name: '',
        icon: '',
        path: '',
        redirect: '',
        component: '',
        title: '',
        alwaysShow: true,
        hidden: false
      },
      rules: {
        name: [{ required: true, message: '菜单名是必填项', trigger: 'blur' }],
        // path: [{ validator: this.verifyPath, trigger: 'blur' }],
        path: [{ required: true, message: '路径是必填项', trigger: 'blur' }],
        component: [{ required: true, message: '组件是必填项', trigger: 'blur' }]
      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    // verifyPath(rule, value, callback) {
    //   if (this.dialogData.parentID !== '' && this.dialogData.parentID > -1 && value === '') {
    //     callback(new Error('路径是必填项'))
    //   } else {
    //     callback()
    //   }
    // },
    getList() {
      this.listLoading = true
      getMenus(this.listQuery).then(res => {
        this.list = res.response.list
        this.total = res.response.totalCount
        this.listLoading = false
      }).catch(
        this.listLoading = false
      )
    },
    getAllMenus() {
      getAllParentMenus().then(res => {
        this.menuList = res.response
      })
    },
    deleteMenu(id) {
      deleteMenu({ id: id }).then(res => {
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
    addMenu() {
      this.dialogData.createBy = this.$store.getters.userID
      this.dialogData.parentID = this.dialogData.parentID === '' ? -1 : this.dialogData.parentID
      addMenu(this.dialogData).then(res => {
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
    saveMenu() {
      this.dialogData.updatedBy = this.$store.getters.userID
      this.dialogData.parentID = this.dialogData.parentID === '' ? -1 : this.dialogData.parentID
      saveMenu(this.dialogData).then(res => {
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
            this.addMenu()
          } else {
            this.saveMenu()
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

        this.getAllMenus()
      })
    },
    handleUpdate(row) {
      this.dialogType = 'edit'
      this.dialogVisible = true
      Object.assign(this.dialogData, this.defaultValue())
      this.$nextTick(() => {
        this.$refs['dataForm'].resetFields()

        this.getAllMenus()
        this.dialogData = {
          id: row.id,
          parentID: row.parentID,
          name: row.name,
          icon: row.icon,
          path: row.path,
          redirect: row.redirect,
          component: row.component,
          title: row.title,
          alwaysShow: row.alwaysShow,
          hidden: row.hidden
        }
      })
    },
    handleDelete(row, index) {
      this.deleteMenu(row.id)
    },
    defaultValue() {
      this.dialogData = {
        parentID: '',
        name: '',
        icon: '',
        path: '',
        redirect: '',
        component: '',
        title: '',
        alwaysShow: true,
        hidden: false
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
