<template>
  <div class="app-container">
    <div class="filter-container">
      <el-button class="filter-item" type="primary" icon="el-icon-document-add" @click="handleCreate">
        新增
      </el-button>
    </div>

    <el-table
      :data="tableData"
      row-key="id"
      :header-cell-style="{background:'#eef1f6',color:'#606266'}"
      :row-class-name="tableRowClassName"
      lazy
      :load="load"
      :tree-props="{children: 'children', hasChildren: 'hasChildren'}"
    >
      <el-table-column
        prop="value"
        label="名称"
      />
      <el-table-column
        prop="type"
        label="类型"
      />
      <el-table-column prop="icon" label="图标">
        <template slot-scope="{row}">
          <a v-if="row.icon!==''" :href="row.icon" target="_blank"><el-link type="primary">{{ row.value }}</el-link></a>
        </template></el-table-column>
      <el-table-column
        prop="isActiveName"
        label="是否启用"
      />
      <el-table-column
        prop="isActiveName"
        label="Actions"
      >
        <template slot-scope="{row}">
          <el-button type="primary" size="mini" @click="handleEdit(row)">
            编辑
          </el-button>
          <el-button v-if="row.type!='Station'" size="mini" type="danger" @click="handleCreateChildren(row)">
            添加子级
          </el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.pageindex" :limit.sync="listQuery.pagesize" @pagination="getList" />

    <el-dialog :title="textMap[dialogStatus]" :visible.sync="dialogFormVisible">
      <el-form ref="dataForm" :rules="rules" :model="temp" label-position="left" label-width="70px">
        <el-form-item v-if="false" label="类型" prop="parentID">
          <el-input v-model="temp.parentID" />
        </el-form-item>
        <el-form-item label="类型" prop="type">
          <el-input v-model="temp.type" disabled="" />
        </el-form-item>
        <el-form-item label="名称" prop="value">
          <el-input v-model="temp.value" />
        </el-form-item>
        <el-form-item label="是否启用" prop="isActive">
          <el-switch
            v-model="temp.isActive"
            active-color="#13ce66"
            inactive-color="#ff4949"
            :active-value="0"
            :inactive-value="1"
          /></el-form-item>
        <el-form-item
          label="图标"
          prop="icon"
        >
          <el-upload
            class="upload-demo"
            drag
            action="#"
            :http-request="httpUpload"
            :on-remove="handleRemove"
            :file-list="fileList"
            :before-upload="beforeUpload"
            accept=".jpg,.png"
          >
            <i class="el-icon-upload" />
            <div class="el-upload__text">将文件拖到此处，或<em>点击上传</em></div>
            <div
              slot="tip"
              class="el-upload__tip"
            >只能上传jpg/png文件，且不超过500kb</div>
          </el-upload>
        </el-form-item>
      </el-form>

      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">
          取消
        </el-button>
        <el-button type="primary" @click="dialogStatus==='create'?createData():updateData()">
          确认
        </el-button>
      </div>
    </el-dialog>

  </div>
</template>

<script>
import Pagination from '@/components/Pagination'
import { fileUpload } from '@/api/upload'
import { getSiteAreaPagingList, getSiteAreaList, addSiteAreaList, saveSiteAreaList } from '@/api/sitearea'

