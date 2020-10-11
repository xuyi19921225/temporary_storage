<template>
  <div class="app-container">
    <div class="filter-container">
      <el-select v-model="listQuery.siteID" placeholder="Site" filterable clearable class="filter-item" style="width: 150px" @change="getSiteAreaList({parentID:listQuery.siteID,module:'division'})">
        <el-option v-for="item in siteList" :key="item.id" :label="item.value" :value="item.id" />
      </el-select>
      <el-select v-model="listQuery.divisionID" placeholder="Divison" filterable clearable style="width: 150px" class="filter-item" @change="getSiteAreaList({parentID:listQuery.divisionID,module:'station'})">
        <el-option v-for="item in divisionList" :key="item.id" :label="item.value" :value="item.id" />
      </el-select>
      <el-select v-model="listQuery.stationID" placeholder="Station" style="width: 150px" clearable class="filter-item" @change="handleFilter">
        <el-option v-for="item in stationList" :key="item.id" :label="item.value" :value="item.id" />
      </el-select>
      <el-select v-model="listQuery.isdelete" style="width: 150px" class="filter-item" clearable @change="handleFilter">
        <el-option v-for="item in isdeleteList" :key="item.key" :label="item.value" :value="item.key" />
      </el-select>
      <el-button v-waves class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter">
        查询
      </el-button>
      <el-button v-waves class="filter-item" type="primary" icon="el-icon-search" @click="handleCreate">
        新增
      </el-button>
      <el-upload
        ref="upload"
        class="upload-demo filter-item"
        action
        :http-request="handleUpload"
        :on-change="onChange"
        accept=".xlsx,.xls"
        :before-upload="beforeUpload"
        :show-file-list="false"
      >
        <el-button v-waves type="primary" :loading="uploadLoading" icon="el-icon-upload">上传</el-button>
      </el-upload>
      <el-button v-waves :loading="downloadLoading" class="filter-item" type="success" icon="el-icon-download" @click="handleDownload">
        模板下载
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
      <el-table-column label="描述" align="center">
        <template slot-scope="{row}">
          <span>{{ row.description }}</span>
        </template>
      </el-table-column>
      <el-table-column label="Actions" width="250px" align="center">
        <template slot-scope="{row,$index}">
          <el-button v-if="row.isDelete===0" type="primary" size="mini" @click="handleUpdate(row)">
            编辑
          </el-button>
          <el-button v-if="row.isDelete===0" size="mini" type="danger" @click="handleDelete(row,$index)">
            删除
          </el-button>
          <el-button type="primary" size="mini" @click="handleDetail(row,$index)">
            详情
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.pageindex" :limit.sync="listQuery.pagesize" @pagination="getList" />

    <el-dialog title="详情" :visible.sync="dialog.dialogFormVisible">
      <el-table
        :key="dialog.tableKey"
        v-loading="dialog.listLoading"
        :data="dialog.list"
        :header-cell-style="{background:'#eef1f6',color:'#606266'}"
        fit
        highlight-current-row
      >
        <el-table-column label="Action" align="center">
          <template slot-scope="{row}">
            <span>{{ row.action }}</span>
          </template>
        </el-table-column>
        <el-table-column label="操作人" align="center">
          <template slot-scope="{row}">
            <span>{{ row.createBy }}</span>
          </template>
        </el-table-column>
        <el-table-column label="操作时间" align="center">
          <template slot-scope="{row}">
            <span>{{ row.createAt }}</span>
          </template>
        </el-table-column>
      </el-table>

      <pagination v-show="dialog.total>0" :total="dialog.total" :page.sync="dialog.listQuery.pageindex" :limit.sync="dialog.listQuery.pagesize" @pagination="getProjectHistoryList" />
    </el-dialog>

  </div>
</template>

<script>
import { getProjectList, deleteProject, batchSaveProject } from '@/api/project'
import { getProjectHistoryList } from '@/api/projecthistory'
import { getSiteAreaListByParameter } from '@/api/sitearea'
import waves from '@/directive/waves' // waves directive
import { parseTime } from '@/utils'
import XLSX from 'xlsx'
import Pagination from '@/components/Pagination' // secondary package based on el-pagination

