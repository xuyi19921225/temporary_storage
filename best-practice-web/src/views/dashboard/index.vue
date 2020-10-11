<template>
  <div v-if="menuIsShow" class="icon-menu">
    <div v-for="item in list" :key="item.id" @click="item.type==='Division'?getStation(item.id):setStation(item.id)">
      <el-card shadow="hover" class="hover">
        <el-image
          style="width: 150px; height: 150px"
          :src="item.icon"
        />
        <span class="title">{{ item.value }}</span>
      </el-card>
    </div>
  </div>
  <div v-else class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.projectTitle" placeholder="项目标题" style="width: 150px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-select v-model="listQuery.stationID" placeholder="Station" style="width: 150px" class="filter-item" clearable @change="handleFilter">
        <el-option v-for="item in stationList" :key="item.id" :label="item.value" :value="item.id" />
      </el-select>
      <el-select v-model="listQuery.department" placeholder="Department" style="width: 150px" class="filter-item" clearable @change="handleFilter">
        <el-option v-for="item in departmentList" :key="item.id" :label="item.department1" :value="item.department1" />
      </el-select>
      <el-button v-waves class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter">
        查询
      </el-button>
    </div>

    <el-table
      :key="tableKey"
      v-loading="listLoading"
      :data="tableList"
      :header-cell-style="{background:'#eef1f6',color:'#606266'}"
      fit
      :row-class-name="tableRowClassName"
    >
      <el-table-column label="Division" align="center">
        <template slot-scope="{row}">
          <span>{{ row.division }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Site" align="center">
        <template slot-scope="{row}">
          <span>{{ row.site }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Station" align="center" show-overflow-tooltip>
        <template slot-scope="{row}">
          <span>{{ row.station }}</span>
        </template>
      </el-table-column>
      <el-table-column label="部门" align="center" show-overflow-tooltip>
        <template slot-scope="{row}">
          <span>{{ row.department }}</span>
        </template>
      </el-table-column>
      <el-table-column label="项目标题" align="center">
        <template slot-scope="{row}">
          <span>{{ row.projectTitle }}</span>
        </template>
      </el-table-column>
      <el-table-column label="项目路径" align="center" show-overflow-tooltip>
        <template slot-scope="{row}">
          <a :href="row.attachment" :download="row.fileName"><el-link type="primary">{{ row.fileName }}</el-link></a>
        </template>
      </el-table-column>
      <el-table-column label="是否删除" align="center">
        <template slot-scope="{row}">
          <span>{{ row.isDeleteString }}</span>
        </template>
      </el-table-column>
      <el-table-column label="创建人" align="center">
        <template slot-scope="{row}">
          <span>{{ row.createBy }}</span>
        </template>
      </el-table-column>
      <el-table-column label="创建时间" align="center" show-overflow-tooltip>
        <template slot-scope="{row}">
          <span>{{ row.createAt }}</span>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.pageindex" :limit.sync="listQuery.pagesize" @pagination="getProjectList" />
  </div>
</template>

<script>
import { getProjectList } from '@/api/dashboard'
import { getSiteAreaListByParameter } from '@/api/sitearea'
import { getDepartmentList } from '@/api/department'
import waves from '@/directive/waves' // waves directive
import Pagination from '@/components/Pagination' // secondary package based on el-pagination

export default {
  name: 'Dashboard',
  components: { Pagination },
  directives: { waves },
  data() {
    return {
      tableKey: 0,
      list: [],
      menuIsShow: true,
      tableList: [],
      total: 0,
      listLoading: true,
      listQuery: {
        pageindex: 1,
        pagesize: 20,
        siteID: null,
        divisionID: null,
        stationID: null,
        projectTitle: '',
        department: ''
      },
      departmentList: [],
      stationList: []
    }
  },
  created() {
    this.listQuery.siteID = this.$store.getters.siteID
    this.getDivision()
  },
  methods: {
    getProjectList() {
      this.listLoading = true
      getProjectList(this.listQuery).then(res => {
        this.tableList = res.response.list
        this.total = res.response.totalCount
        this.listLoading = false
      })
    },
    getDivision() {
      getSiteAreaListByParameter({ parentID: this.listQuery.siteID, isActive: 0 }).then(res => {
        this.list = res.response
      })
    },
    getStation(id) {
      this.listQuery.divisionID = id
      getSiteAreaListByParameter({ parentID: this.listQuery.divisionID, isActive: 0 }).then(res => {
        this.list = res.response
      })
    },
    getStationList() {
      getSiteAreaListByParameter({ parentID: this.listQuery.divisionID, isActive: 0 }).then(res => {
        this.stationList = res.response
      })
    },
    getDepartmentList() {
      getDepartmentList().then(res => {
        this.departmentList = res.response
      })
    },
    setStation(id) {
      this.menuIsShow = false
      this.getDepartmentList()
      this.getStationList()
      this.listQuery.stationID = id
      this.getProjectList()
    },
    handleFilter() {
      this.listQuery.pageindex = 1
      this.getProjectList()
    },
    tableRowClassName({ row, rowIndex }) {
      if (row.isDelete === 0) {
        return 'success-row'
      } else if (row.isDelete === 1) {
        return 'danger-row'
      }
      return ''
    }
  }
}
</script>

<style lang="scss" scoped>
.icon-menu{
  display: flex;
  align-items: center;
  justify-content:space-around;
  height: calc(100vh - 50px);
}
.hover{
  cursor: pointer;
}

.title{
  display:block;
  text-align:center;
  margin-bottom:-10px;
  font-size:23px;
  font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
  font-weight: bold;
}

</style>

<style>
.el-table .danger-row {
    background:#fee1e2;
  }

.el-table .success-row {
    background: #f0f9eb;
  }
</style>
