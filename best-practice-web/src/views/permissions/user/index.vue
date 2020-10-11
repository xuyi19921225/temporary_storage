<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.NTID" placeholder="NTID" style="width: 150px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-input v-model="listQuery.jobNumber" placeholder="工号" style="width: 150px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-select v-model="listQuery.siteID" placeholder="Site" filterable clearable class="filter-item" style="width: 150px" @change="getDivisonListBySiteID">
        <el-option v-for="item in siteList" :key="item.id" :label="item.value" :value="item.id" />
      </el-select>
      <el-select v-model="listQuery.divisionID" placeholder="Divison" filterable clearable style="width: 150px" class="filter-item" @change="handleFilter">
        <el-option v-for="item in divisionList" :key="item.id" :label="item.value" :value="item.id" />
      </el-select>
      <el-select v-model="listQuery.department1" placeholder="Department" clearable style="width: 150px" class="filter-item" @change="handleFilter">
        <el-option v-for="item in departmentList" :key="item.id" :label="item.department1" :value="item.department1" />
      </el-select>
      <el-button v-waves class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter">
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
      <el-table-column label="Division" width="110px" align="center">
        <template slot-scope="{row}">
          <span>{{ row.division }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Site" width="110px" align="center">
        <template slot-scope="{row}">
          <span>{{ row.site }}</span>
        </template>
      </el-table-column>
      <el-table-column label="部门" width="200px" align="center" show-overflow-tooltip>
        <template slot-scope="{row}">
          <span>{{ row.department }}</span>
        </template>
      </el-table-column>
      <el-table-column label="邮箱" width="200px" align="center" show-overflow-tooltip>
        <template slot-scope="{row}">
          <span>{{ row.email }}</span>
        </template>
      </el-table-column>
      <el-table-column label="用户名" width="110px" align="center">
        <template slot-scope="{row}">
          <span>{{ row.userName }}</span>
        </template>
      </el-table-column>
      <el-table-column label="角色" width="110px" align="center">
        <template slot-scope="{row}">
          <span>{{ row.roleName }}</span>
        </template>
      </el-table-column>
      <el-table-column label="创建人" width="110px" align="center">
        <template slot-scope="{row}">
          <span>{{ row.createBy }}</span>
        </template>
      </el-table-column>
      <el-table-column label="创建时间" align="center" show-overflow-tooltip>
        <template slot-scope="{row}">
          <span>{{ row.createAt }}</span>
        </template>
      </el-table-column>
      <el-table-column label="修改人" width="110px" align="center">
        <template slot-scope="{row}">
          <span>{{ row.updatedBy }}</span>
        </template>
      </el-table-column>
      <el-table-column label="修改时间" align="center" show-overflow-tooltip>
        <template slot-scope="{row}">
          <span>{{ row.updatedAt }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Actions" align="center" width="150px" class-name="small-padding fixed-width">
        <template slot-scope="{row,$index}">
          <el-button type="primary" size="mini" @click="handleUpdate(row)">
            编辑
          </el-button>
          <el-button v-if="row.status!='deleted'" size="mini" type="danger" @click="handleDelete(row,$index)">
            删除
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.pageindex" :limit.sync="listQuery.pagesize" @pagination="getList" />

  </div>
</template>

<script>
import { getUserList, deleteUser } from '@/api/user'
import { getSiteAreaListByParameter } from '@/api/sitearea'
import { getDepartmentList } from '@/api/department'
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
        jobNumber: '',
        NTID: '',
        divisionID: '',
        siteID: '',
        department1: ''
      },
      siteList: [],
      divisionList: [],
      departmentList: []
    }
  },
  created() {
    this.getList()
    this.getSiteList()
    this.getDepartmentList()
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
    getSiteList() {
      getSiteAreaListByParameter({ parentID: -1 }).then(res => {
        this.siteList = res.response
      })
    },
    getDivisonListBySiteID() {
      getSiteAreaListByParameter({ parentID: this.listQuery.siteID }).then(res => {
        this.divisionList = res.response
      })
    },
    getDepartmentList() {
      getDepartmentList().then(res => {
        this.departmentList = res.response
      })
    },
    deleteUser(id) {
      deleteUser({ id: id }).then(res => {
        if (res.success === true) {
          this.$notify({
            title: 'Success',
            message: 'Delete Successfully',
            type: 'success',
            duration: 2000
          })
          this.getList()
        } else {
          this.$notify.error({
            title: 'Error',
            message: 'Delete Failed',
            duration: 2000
          })
        }
      })
    },
    handleFilter() {
      this.listQuery.pageindex = 1
      this.getList()
    },
    handleCreate() {
      this.$router.push({
        path: '/user/create'
      })
    },
    handleUpdate(row) {
      this.$router.push({
        path: '/user/edit',
        query: {
          id: row.id,
          siteID: row.siteID
        }
      })
    },
    handleDelete(row, index) {
      this.deleteUser(row.id)
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