export default {
  name: 'ProjectIndexTable',
  components: { Pagination },
  directives: { waves },
  data() {
    return {
      tableKey: 0,
      list: null,
      total: 0,
      uploadLoading: false,
      downloadLoading: false,
      filename: '项目导入模板',
      autoWidth: true,
      bookType: 'xlsx',
      listLoading: false,
      listQuery: {
        pageindex: 1,
        pagesize: 20,
        siteID: '',
        divisionID: '',
        stationID: '',
        isdelete: 0
      },
      siteList: [],
      divisionList: [],
      stationList: [],
      isdeleteList: [{
        key: 0, value: '未删除'
      }, {
        key: 1, value: '已删除'
      }],
      dialog: {
        tableKey: 1,
        dialogFormVisible: false,
        listLoading: true,
        list: null,
        total: 0,
        listQuery: {
          pageindex: 1,
          pagesize: 10,
          parentID: null
        }
      }
    }
  },
  created() {
    this.getList()
    this.getSiteAreaList({ parentID: -1 })
  },
  methods: {
    getList() {
      this.listLoading = true
      getProjectList(this.listQuery).then(res => {
        this.list = res.response.list
        this.total = res.response.totalCount
        this.listLoading = false
      }).catch(
        this.listLoading = false
      )
    },
    getSiteAreaList({ parentID, module = 'site' }) {
      getSiteAreaListByParameter({ parentID: parentID }).then(res => {
        switch (module) {
          case 'site':
            this.siteList = res.response
            break
          case 'division':
            this.divisionList = res.response
            break
          case 'station':
            this.stationList = res.response
            break
          default:
            break
        }
      })
    },
    getProjectHistoryList() {
      getProjectHistoryList(this.dialog.listQuery).then(res => {
        this.dialog.list = res.response.list
        this.dialog.total = res.response.totalCount
        this.dialog.listLoading = false
      }).catch(
        this.dialog.listLoading = false
      )
    },
    handleUpload(file) {
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
          defval: ''
        })

        if (outdata.length > 0) {
          var list = []

          for (let index = 0; index < outdata.length; index++) {
            list.push({
              site: outdata[index]['Site'],
              division: outdata[index]['Division'],
              station: outdata[index]['Station'],
              department: outdata[index]['Department'],
              projectTitle: outdata[index]['ProjectTitle'],
              createBy: this.$store.getters.name
            })
          }

          batchSaveProject(list).then(res => {
            if (res.success === true) {
              this.$message(res.response.message)
              this.getList()
            } else {
              this.$message(res.response.message)
            }
            this.uploadLoading = false
          })
        } else {
          this.$message('没有数据批量导入')
          this.uploadLoading = false
        }
      }
      reader.readAsArrayBuffer(file.file)
    },
    handleDownload() {
      this.downloadLoading = true
      import('@/vendor/Export2Excel').then(excel => {
        const tHeader = ['Site', 'Division', 'Station', 'Department', 'ProjectTitle']
        const filterVal = ['Site', 'Division', 'Station', 'Department', 'ProjectTitle']
        const data = this.formatJson(filterVal)
        excel.export_json_to_excel({
          header: tHeader,
          data,
          filename: this.filename,
          autoWidth: this.autoWidth,
          bookType: this.bookType
        })
        this.downloadLoading = false
      })
    },
    handleFilter() {
      this.listQuery.pageindex = 1
      this.getList()
    },
    onChange(file, fileList) {
      this.$refs['upload'].clearFiles()
    },
    beforeUpload(file) {
      const fileType = file.type
      const fileTypeArr = ['application/vnd.openxmlformats-officedocument.spreadsheetml.sheet']
      const fileTrpeArr2 = ['application/vnd.ms-excel']
      const isFileType = fileTypeArr.indexOf(fileType) !== -1 || fileTrpeArr2.indexOf(fileType) !== -1
      const isLt2M = file.size / 1024 / 1024 < 2

      if (!isFileType) {
        this.$message.error('只能上传Excel文件格式')
        return false
      }
      if (!isLt2M) {
        this.$message.error('只能上传Excel文件大小小于2M')
        return false
      }
      return true
    },
    handleCreate() {
      this.$router.push({
        path: '/project-maintenance/create'
      })
    },
    handleUpdate(row) {
      this.$router.push({
        path: '/project-maintenance/edit',
        query: {
          id: row.id,
          siteID: row.siteID,
          divisionID: row.divisionID
        }
      })
    },
    handleDelete(row, index) {
      this.$confirm('是否删除数据？', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        deleteProject({ id: row.id, updatedBy: this.$store.getters.name }).then(res => {
          if (res.success === true) {
            this.$notify({
              title: 'Success',
              message: 'Delete Successfully',
              type: 'success',
              duration: 2000
            })
            this.getList()
          } else {
            this.$notify({
              title: 'Success',
              message: 'Delete Failed',
              type: 'warning',
              duration: 2000
            })
          }
        })
      })
    },
    handleDetail(row, index) {
      this.dialog.dialogFormVisible = true
      this.dialog.listQuery.parentID = row.id
      this.getProjectHistoryList()
    },
    formatJson(filterVal) {
      return this.list.map(v => filterVal.map(j => {
        if (j === 'timestamp') {
          return parseTime(v[j])
        } else {
          return v[j]
        }
      }))
    },
    getSortClass: function(key) {
      const sort = this.listQuery.sort
      return sort === `+${key}` ? 'ascending' : 'descending'
    }
  }
}
</script>