export default {
  name: 'MainTenanceTable',
  components: { Pagination },
  data() {
    return {
      listLoading: true,
      listQuery: {
        pageindex: 1,
        pagesize: 20,
        parentID: -1
      },
      tableData: [],
      total: 0,
      textMap: {
        update: 'Edit',
        create: 'Create'
      },
      fileList: [],
      dialogFormVisible: false,
      dialogStatus: '',
      temp: {
        parentID: -1,
        type: '',
        isActive: 0,
        value: '',
        icon: ''
      },
      siteList: [],
      dictionaryTypeOptions: [{
        key: 0,
        type: 'Site'
      }, {
        key: 1,
        type: 'Division'
      }, {
        key: 3,
        type: 'Station'
      }],
      rules: {
        type: [
          { required: true, message: '类型不能为空', trigger: ['blur', 'change'] }
        ],
        value: [
          { required: true, message: '名称不能为空', trigger: ['blur', 'change'] }
        ]
      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    getList() {
      this.listLoading = true
      getSiteAreaPagingList(this.listQuery).then(res => {
        this.tableData = res.response.list
        this.total = res.response.totalCount
        this.listLoading = false
      })
    },
    createData() {
      this.temp.createBy = this.$store.getters.name
      addSiteAreaList(this.temp).then(res => {
        if (res.success === true) {
          this.$message('创建成功')
          this.dialogFormVisible = false
          this.$router.replace({
            path: '/redirect' + this.$route.path
          })
        } else {
          this.$message(res.response.message)
        }
      })
    },
    updateData() {
      this.temp.updatedBy = this.$store.getters.name
      saveSiteAreaList(this.temp).then(res => {
        if (res.success === true) {
          this.$message('更新成功')
          this.dialogFormVisible = false
          this.$router.replace({
            path: '/redirect' + this.$route.path
          })
        } else {
          this.$message(res.message)
        }
      })
    },
    httpUpload(params) {
      const file = params.file

      // 根据后台需求数据格式
      const form = new FormData()
      // 文件对象
      form.append('file', file)

      // 项目封装的请求方法，下面做简单介绍
      fileUpload(form)
        .then(res => {
          if (res.success === true) {
            this.$message('上传成功')
            this.temp.icon = res.response.url
            this.fileList = [{ name: this.temp.value, url: this.temp.icon }]
          } else {
            this.$message.error(res.message)
            this.fileList = []
          }
        })
    },
    load(tree, treeNode, resolve) {
      getSiteAreaList({ parentID: tree.id }).then(res => {
        resolve(res.response)
      })
    },
    handleRemove(file, fileList) {
      if (file && file.status === 'success') {
        this.fileList = []
      }
    },
    beforeUpload(file) {
      const fileType = file.type
      const fileTypeArr = ['image/png', 'image/jpg']
      const isImage = fileTypeArr.indexOf(fileType) !== -1
      const isLt500K = file.size / 1024 / 1024 / 1024 < 500

      if (!isImage) {
        this.$message.error('只能上传图片格式png、jpg、gif!')
        return false
      }
      if (!isLt500K) {
        this.$message.error('只能上传图片大小小于500KB')
        return false
      }
      return true
    },
    handleCreate() {
      this.resetTemp()
      this.temp.type = 'Site'
      this.dialogStatus = 'create'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    handleCreateChildren(row) {
      this.resetTemp()
      if (row.type === 'Site') {
        this.temp.type = 'Division'
      } else if (row.type === 'Division') {
        this.temp.type = 'Station'
      }
      this.temp.parentID = row.id
      this.dialogStatus = 'create'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    handleEdit(row) {
      this.resetTemp()
      this.temp.id = row.id
      this.temp.type = row.type
      this.temp.value = row.value
      this.temp.isActive = row.isActive
      this.temp.parentID = row.parentID
      this.temp.icon = row.icon
      this.fileList = [{ name: this.temp.value, url: this.temp.icon }]
      this.dialogStatus = 'update'
      this.dialogFormVisible = true
      this.$nextTick(() => {
        this.$refs['dataForm'].clearValidate()
      })
    },
    tableRowClassName({ row, rowIndex }) {
      if (row.type === 'Site') {
        return 'level1'
      } else if (row.type === 'Division') {
        return 'level2'
      } else {
        return 'level3'
      }
    },
    resetTemp() {
      this.temp = {
        parentID: -1,
        type: '',
        isActive: 0,
        value: '',
        icon: ''
      }
      this.fileList = []
    }
  }
}
</script>

<style>

.el-table .level1 {
    background: #ebf6fe;
  }

.el-table .level2 {
    background:#fdf5ed;
  }

.el-table .level3{
    background: #ffeff0;
  }
</style>
